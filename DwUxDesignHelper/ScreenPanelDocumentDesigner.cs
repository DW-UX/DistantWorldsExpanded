using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using Microsoft.DotNet.DesignTools.Designers;

namespace DistantWorlds.Controls.Design;

[ToolboxItemFilter("DistantWorlds.Controls.ScreenPanel", ToolboxItemFilterType.Allow)]
internal class ScreenPanelDocumentDesigner : DocumentDesigner {

  static ScreenPanelDocumentDesigner() {
    Info("ScreenPanelDocumentDesigner class initialized");
    LicenseManager.CurrentContext = new DwUxLicenseContext(LicenseManager.CurrentContext);
  }

  public ScreenPanelDocumentDesigner() {
    AutoResizeHandles = true;
    Info("ScreenPanelDocumentDesigner constructed");
    LicenseManager.CurrentContext = new DwUxLicenseContext(LicenseManager.CurrentContext);
  }

  private Size Size {
    get => Control.ClientSize;
    set => Control.ClientSize = value;
  }

  public override IReadOnlyCollection<IComponent> AssociatedComponents {
    get {
      Info("ScreenPanelDocumentDesigner AssociatedComponents");
      var old = base.AssociatedComponents;
      if (old == null)
        return NoComponents;

      var body = old.OfType<Control>().FirstOrDefault(c => c is { Name: "pnlBody" });

      if (body == null)
        return NoComponents;

      List<IComponent>? children = null;

      Info("Filtered:");
      foreach (IComponent component in body.Controls) {
        if (component is Control control)
          Info(control.Name);
        else
          Warn($"{component.GetType().FullName}: {component}");

        (children ??= new()).Add(component);
      }

      return children
        ?? NoComponents;
    }
  }

  private static IReadOnlyCollection<IComponent> NoComponents
    => Array.Empty<IComponent>();

  protected override void PreFilterProperties(IDictionary properties) {
    Info("ScreenPanelDocumentDesigner PreFilterProperties");

    // allow the base class to filter first
    base.PreFilterProperties(properties);

    // Handle shadowed properties
    string[] shadowProps = { "Size" };

    var empty = Array.Empty<Attribute>();

    for (var i = 0; i < shadowProps.Length; i++) {
      if (properties[shadowProps[i]] is PropertyDescriptor prop)
        properties[shadowProps[i]] = TypeDescriptor.CreateProperty
          (typeof(ScreenPanelDocumentDesigner), prop, empty);
    }
  }

  public override void Initialize(IComponent component) {
    Info("ScreenPanelDocumentDesigner Initialize");
    base.Initialize(component);
  }

  public override void InitializeExistingComponent(IDictionary? defaultValues) {
    Info("ScreenPanelDocumentDesigner InitializeExistingComponent");
    base.InitializeExistingComponent(defaultValues);
  }

  public override void InitializeNewComponent(IDictionary? defaultValues) {
    Info("ScreenPanelDocumentDesigner InitializeNewComponent");
    base.InitializeNewComponent(defaultValues);
  }

  protected override void Dispose(bool disposing) {
    Info("ScreenPanelDocumentDesigner disposing");
    base.Dispose(disposing);
  }

}

internal class DwUxLicenseContext : LicenseContext {

  private readonly LicenseContext? _prevCtx;

  public DwUxLicenseContext(LicenseContext? prevCtx) {
    _prevCtx = prevCtx;
  }

  public override LicenseUsageMode UsageMode
    => LicenseUsageMode.Designtime;

  public override string? GetSavedLicenseKey(Type type, Assembly? resourceAssembly)
    => _prevCtx is null
      ? base.GetSavedLicenseKey(type, resourceAssembly)
      : _prevCtx.GetSavedLicenseKey(type, resourceAssembly);

  public override void SetSavedLicenseKey(Type type, string key) {
    if (_prevCtx is null)
      base.SetSavedLicenseKey(type, key);
    else
      _prevCtx.SetSavedLicenseKey(type, key);
  }

  public override object? GetService(Type type)
    => _prevCtx is null
      ? base.GetService(type)
      : _prevCtx.GetService(type);

}