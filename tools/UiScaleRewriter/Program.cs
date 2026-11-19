// Rewrites the game's source so fonts and pixel layout values go through UiScale.
//
//   1. `new Font(...)`            -> `UiScale.NewFont(...)`
//   2. numeric literals that end up as WinForms layout values or GDI+ drawing
//      coordinates                -> `UiScale.S(literal)`
//
// Rule 2 starts from known sinks (Control.Location, Graphics.DrawString, ...) and
// follows values backwards through locals, fields, parameters, properties and
// method returns, so `int rowHeight = 20;` is scaled when rowHeight is only ever
// used to position things. Values fed by game-model data are left alone.
//
// Usage: UiScaleRewriter <path-to-csproj> [--apply] [--report <file>]

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Text;

if (args.Length == 0)
{
    Console.Error.WriteLine("usage: UiScaleRewriter <dump-prefix> [--apply] [--report <file>]");
    Console.Error.WriteLine("  <dump-prefix> is the folder + project name written by tools/DumpCompile.targets");
    return 2;
}

string prefix = args[0];
bool apply = args.Contains("--apply");
string? reportPath = args.SkipWhile(a => a != "--report").Skip(1).FirstOrDefault();

Console.WriteLine("Loading project...");
string[] defines = File.ReadAllText(prefix + ".defines.txt")
    .Split(new[] { ';', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
var parseOptions = new CSharpParseOptions(LanguageVersion.Preview, preprocessorSymbols: defines);
var syntaxTrees = File.ReadAllLines(prefix + ".compile.txt")
    .Where(line => line.Length > 0)
    .AsParallel().AsOrdered()
    .Select(path =>
    {
        using FileStream stream = File.OpenRead(path);
        return CSharpSyntaxTree.ParseText(SourceText.From(stream), parseOptions, path);
    })
    .ToList();
var references = File.ReadAllLines(prefix + ".refs.txt")
    .Where(line => line.Length > 0)
    .Select(line =>
    {
        string[] parts = line.Split('|');
        MetadataReference reference = MetadataReference.CreateFromFile(parts[0]);
        return parts.Length > 1 && parts[1].Length > 0
            ? ((PortableExecutableReference)reference).WithAliases(parts[1].Split(',', StringSplitOptions.TrimEntries))
            : reference;
    })
    .ToList();
Compilation compilation = CSharpCompilation.Create("DistantWorlds", syntaxTrees, references,
    new CSharpCompilationOptions(OutputKind.WindowsApplication, allowUnsafe: true));

// --members <metadata type name>: list a referenced type's members and exit (for checking library APIs).
if (args.SkipWhile(a => a != "--members").Skip(1).FirstOrDefault() is { } typeName)
{
    INamedTypeSymbol? type = compilation.GetTypeByMetadataName(typeName);
    Console.WriteLine(type == null ? "type not found" : $"{type.DeclaredAccessibility} {type.ToDisplayString()} : {type.BaseType}");
    foreach (ISymbol member in type?.GetMembers() ?? default)
    {
        Console.WriteLine($"  {member.DeclaredAccessibility,-10} {member.Kind,-10} {member.ToDisplayString()}");
    }
    return 0;
}

int loadErrors = compilation.GetDiagnostics().Count(d => d.Severity == DiagnosticSeverity.Error);
Console.WriteLine($"Loaded {compilation.SyntaxTrees.Count()} files, {loadErrors} compile errors before rewriting.");
if (loadErrors > 0)
{
    foreach (Diagnostic d in compilation.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error).Take(10))
    {
        Console.WriteLine("  " + d);
    }
    Console.Error.WriteLine("Refusing to rewrite a project that does not compile.");
    return 1;
}

var rewriter = new Rewriter(compilation);
rewriter.Run();
rewriter.WriteResults(apply, reportPath);
return 0;

sealed class Rewriter
{
    private static readonly SymbolEqualityComparer Eq = SymbolEqualityComparer.Default;

    private readonly Compilation _comp;
    private readonly Dictionary<SyntaxTree, SemanticModel> _models = new();
    private readonly List<SyntaxTree> _trees;

    // Symbol -> every expression assigned to / passed for / returned from it.
    private readonly Dictionary<ISymbol, List<ExpressionSyntax>> _defs = new(Eq);
    private readonly List<ExpressionSyntax> _sinks = new();
    // Sinks that only count once their receiver is known to hold pixels (rect.X = 5, rect.Inflate(2, 2)).
    private readonly List<(ISymbol Receiver, ExpressionSyntax Expr)> _conditionalSinks = new();
    // A by-reference parameter and a variable passed for it: if one holds pixels, so does the other.
    private readonly List<(ISymbol Parameter, ISymbol Variable)> _aliases = new();
    // `width < 300`-style comparisons; the literal is a pixel threshold when the other side holds pixels.
    private readonly List<BinaryExpressionSyntax> _thresholds = new();

    private readonly HashSet<ISymbol> _pixel = new(Eq);
    private readonly HashSet<ISymbol> _banned = new(Eq);
    private readonly Dictionary<ISymbol, bool> _logicFed = new(Eq);

    private readonly Dictionary<SyntaxNode, MemberFlags> _memberFlags = new();
    private readonly Dictionary<SyntaxTree, List<TextChange>> _changes = new();
    private readonly HashSet<SyntaxNode> _marked = new();
    private readonly List<string> _notes = new();
    private int _fontCount, _literalCount;
    private bool _changed;
    private bool _marking;

    private readonly INamedTypeSymbol? _control, _form, _toolStripItem, _gridBand, _columnHeader, _cellStyle;
    private readonly INamedTypeSymbol? _graphics, _font, _bitmap, _textRenderer, _controlPaint, _graphicsPath;
    private readonly INamedTypeSymbol? _stringFormat, _gradientBrush, _region, _columnStyle, _rowStyle, _padding;
    private readonly HashSet<INamedTypeSymbol> _geometry = new(Eq);

    private static readonly HashSet<string> LayoutProperties = new()
    {
        "Location", "Size", "Bounds", "Width", "Height", "Left", "Top", "ClientSize",
        "MinimumSize", "MaximumSize", "Margin", "Padding", "AutoScrollMinSize", "AutoScrollMargin",
        "ItemHeight", "ItemSize", "ColumnHeadersHeight", "RowHeadersWidth", "DropDownWidth",
        "DropDownHeight", "SplitterDistance", "Indent", "ColumnWidth", "MinimumWidth",
        "MinimumHeight", "DividerWidth", "DividerHeight", "TileSize",
    };

    private static readonly HashSet<string> OwnWindowProperties = new()
    {
        "Location", "Size", "Bounds", "Width", "Height", "Left", "Top", "ClientSize", "MinimumSize", "MaximumSize",
    };

    // Framework members that already hold real (scaled) pixels.
    private static readonly HashSet<string> PixelMemberNames = new()
    {
        "Width", "Height", "X", "Y", "Left", "Top", "Right", "Bottom", "Size", "Location", "ClientSize",
        "ClientRectangle", "Bounds", "DisplayRectangle", "PreferredSize", "PreferredHeight", "PreferredWidth",
    };

    private static readonly HashSet<string> PixelMethodNames = new()
    {
        "MeasureString", "MeasureText", "GetHeight", "GetPreferredSize",
    };

    private static readonly HashSet<string> PassThroughMethods = new()
    {
        "Max", "Min", "Abs", "Round", "Floor", "Ceiling", "Truncate", "Clamp", "ToInt32", "ToSingle", "ToDouble",
    };

    private static readonly HashSet<string> NonPixelParameterNames = new()
    {
        "angle", "startAngle", "sweepAngle", "tension", "offset", "numberOfSegments", "count",
        "sx", "sy", "dpi", "xDpi", "yDpi", "index", "length", "startIndex",
    };

    private static readonly HashSet<string> CountParameterNames = new()
    {
        "index", "startIndex", "count", "length", "capacity", "totalWidth", "digits", "decimals",
        "repeatCount", "millisecondsTimeout", "interval", "dueTime", "period", "maxValue", "minValue",
    };

    public Rewriter(Compilation comp)
    {
        _comp = comp;
        _trees = comp.SyntaxTrees.Where(IsRewritable).ToList();

        _control = T("System.Windows.Forms.Control");
        _form = T("System.Windows.Forms.Form");
        _toolStripItem = T("System.Windows.Forms.ToolStripItem");
        _gridBand = T("System.Windows.Forms.DataGridViewBand");
        _columnHeader = T("System.Windows.Forms.ColumnHeader");
        _cellStyle = T("System.Windows.Forms.DataGridViewCellStyle");
        _graphics = T("System.Drawing.Graphics");
        _font = T("System.Drawing.Font");
        _bitmap = T("System.Drawing.Bitmap");
        _textRenderer = T("System.Windows.Forms.TextRenderer");
        _controlPaint = T("System.Windows.Forms.ControlPaint");
        _graphicsPath = T("System.Drawing.Drawing2D.GraphicsPath");
        _stringFormat = T("System.Drawing.StringFormat");
        _gradientBrush = T("System.Drawing.Drawing2D.LinearGradientBrush");
        _region = T("System.Drawing.Region");
        _columnStyle = T("System.Windows.Forms.ColumnStyle");
        _rowStyle = T("System.Windows.Forms.RowStyle");
        _padding = T("System.Windows.Forms.Padding");
        foreach (string name in new[] { "Point", "PointF", "Size", "SizeF", "Rectangle", "RectangleF" })
        {
            if (T("System.Drawing." + name) is { } t)
            {
                _geometry.Add(t);
            }
        }
        if (_padding != null)
        {
            _geometry.Add(_padding);
        }
    }

    private INamedTypeSymbol? T(string name) => _comp.GetTypeByMetadataName(name);

    private static bool IsRewritable(SyntaxTree tree)
    {
        string path = tree.FilePath.Replace('/', '\\');
        if (path.Contains("\\obj\\") || path.Contains("\\bin\\"))
        {
            return false;
        }
        if (path.Contains("\\NetDataContractSerializer\\"))
        {
            return false;
        }
        return !path.EndsWith("\\UiScale.cs", StringComparison.OrdinalIgnoreCase)
            && !path.EndsWith("\\ScaledSpriteFont.cs", StringComparison.OrdinalIgnoreCase);
    }

    private SemanticModel M(SyntaxTree tree)
    {
        if (!_models.TryGetValue(tree, out SemanticModel? model))
        {
            _models[tree] = model = _comp.GetSemanticModel(tree);
        }
        return model;
    }

    private SemanticModel M(SyntaxNode node) => M(node.SyntaxTree);

    public void Run()
    {
        Console.WriteLine("Indexing...");
        foreach (SyntaxTree tree in _trees)
        {
            Index(tree);
        }
        Console.WriteLine($"  {_sinks.Count} direct sinks, {_defs.Count} symbols with definitions.");

        SeedSpriteTextPositions();

        int round = 0;
        do
        {
            _changed = false;
            VisitRoots();
            Console.WriteLine($"  round {++round}: {_pixel.Count} pixel symbols");
        }
        while (_changed && round < 50);

        _marking = true;
        VisitRoots();
        Console.WriteLine($"Marked {_literalCount} layout values and {_fontCount} fonts.");
    }

    // Text on the main view is drawn with sprite fonts, which are scaled, so the fixed
    // offsets passed to the text helper have to grow with them.
    private void SeedSpriteTextPositions()
    {
        foreach (INamedTypeSymbol helper in _comp.GetSymbolsWithName("XnaDrawingHelper", SymbolFilter.Type).OfType<INamedTypeSymbol>())
        {
            foreach (IMethodSymbol method in helper.GetMembers().OfType<IMethodSymbol>())
            {
                if (!method.Name.StartsWith("DrawString", StringComparison.Ordinal))
                {
                    continue;
                }
                foreach (IParameterSymbol parameter in method.Parameters)
                {
                    if (parameter.Name is "x" or "y" or "point" && IsPixelType(parameter.Type))
                    {
                        _pixel.Add(parameter);
                    }
                }
            }
        }
    }

    private void VisitRoots()
    {
        foreach (ExpressionSyntax sink in _sinks)
        {
            Visit(sink);
        }
        foreach ((ISymbol receiver, ExpressionSyntax expr) in _conditionalSinks)
        {
            if (_pixel.Contains(receiver))
            {
                Visit(expr);
            }
        }
        foreach ((ISymbol parameter, ISymbol variable) in _aliases)
        {
            if (_pixel.Contains(parameter))
            {
                Taint(variable);
            }
            else if (_pixel.Contains(variable))
            {
                Taint(parameter);
            }
        }
        foreach (ISymbol symbol in _pixel.ToList())
        {
            if (_defs.TryGetValue(symbol, out List<ExpressionSyntax>? defs))
            {
                foreach (ExpressionSyntax def in defs)
                {
                    Visit(def);
                }
            }
        }
        if (_marking)
        {
            foreach (BinaryExpressionSyntax comparison in _thresholds)
            {
                bool literalOnLeft = IsThresholdLiteral(comparison.Left);
                if (HoldsUiPixels(literalOnLeft ? comparison.Right : comparison.Left))
                {
                    Mark(Unwrap(literalOnLeft ? comparison.Left : comparison.Right));
                }
            }
        }
    }

    // A plain number of 2 or more; 0, 1 and negative values are usually flags or sentinels.
    private static bool IsThresholdLiteral(ExpressionSyntax e)
    {
        e = Unwrap(e);
        return e is LiteralExpressionSyntax literal && literal.IsKind(SyntaxKind.NumericLiteralExpression)
            && literal.Token.Value is IConvertible value && value.ToDouble(null) >= 2.0;
    }

    // True when the expression as a whole is a UI pixel quantity (not merely mentions one).
    private bool HoldsUiPixels(ExpressionSyntax e)
    {
        e = Unwrap(e);
        switch (e)
        {
            case BinaryExpressionSyntax b when b.Kind() is SyntaxKind.AddExpression or SyntaxKind.SubtractExpression:
                return HoldsUiPixels(b.Left) || HoldsUiPixels(b.Right);
            case BinaryExpressionSyntax b when b.IsKind(SyntaxKind.MultiplyExpression):
                return HoldsUiPixels(b.Left) || HoldsUiPixels(b.Right);
            case BinaryExpressionSyntax b when b.IsKind(SyntaxKind.DivideExpression):
                return HoldsUiPixels(b.Left);
            case InvocationExpressionSyntax invocation:
                string? called = (invocation.Expression as MemberAccessExpressionSyntax)?.Name.Identifier.ValueText;
                if (called != null && PixelMethodNames.Contains(called))
                {
                    return true;
                }
                return called != null && PassThroughMethods.Contains(called)
                    && invocation.ArgumentList.Arguments.Any(a => HoldsUiPixels(a.Expression));
            case IdentifierNameSyntax or MemberAccessExpressionSyntax:
                ISymbol? raw = M(e).GetSymbolInfo(e).Symbol;
                if (raw == null)
                {
                    return false;
                }
                if (Normalize(raw) is { } symbol && _pixel.Contains(symbol.OriginalDefinition))
                {
                    return true;
                }
                if (InSource(raw) || raw is not (IPropertySymbol or IFieldSymbol) || !PixelMemberNames.Contains(raw.Name))
                {
                    return false;
                }
                // Width/Height of an image is the picture's own size, which is not scaled.
                string ns = raw.ContainingType?.ContainingNamespace?.ToDisplayString() ?? "";
                return (ns.StartsWith("System.Drawing", StringComparison.Ordinal) || ns.StartsWith("System.Windows.Forms", StringComparison.Ordinal))
                    && raw.ContainingType?.Name is not ("Image" or "Bitmap" or "Icon" or "Metafile");
            default:
                return false;
        }
    }

    // ---------------------------------------------------------------- indexing

    private void Index(SyntaxTree tree)
    {
        SemanticModel model = M(tree);
        foreach (SyntaxNode node in tree.GetRoot().DescendantNodes())
        {
            switch (node)
            {
                case ObjectCreationExpressionSyntax creation:
                    IndexFont(creation, model);
                    break;
                case ExpressionStatementSyntax { Expression: InvocationExpressionSyntax initialize } statement
                    when initialize.Expression.ToString() is "InitializeComponent" or "this.InitializeComponent"
                        && statement.Parent?.Parent is ConstructorDeclarationSyntax:
                    IndexAutoScaledContainer(statement, initialize, model);
                    break;
                case VariableDeclaratorSyntax { Initializer: not null } declarator:
                    if (model.GetDeclaredSymbol(declarator) is { } declared and (ILocalSymbol or IFieldSymbol))
                    {
                        AddDef(declared, declarator.Initializer.Value);
                    }
                    break;
                case PropertyDeclarationSyntax property:
                    if (model.GetDeclaredSymbol(property) is { } propertySymbol)
                    {
                        if (property.Initializer != null)
                        {
                            AddDef(propertySymbol, property.Initializer.Value);
                        }
                        if (property.ExpressionBody != null)
                        {
                            AddDef(propertySymbol, property.ExpressionBody.Expression);
                        }
                    }
                    break;
                case AssignmentExpressionSyntax assignment:
                    IndexAssignment(assignment, model);
                    break;
                case ArgumentSyntax argument:
                    IndexArgument(argument, model);
                    break;
                case ReturnStatementSyntax { Expression: not null } ret:
                    if (ReturnOwner(model.GetEnclosingSymbol(ret.SpanStart)) is { } owner)
                    {
                        AddDef(owner, ret.Expression);
                    }
                    break;
                case ArrowExpressionClauseSyntax arrow when arrow.Parent is not PropertyDeclarationSyntax:
                    if (ReturnOwner(model.GetEnclosingSymbol(arrow.Expression.SpanStart)) is { } arrowOwner)
                    {
                        AddDef(arrowOwner, arrow.Expression);
                    }
                    break;
                case ElementAccessExpressionSyntax elementAccess:
                    foreach (ArgumentSyntax indexArgument in elementAccess.ArgumentList.Arguments)
                    {
                        BanIdentifiers(indexArgument.Expression, model);
                    }
                    break;
                case PostfixUnaryExpressionSyntax postfix
                    when postfix.IsKind(SyntaxKind.PostIncrementExpression) || postfix.IsKind(SyntaxKind.PostDecrementExpression):
                    BanIdentifiers(postfix.Operand, model);
                    break;
                case PrefixUnaryExpressionSyntax prefix
                    when prefix.IsKind(SyntaxKind.PreIncrementExpression) || prefix.IsKind(SyntaxKind.PreDecrementExpression):
                    BanIdentifiers(prefix.Operand, model);
                    break;
                case BinaryExpressionSyntax comparison when IsComparison(comparison):
                    if (IsCountAccess(comparison.Left))
                    {
                        BanIdentifiers(comparison.Right, model);
                    }
                    if (IsCountAccess(comparison.Right))
                    {
                        BanIdentifiers(comparison.Left, model);
                    }
                    if (!comparison.IsKind(SyntaxKind.EqualsExpression) && !comparison.IsKind(SyntaxKind.NotEqualsExpression)
                        && IsThresholdLiteral(comparison.Left) != IsThresholdLiteral(comparison.Right)
                        && !InWorldView(comparison))
                    {
                        _thresholds.Add(comparison);
                    }
                    break;
                case ArrayRankSpecifierSyntax rank:
                    foreach (ExpressionSyntax size in rank.Sizes)
                    {
                        BanIdentifiers(size, model);
                    }
                    break;
                case ForStatementSyntax { Declaration: not null } loop:
                    foreach (VariableDeclaratorSyntax loopVariable in loop.Declaration.Variables)
                    {
                        if (model.GetDeclaredSymbol(loopVariable) is { } loopSymbol)
                        {
                            _banned.Add(loopSymbol);
                        }
                    }
                    break;
            }
        }
    }

    // A container whose designer layout is auto-scaled from its font only follows the UI scale
    // if that font is scaled, so make its constructor scale the font right after the layout is built.
    private void IndexAutoScaledContainer(ExpressionStatementSyntax statement, InvocationExpressionSyntax initialize, SemanticModel model)
    {
        const string Call = "UiScale.ScaleLayout(this);";
        if (model.GetSymbolInfo(initialize).Symbol is not IMethodSymbol method
            || method.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() is not MethodDeclarationSyntax { Body: { } body }
            || !FlagsFor(body).AutoScaledInit)
        {
            return;
        }
        if (statement.Parent is BlockSyntax block)
        {
            int index = block.Statements.IndexOf(statement);
            if (index + 1 < block.Statements.Count && block.Statements[index + 1].ToString().Trim() == Call)
            {
                return;
            }
        }
        string indent = string.Concat(statement.GetLeadingTrivia().ToFullString().Split('\n').Last());
        AddChange(statement.SyntaxTree, new TextChange(new TextSpan(statement.Span.End, 0), "\r\n" + indent + Call));
        _fontCount++;
    }

    private static bool IsComparison(BinaryExpressionSyntax b) => b.Kind() is
        SyntaxKind.LessThanExpression or SyntaxKind.LessThanOrEqualExpression or
        SyntaxKind.GreaterThanExpression or SyntaxKind.GreaterThanOrEqualExpression or
        SyntaxKind.EqualsExpression or SyntaxKind.NotEqualsExpression;

    private static bool IsCountAccess(ExpressionSyntax e)
    {
        e = Unwrap(e);
        return e is MemberAccessExpressionSyntax { Name.Identifier.ValueText: "Count" or "Length" or "Rank" };
    }

    private static ExpressionSyntax Unwrap(ExpressionSyntax e)
    {
        while (true)
        {
            switch (e)
            {
                case ParenthesizedExpressionSyntax p: e = p.Expression; continue;
                case CastExpressionSyntax c: e = c.Expression; continue;
                default: return e;
            }
        }
    }

    private void BanIdentifiers(ExpressionSyntax e, SemanticModel model)
    {
        foreach (SyntaxNode n in e.DescendantNodesAndSelf())
        {
            if (n is IdentifierNameSyntax id && Normalize(model.GetSymbolInfo(id).Symbol) is { } s)
            {
                _banned.Add(s);
            }
        }
    }

    private static ISymbol? ReturnOwner(ISymbol? enclosing)
    {
        if (enclosing is not IMethodSymbol method)
        {
            return null;
        }
        return method.MethodKind switch
        {
            MethodKind.PropertyGet => method.AssociatedSymbol,
            MethodKind.Ordinary or MethodKind.LocalFunction => method.OriginalDefinition,
            _ => null,
        };
    }

    private void AddDef(ISymbol symbol, ExpressionSyntax expr)
    {
        symbol = symbol.OriginalDefinition;
        if (!_defs.TryGetValue(symbol, out List<ExpressionSyntax>? list))
        {
            _defs[symbol] = list = new List<ExpressionSyntax>();
        }
        list.Add(expr);
    }

    private void IndexFont(ObjectCreationExpressionSyntax creation, SemanticModel model)
    {
        if (model.GetSymbolInfo(creation).Symbol is not IMethodSymbol ctor
            || !Eq.Equals(ctor.ContainingType, _font)
            || ctor.Parameters.Length == 0)
        {
            return;
        }
        // new Font(prototype, style) keeps the prototype's already scaled size.
        if (Eq.Equals(ctor.Parameters[0].Type, _font))
        {
            return;
        }
        ArgumentListSyntax? list = creation.ArgumentList;
        if (list == null || list.Arguments.Count < 2 || creation.Initializer != null)
        {
            return;
        }
        ExpressionSyntax size = list.Arguments[1].Expression;
        if (DerivesFromExistingFontSize(size, model))
        {
            // The base size is already scaled; only the fixed part added to it still needs scaling.
            _sinks.Add(size);
            Note($"font size derived from an existing font: {Where(creation)}  {size}");
            return;
        }
        var span = TextSpan.FromBounds(creation.NewKeyword.SpanStart, creation.Type.Span.End);
        AddChange(creation.SyntaxTree, new TextChange(span, "UiScale.NewFont"));
        _fontCount++;
    }

    private bool DerivesFromExistingFontSize(ExpressionSyntax size, SemanticModel model)
    {
        foreach (SyntaxNode n in size.DescendantNodesAndSelf())
        {
            if (n is MemberAccessExpressionSyntax access
                && access.Name.Identifier.ValueText is "Size" or "SizeInPoints" or "Height" or "GetHeight"
                && model.GetSymbolInfo(access).Symbol is { } member
                && Eq.Equals(member.ContainingType, _font))
            {
                return true;
            }
        }
        return false;
    }

    private void IndexAssignment(AssignmentExpressionSyntax assignment, SemanticModel model)
    {
        if (assignment.Kind() is not (SyntaxKind.SimpleAssignmentExpression
            or SyntaxKind.AddAssignmentExpression or SyntaxKind.SubtractAssignmentExpression))
        {
            return;
        }
        ISymbol? target = model.GetSymbolInfo(assignment.Left).Symbol;
        bool worldView = InWorldView(assignment);
        switch (target)
        {
            case IPropertySymbol property:
                if (!worldView && IsLayoutSink(property, assignment, model))
                {
                    _sinks.Add(assignment.Right);
                }
                else if (!worldView && IsGeometry(property.ContainingType)
                    && assignment.Left is MemberAccessExpressionSyntax memberAccess
                    && Normalize(model.GetSymbolInfo(memberAccess.Expression).Symbol) is { } receiver)
                {
                    _conditionalSinks.Add((receiver, assignment.Right));
                }
                if (InSource(property))
                {
                    AddDef(property, assignment.Right);
                }
                break;
            case IFieldSymbol field:
                if (!worldView && IsGeometry(field.ContainingType)
                    && assignment.Left is MemberAccessExpressionSyntax fieldAccess
                    && Normalize(model.GetSymbolInfo(fieldAccess.Expression).Symbol) is { } fieldReceiver)
                {
                    _conditionalSinks.Add((fieldReceiver, assignment.Right));
                }
                if (InSource(field))
                {
                    AddDef(field, assignment.Right);
                }
                break;
            case ILocalSymbol or IParameterSymbol:
                AddDef(target, assignment.Right);
                break;
        }
    }

    private bool IsLayoutSink(IPropertySymbol property, AssignmentExpressionSyntax assignment, SemanticModel model)
    {
        if (!LayoutProperties.Contains(property.Name))
        {
            return false;
        }
        INamedTypeSymbol owner = property.ContainingType;
        bool isControl = DerivesFrom(owner, _control);
        if (!isControl && !DerivesFrom(owner, _toolStripItem) && !DerivesFrom(owner, _gridBand)
            && !DerivesFrom(owner, _columnHeader) && !(Eq.Equals(owner, _cellStyle) && property.Name == "Padding"))
        {
            return false;
        }
        MemberFlags flags = FlagsFor(assignment);
        if (flags.AutoScaledInit && isControl && ScaledWithContainer.Contains(property.Name))
        {
            return false;
        }
        // ListView column widths of -1 / -2 mean "auto size".
        if (DerivesFrom(owner, _columnHeader) && assignment.Right is PrefixUnaryExpressionSyntax)
        {
            return false;
        }
        // The game windows themselves follow the screen, not the UI scale.
        if (isControl && OwnWindowProperties.Contains(property.Name) && IsOwnMember(assignment.Left)
            && model.GetEnclosingSymbol(assignment.SpanStart)?.ContainingType is { Name: "Main" or "Start" } type
            && DerivesFrom(type, _form))
        {
            return false;
        }
        return true;
    }

    private static bool IsOwnMember(ExpressionSyntax left) => left is IdentifierNameSyntax
        || left is MemberAccessExpressionSyntax { Expression: ThisExpressionSyntax or BaseExpressionSyntax };

    private void IndexArgument(ArgumentSyntax argument, SemanticModel model)
    {
        if (!argument.RefKindKeyword.IsKind(SyntaxKind.None))
        {
            // `ref y` shares one variable between caller and callee: a running row position, typically.
            if (model.GetOperation(argument) is IArgumentOperation { Parameter: { } shared }
                && shared.ContainingSymbol is IMethodSymbol sharedMethod && InSource(sharedMethod)
                && Normalize(model.GetSymbolInfo(argument.Expression).Symbol) is { } variable)
            {
                _aliases.Add((shared.OriginalDefinition, variable));
            }
            return;
        }
        if (model.GetOperation(argument) is not IArgumentOperation { Parameter: { } parameter })
        {
            return;
        }
        if (parameter.ContainingSymbol is not IMethodSymbol method)
        {
            return;
        }
        if (InSource(method))
        {
            if (method.ContainingType?.Name != "UiScale")
            {
                AddDef(parameter.OriginalDefinition, argument.Expression);
            }
            return;
        }
        if (CountParameterNames.Contains(parameter.Name))
        {
            BanIdentifiers(argument.Expression, model);
        }
        if (InWorldView(argument))
        {
            return;
        }
        if (IsFrameworkSink(method, parameter, argument))
        {
            _sinks.Add(argument.Expression);
        }
        else if (IsGeometry(method.ContainingType) && method.Name is "Inflate" or "Offset" && !method.IsStatic
            && argument.Parent?.Parent is InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax call }
            && Normalize(model.GetSymbolInfo(call.Expression).Symbol) is { } receiver)
        {
            _conditionalSinks.Add((receiver, argument.Expression));
        }
    }

    private bool IsFrameworkSink(IMethodSymbol method, IParameterSymbol parameter, ArgumentSyntax argument)
    {
        if (!IsPixelType(parameter.Type) || NonPixelParameterNames.Contains(parameter.Name)
            || parameter.Name.StartsWith("src", StringComparison.Ordinal))
        {
            return false;
        }
        INamedTypeSymbol owner = method.ContainingType;
        MemberFlags flags = FlagsFor(argument);
        if (method.MethodKind == MethodKind.Constructor)
        {
            if (Eq.Equals(owner, _bitmap))
            {
                return flags.TextBitmap && parameter.Name is "width" or "height";
            }
            if (Eq.Equals(owner, _gradientBrush) || Eq.Equals(owner, _region))
            {
                return !flags.SpriteMethod;
            }
            if (Eq.Equals(owner, _columnStyle) || Eq.Equals(owner, _rowStyle))
            {
                return !flags.AutoScaledInit && parameter.Name is "width" or "height"
                    && argument.Parent is ArgumentListSyntax list && list.Arguments[0].ToString().Contains("Absolute");
            }
            return false;
        }
        if (Eq.Equals(owner, _graphics))
        {
            if (flags.SpriteMethod)
            {
                return false;
            }
            string name = method.Name;
            return name.StartsWith("Draw", StringComparison.Ordinal) || name.StartsWith("Fill", StringComparison.Ordinal)
                || name.StartsWith("Measure", StringComparison.Ordinal)
                || name is "SetClip" or "IntersectClip" or "ExcludeClip" or "TranslateTransform" or "IsVisible";
        }
        if (Eq.Equals(owner, _textRenderer) || Eq.Equals(owner, _controlPaint))
        {
            return true;
        }
        if (Eq.Equals(owner, _graphicsPath))
        {
            return method.Name.StartsWith("Add", StringComparison.Ordinal) && !flags.SpriteMethod;
        }
        if (Eq.Equals(owner, _stringFormat))
        {
            return method.Name == "SetTabStops";
        }
        if (DerivesFrom(owner, _control))
        {
            return method.Name is "SetBounds" or "Invalidate" or "SetAutoScrollMargin" && !flags.AutoScaledInit;
        }
        return false;
    }

    private sealed record MemberFlags(bool AutoScaledInit, bool SpriteMethod, bool TextBitmap);

    private static readonly MemberFlags NoFlags = new(false, false, false);

    private MemberFlags FlagsFor(SyntaxNode node)
    {
        SyntaxNode? member = node.Ancestors().FirstOrDefault(a =>
            a is BaseMethodDeclarationSyntax or AccessorDeclarationSyntax or LocalFunctionStatementSyntax);
        if (member == null)
        {
            return NoFlags;
        }
        // A local function shares its container's drawing context.
        while (member is LocalFunctionStatementSyntax
            && member.Ancestors().FirstOrDefault(a => a is BaseMethodDeclarationSyntax or AccessorDeclarationSyntax) is { } outer)
        {
            member = outer;
        }
        if (_memberFlags.TryGetValue(member, out MemberFlags? flags))
        {
            return flags;
        }

        bool fromImage = false, text = false, controlSizedBitmap = false, autoScaleDimensions = false;
        foreach (SyntaxNode n in member.DescendantNodes())
        {
            switch (n)
            {
                case InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax call }:
                    string name = call.Name.Identifier.ValueText;
                    fromImage |= name == "FromImage";
                    text |= name is "DrawString" or "DrawText" or "MeasureString" or "MeasureText";
                    break;
                case ObjectCreationExpressionSyntax { ArgumentList: not null } creation
                    when creation.Type.ToString().EndsWith("Bitmap", StringComparison.Ordinal):
                    string arguments = creation.ArgumentList.ToString();
                    controlSizedBitmap |= arguments.Contains("Width") || arguments.Contains("Height") || arguments.Contains("Size");
                    break;
                case AssignmentExpressionSyntax assignment
                    when assignment.Left.ToString().EndsWith("AutoScaleDimensions", StringComparison.Ordinal):
                    autoScaleDimensions = true;
                    break;
            }
        }
        bool isInitializeComponent = member is MethodDeclarationSyntax { Identifier.ValueText: "InitializeComponent" };
        flags = new MemberFlags(
            AutoScaledInit: isInitializeComponent && autoScaleDimensions,
            SpriteMethod: fromImage && !text && !controlSizedBitmap,
            TextBitmap: fromImage && text);
        _memberFlags[member] = flags;
        return flags;
    }

    // ---------------------------------------------------------------- visiting

    private void Visit(ExpressionSyntax e)
    {
        switch (e)
        {
            case ParenthesizedExpressionSyntax p:
                Visit(p.Expression);
                return;
            case CastExpressionSyntax c:
                Visit(c.Expression);
                return;
            case PrefixUnaryExpressionSyntax u
                when u.IsKind(SyntaxKind.UnaryMinusExpression) || u.IsKind(SyntaxKind.UnaryPlusExpression):
                if (IsNumericLiteral(u.Operand))
                {
                    Mark(u);
                }
                else
                {
                    Visit(u.Operand);
                }
                return;
            case LiteralExpressionSyntax literal when IsNumericLiteral(literal):
                Mark(literal);
                return;
            case BinaryExpressionSyntax b:
                VisitBinary(b);
                return;
            case ConditionalExpressionSyntax conditional:
                Visit(conditional.WhenTrue);
                Visit(conditional.WhenFalse);
                return;
            case ObjectCreationExpressionSyntax creation:
                if (creation.ArgumentList != null && M(creation).GetTypeInfo(creation).Type is INamedTypeSymbol created
                    && IsGeometry(created))
                {
                    foreach (ArgumentSyntax a in creation.ArgumentList.Arguments)
                    {
                        Visit(a.Expression);
                    }
                }
                return;
            case ImplicitObjectCreationExpressionSyntax implicitCreation:
                if (M(implicitCreation).GetTypeInfo(implicitCreation).Type is INamedTypeSymbol implicitType
                    && IsGeometry(implicitType))
                {
                    foreach (ArgumentSyntax a in implicitCreation.ArgumentList.Arguments)
                    {
                        Visit(a.Expression);
                    }
                }
                return;
            case ArrayCreationExpressionSyntax { Initializer: not null } array:
                VisitAll(array.Initializer.Expressions);
                return;
            case ImplicitArrayCreationExpressionSyntax implicitArray:
                VisitAll(implicitArray.Initializer.Expressions);
                return;
            case InitializerExpressionSyntax initializer:
                VisitAll(initializer.Expressions);
                return;
            case InvocationExpressionSyntax invocation:
                VisitInvocation(invocation);
                return;
            case IdentifierNameSyntax or MemberAccessExpressionSyntax:
                VisitReference(e);
                return;
        }
    }

    private void VisitAll(IEnumerable<ExpressionSyntax> expressions)
    {
        foreach (ExpressionSyntax e in expressions)
        {
            Visit(e);
        }
    }

    private void VisitBinary(BinaryExpressionSyntax b)
    {
        if (IsLiteralArithmetic(b))
        {
            Mark(b);
            return;
        }
        switch (b.Kind())
        {
            case SyntaxKind.AddExpression:
            case SyntaxKind.SubtractExpression:
            case SyntaxKind.CoalesceExpression:
                Visit(b.Left);
                Visit(b.Right);
                return;
            case SyntaxKind.DivideExpression:
                if (IsLiteralValue(b.Left))
                {
                    if (IsCountLike(b.Right))
                    {
                        Visit(b.Left);
                    }
                }
                else
                {
                    Visit(b.Left);
                }
                return;
            case SyntaxKind.MultiplyExpression:
                VisitProduct(b);
                return;
        }
    }

    // In `a * b` exactly one side carries the pixels; the other is a count or a ratio.
    private void VisitProduct(BinaryExpressionSyntax b)
    {
        bool leftLiteral = IsLiteralValue(b.Left), rightLiteral = IsLiteralValue(b.Right);
        bool leftPixel = !leftLiteral && IsPixelish(b.Left), rightPixel = !rightLiteral && IsPixelish(b.Right);
        if (leftLiteral != rightLiteral)
        {
            ExpressionSyntax literal = leftLiteral ? b.Left : b.Right;
            ExpressionSyntax other = leftLiteral ? b.Right : b.Left;
            if ((leftLiteral ? rightPixel : leftPixel) || IsNumericConstant(other))
            {
                // `width * 2`, `margin * 2`: the literal is a ratio.
                Visit(other);
            }
            else if (IsCountLike(other))
            {
                // `i * 20`: the literal is the pixel size of one item.
                Visit(literal);
            }
            else if (_marking)
            {
                Note($"ambiguous product left unscaled: {Where(b)}  {b}");
            }
            return;
        }
        if (leftPixel || rightPixel)
        {
            if (leftPixel)
            {
                Visit(b.Left);
            }
            if (rightPixel)
            {
                Visit(b.Right);
            }
            return;
        }
        bool leftCount = IsCountLike(b.Left), rightCount = IsCountLike(b.Right);
        if (leftCount && !rightCount)
        {
            Visit(b.Right);
        }
        else if (rightCount && !leftCount)
        {
            Visit(b.Left);
        }
        else if (_marking)
        {
            Note($"ambiguous product left unscaled: {Where(b)}  {b}");
        }
    }

    private bool IsCountLike(ExpressionSyntax e)
    {
        e = Unwrap(e);
        if (IsCountAccess(e))
        {
            return true;
        }
        if (e is IdentifierNameSyntax or MemberAccessExpressionSyntax)
        {
            ISymbol? symbol = Normalize(M(e).GetSymbolInfo(e).Symbol);
            return symbol != null && (_banned.Contains(symbol) || IsAssignedFromCount(symbol));
        }
        // `count - 1`, `rows + 2`
        if (e is BinaryExpressionSyntax b && b.Kind() is SyntaxKind.AddExpression or SyntaxKind.SubtractExpression)
        {
            return (IsCountLike(b.Left) && IsLiteralValue(b.Right)) || (IsLiteralValue(b.Left) && IsCountLike(b.Right));
        }
        return false;
    }

    // A named constant such as `const int margin = 8`, which is scaled where it is used.
    private bool IsNumericConstant(ExpressionSyntax e)
    {
        e = Unwrap(e);
        return e is IdentifierNameSyntax or MemberAccessExpressionSyntax
            && M(e).GetSymbolInfo(e).Symbol is IFieldSymbol { IsConst: true } or ILocalSymbol { IsConst: true };
    }

    private readonly Dictionary<ISymbol, bool> _assignedFromCount = new(Eq);

    // `int rows = list.Count;` makes rows a count even though it is never used as an index.
    private bool IsAssignedFromCount(ISymbol symbol)
    {
        if (!_assignedFromCount.TryGetValue(symbol, out bool result))
        {
            result = SymbolType(symbol)?.SpecialType == SpecialType.System_Int32
                && _defs.TryGetValue(symbol, out List<ExpressionSyntax>? defs)
                && defs.Any(d => d.DescendantNodesAndSelf().OfType<ExpressionSyntax>().Any(IsCountAccess));
            _assignedFromCount[symbol] = result;
        }
        return result;
    }

    private static readonly HashSet<string> WorldViewTypes = new() { "MainView", "BaconMainView", "LightningGenerator" };

    // The main view renders the game world, which follows the zoom level and not the UI scale.
    private static bool InWorldView(SyntaxNode node) => node.Ancestors().OfType<TypeDeclarationSyntax>()
        .Any(t => WorldViewTypes.Contains(t.Identifier.ValueText));

    private void VisitInvocation(InvocationExpressionSyntax invocation)
    {
        if (M(invocation).GetSymbolInfo(invocation).Symbol is not IMethodSymbol method)
        {
            return;
        }
        if (method.ContainingType?.Name == "UiScale")
        {
            return;
        }
        if (!InSource(method))
        {
            if (PassThroughMethods.Contains(method.Name) && method.ContainingType?.Name is "Math" or "MathF" or "Convert")
            {
                int take = method.Name == "Round" ? 1 : int.MaxValue;
                VisitAll(invocation.ArgumentList.Arguments.Take(take).Select(a => a.Expression));
            }
            return;
        }
        if (IsPixelType(method.ReturnType))
        {
            Taint(method.OriginalDefinition);
        }
    }

    private void VisitReference(ExpressionSyntax e)
    {
        ISymbol? raw = M(e).GetSymbolInfo(e).Symbol;
        if (raw == null)
        {
            return;
        }
        // rect.Width, point.X: the struct holds the pixels.
        if (!InSource(raw) && e is MemberAccessExpressionSyntax access && IsGeometry(raw.ContainingType)
            && PixelMemberNames.Contains(raw.Name))
        {
            Visit(access.Expression);
            return;
        }
        ISymbol? symbol = Normalize(raw);
        if (symbol == null || !InSource(symbol))
        {
            return;
        }
        bool isConst = symbol is IFieldSymbol { IsConst: true } or ILocalSymbol { IsConst: true };
        if (isConst)
        {
            if (IsNumeric(SymbolType(symbol)))
            {
                Mark(e);
            }
            return;
        }
        Taint(symbol);
    }

    private void Taint(ISymbol symbol)
    {
        symbol = symbol.OriginalDefinition;
        if (_pixel.Contains(symbol) || !InSource(symbol)
            || symbol.ContainingType?.Name == "UiScale" || !IsPixelType(SymbolType(symbol)))
        {
            return;
        }
        // In the world view only locals may carry pixels; its fields hold camera and world state.
        if (symbol is not ILocalSymbol && WorldViewTypes.Contains(symbol.ContainingType?.Name ?? ""))
        {
            return;
        }
        if (_banned.Contains(symbol))
        {
            NoteSkipped("used as index or counter", symbol);
            return;
        }
        if (IsLogicFed(symbol))
        {
            NoteSkipped("fed by game data", symbol);
            return;
        }
        _pixel.Add(symbol);
        _changed = true;
        if (symbol is IParameterSymbol { ContainingSymbol: IMethodSymbol { OverriddenMethod: { } overridden } } parameter
            && parameter.Ordinal < overridden.Parameters.Length)
        {
            Taint(overridden.Parameters[parameter.Ordinal]);
        }
    }

    // Records a layout symbol that was left alone although fixed numbers are stored in it,
    // so the report lists the places most likely to need a manual look.
    private void NoteSkipped(string reason, ISymbol symbol)
    {
        if (!_marking || !_defs.TryGetValue(symbol, out List<ExpressionSyntax>? defs))
        {
            return;
        }
        ExpressionSyntax? withLiteral = defs.FirstOrDefault(d => d.DescendantNodesAndSelf().Any(n =>
            n is LiteralExpressionSyntax literal && literal.IsKind(SyntaxKind.NumericLiteralExpression)
            && literal.Token.Value is IConvertible value && Math.Abs(value.ToDouble(null)) >= 3.0));
        if (withLiteral != null)
        {
            Note($"skipped ({reason}): {Where(withLiteral)}  {symbol.ContainingType?.Name}.{symbol.Name} = {withLiteral}");
        }
    }

    // True when any value stored in the symbol comes from another class's game data.
    private bool IsLogicFed(ISymbol symbol)
    {
        if (_logicFed.TryGetValue(symbol, out bool known))
        {
            return known;
        }
        bool result = false;
        INamedTypeSymbol? home = symbol.ContainingType;
        if (_defs.TryGetValue(symbol, out List<ExpressionSyntax>? defs))
        {
            foreach (ExpressionSyntax def in defs)
            {
                SemanticModel model = M(def);
                // What is passed into a call (text to measure, say) is not the value being stored.
                foreach (SyntaxNode n in def.DescendantNodesAndSelf(
                    node => node is not ArgumentListSyntax { Parent: InvocationExpressionSyntax }))
                {
                    if (n is not (IdentifierNameSyntax or MemberAccessExpressionSyntax))
                    {
                        continue;
                    }
                    ISymbol? referenced = model.GetSymbolInfo(n).Symbol;
                    if (referenced is IFieldSymbol or IPropertySymbol or IMethodSymbol
                        && IsPixelType(SymbolType(referenced))
                        && referenced.ContainingType is { Name: not "GraphicsHelper" } owner
                        && !Eq.Equals(owner.OriginalDefinition, home?.OriginalDefinition)
                        && owner.ContainingNamespace?.ToDisplayString().StartsWith("DistantWorlds.Types", StringComparison.Ordinal) == true)
                    {
                        result = true;
                        break;
                    }
                }
                if (result)
                {
                    break;
                }
            }
        }
        _logicFed[symbol] = result;
        return result;
    }

    private bool IsPixelish(ExpressionSyntax e)
    {
        SemanticModel model = M(e);
        foreach (SyntaxNode n in e.DescendantNodesAndSelf())
        {
            switch (n)
            {
                case InvocationExpressionSyntax invocation:
                    string? called = invocation.Expression switch
                    {
                        MemberAccessExpressionSyntax m => m.Name.Identifier.ValueText,
                        IdentifierNameSyntax i => i.Identifier.ValueText,
                        _ => null,
                    };
                    if (called != null && PixelMethodNames.Contains(called))
                    {
                        return true;
                    }
                    if (called == "S" && invocation.Expression.ToString() == "UiScale.S")
                    {
                        return true;
                    }
                    break;
                case IdentifierNameSyntax or MemberAccessExpressionSyntax:
                    ISymbol? raw = model.GetSymbolInfo(n).Symbol;
                    if (raw == null)
                    {
                        break;
                    }
                    if (!InSource(raw) && raw is IPropertySymbol or IFieldSymbol && PixelMemberNames.Contains(raw.Name))
                    {
                        return true;
                    }
                    if (Normalize(raw) is { } symbol && _pixel.Contains(symbol.OriginalDefinition))
                    {
                        return true;
                    }
                    break;
            }
        }
        return false;
    }

    // ---------------------------------------------------------------- marking

    // What Control.Scale changes on a control. Everything else (row heights, column widths,
    // a control's own size properties) has to be scaled in the source even in a scaled container.
    private static readonly HashSet<string> ScaledWithContainer = new()
    {
        "Location", "Size", "Bounds", "Width", "Height", "Left", "Top", "ClientSize",
        "MinimumSize", "MaximumSize", "Margin", "Padding", "SplitterDistance",
    };

    private bool IsScaledWithContainer(SyntaxNode node)
    {
        foreach (SyntaxNode ancestor in node.Ancestors())
        {
            if (ancestor is AssignmentExpressionSyntax assignment && assignment.Right.Span.Contains(node.Span))
            {
                return M(assignment).GetSymbolInfo(assignment.Left).Symbol is IPropertySymbol property
                    && ScaledWithContainer.Contains(property.Name) && DerivesFrom(property.ContainingType, _control);
            }
            if (ancestor is StatementSyntax)
            {
                break;
            }
        }
        return false;
    }

    private void Mark(ExpressionSyntax node)
    {
        if (!_marking || !_marked.Add(node))
        {
            return;
        }
        // Control bounds in this designer layout are scaled as a whole by UiScale.ScaleLayout.
        if (FlagsFor(node).AutoScaledInit && IsScaledWithContainer(node))
        {
            return;
        }
        SemanticModel model = M(node);
        Optional<object?> constant = model.GetConstantValue(node);
        if (constant.HasValue && constant.Value is IConvertible value)
        {
            try
            {
                if (value.ToDouble(null) == 0.0)
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        TypeInfo type = model.GetTypeInfo(node);
        if (!IsNumeric(type.Type) || (type.ConvertedType != null && !IsNumeric(type.ConvertedType)
            && type.ConvertedType.SpecialType != SpecialType.System_Int64
            && type.ConvertedType.SpecialType != SpecialType.System_Object))
        {
            return;
        }
        if (NeedsConstant(node))
        {
            return;
        }
        AddChange(node.SyntaxTree, new TextChange(new TextSpan(node.SpanStart, 0), "UiScale.S("));
        AddChange(node.SyntaxTree, new TextChange(new TextSpan(node.Span.End, 0), ")"));
        _literalCount++;
    }

    private static bool NeedsConstant(SyntaxNode node)
    {
        foreach (SyntaxNode a in node.Ancestors())
        {
            switch (a)
            {
                case AttributeArgumentSyntax or CaseSwitchLabelSyntax or EnumMemberDeclarationSyntax or ParameterSyntax
                    or CasePatternSwitchLabelSyntax or ConstantPatternSyntax:
                    return true;
                case LocalDeclarationStatementSyntax local when local.Modifiers.Any(SyntaxKind.ConstKeyword):
                    return true;
                case FieldDeclarationSyntax field when field.Modifiers.Any(SyntaxKind.ConstKeyword):
                    return true;
                case StatementSyntax or MemberDeclarationSyntax:
                    return false;
            }
        }
        return false;
    }

    private void AddChange(SyntaxTree tree, TextChange change)
    {
        if (!_changes.TryGetValue(tree, out List<TextChange>? list))
        {
            _changes[tree] = list = new List<TextChange>();
        }
        list.Add(change);
    }

    // ---------------------------------------------------------------- helpers

    private static bool IsNumericLiteral(ExpressionSyntax e)
        => e is LiteralExpressionSyntax literal && literal.IsKind(SyntaxKind.NumericLiteralExpression);

    private static bool IsLiteralValue(ExpressionSyntax e)
    {
        e = Unwrap(e);
        if (e is PrefixUnaryExpressionSyntax u && u.IsKind(SyntaxKind.UnaryMinusExpression))
        {
            e = Unwrap(u.Operand);
        }
        return IsNumericLiteral(e) || (e is BinaryExpressionSyntax b && IsLiteralArithmetic(b));
    }

    private static bool IsLiteralArithmetic(BinaryExpressionSyntax b)
    {
        if (b.Kind() is not (SyntaxKind.AddExpression or SyntaxKind.SubtractExpression
            or SyntaxKind.MultiplyExpression or SyntaxKind.DivideExpression))
        {
            return false;
        }
        return IsLiteralValue(b.Left) && IsLiteralValue(b.Right);
    }

    private ISymbol? Normalize(ISymbol? symbol)
    {
        switch (symbol)
        {
            case IParameterSymbol { ContainingSymbol: IMethodSymbol { MethodKind: MethodKind.PropertySet, AssociatedSymbol: { } property } }:
                return property.OriginalDefinition;
            case ILocalSymbol or IParameterSymbol or IFieldSymbol or IPropertySymbol:
                return symbol.OriginalDefinition;
            default:
                return null;
        }
    }

    private static ITypeSymbol? SymbolType(ISymbol symbol) => symbol switch
    {
        ILocalSymbol l => l.Type,
        IParameterSymbol p => p.Type,
        IFieldSymbol f => f.Type,
        IPropertySymbol p => p.Type,
        IMethodSymbol m => m.ReturnType,
        _ => null,
    };

    private static bool IsNumeric(ITypeSymbol? type) => type?.SpecialType is
        SpecialType.System_Int32 or SpecialType.System_Single or SpecialType.System_Double;

    private bool IsGeometry(ITypeSymbol? type) => type is INamedTypeSymbol named && _geometry.Contains(named);

    private bool IsPixelType(ITypeSymbol? type)
    {
        if (type is IArrayTypeSymbol array)
        {
            type = array.ElementType;
        }
        return IsNumeric(type) || IsGeometry(type);
    }

    private static bool InSource(ISymbol symbol) => symbol.Locations.Any(l => l.IsInSource);

    private static bool DerivesFrom(ITypeSymbol? type, INamedTypeSymbol? ancestor)
    {
        if (ancestor == null)
        {
            return false;
        }
        for (ITypeSymbol? t = type; t != null; t = t.BaseType)
        {
            if (Eq.Equals(t.OriginalDefinition, ancestor))
            {
                return true;
            }
        }
        return false;
    }

    private static string Where(SyntaxNode node)
    {
        FileLinePositionSpan span = node.GetLocation().GetLineSpan();
        return $"{Path.GetFileName(span.Path)}:{span.StartLinePosition.Line + 1}";
    }

    private void Note(string text)
    {
        if (!_notes.Contains(text))
        {
            _notes.Add(text);
        }
    }

    // ---------------------------------------------------------------- output

    public void WriteResults(bool apply, string? reportPath)
    {
        var report = new StringBuilder();
        report.AppendLine($"fonts: {_fontCount}, layout values: {_literalCount}, pixel symbols: {_pixel.Count}");
        report.AppendLine();
        report.AppendLine("== changes per file ==");
        foreach ((SyntaxTree tree, List<TextChange> changes) in _changes.OrderByDescending(c => c.Value.Count))
        {
            report.AppendLine($"{changes.Count,6}  {tree.FilePath}");
        }
        report.AppendLine();
        report.AppendLine("== notes ==");
        foreach (string note in _notes)
        {
            report.AppendLine(note);
        }
        report.AppendLine();
        report.AppendLine("== pixel symbols ==");
        foreach (string line in _pixel.Select(s => $"{s.Kind,-10} {s.ContainingType?.Name}.{s.ContainingSymbol?.Name}.{s.Name}").OrderBy(s => s))
        {
            report.AppendLine(line);
        }
        if (reportPath != null)
        {
            File.WriteAllText(reportPath, report.ToString());
            Console.WriteLine($"Report written to {reportPath}");
        }

        if (!apply)
        {
            Console.WriteLine($"Dry run: {_changes.Count} files would change. Pass --apply to write them.");
            return;
        }
        foreach ((SyntaxTree tree, List<TextChange> changes) in _changes)
        {
            SourceText text = tree.GetText();
            // Insertions at the same position must keep their order, so sort stably by position.
            List<TextChange> ordered = changes.Select((c, i) => (c, i))
                .OrderBy(x => x.c.Span.Start).ThenBy(x => x.c.Span.Length == 0 && x.c.NewText == ")" ? 0 : 1).ThenBy(x => x.i)
                .Select(x => x.c).ToList();
            SourceText updated = text.WithChanges(ordered);
            using var writer = new StreamWriter(tree.FilePath, append: false, text.Encoding ?? new UTF8Encoding(false));
            updated.Write(writer);
        }
        Console.WriteLine($"Rewrote {_changes.Count} files.");
    }
}
