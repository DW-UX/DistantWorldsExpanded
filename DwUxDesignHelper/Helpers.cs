using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using static DistantWorlds.Controls.Design.Logging;

namespace DistantWorlds.Controls.Design;

internal static class Helpers {

  static Helpers() {
    AppDomain.CurrentDomain.FirstChanceException += (sender, args) => {
      Warn($"Unhandled Exception\n{args.Exception.ToStringDemystified()}");
    };
    AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
      Error($"Unhandled Exception\n{((Exception)args.ExceptionObject).ToStringDemystified()}");
    };
    TaskScheduler.UnobservedTaskException += (sender, args) => {
      Error($"Unobserved Task Exception\n{args.Exception.ToStringDemystified()}");
    };
  }

  public static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

  public static void Debug(FormattableString message)
    => LogWriteLine(LogFormat(message, LogLevel.Debug), LogLevel.Debug);

  public static void Debug(string message, params object[] args)
    => LogWriteLine(LogFormat(FormattableStringFactory.Create(message, args), LogLevel.Debug), LogLevel.Debug);

  public static void Verbose(FormattableString message)
    => LogWriteLine(LogFormat(message, LogLevel.Verbose), LogLevel.Verbose);

  public static void Verbose(string message, params object[] args)
    => LogWriteLine(LogFormat(FormattableStringFactory.Create(message, args), LogLevel.Verbose), LogLevel.Verbose);

  public static void Info(FormattableString message)
    => LogWriteLine(LogFormat(message));

  public static void Info(string message, params object[] args)
    => LogWriteLine(LogFormat(FormattableStringFactory.Create(message, args)));

  public static void Warn(FormattableString message)
    => LogWriteLine(LogFormat(message, LogLevel.Warning), LogLevel.Warning);

  public static void Warn(string message, params object[] args)
    => LogWriteLine(LogFormat(FormattableStringFactory.Create(message, args), LogLevel.Warning), LogLevel.Warning);

  public static void Error(FormattableString message)
    => LogWriteLine(LogFormat(message, LogLevel.Error), LogLevel.Error);

  public static void Error(string message, params object[] args)
    => LogWriteLine(LogFormat(FormattableStringFactory.Create(message, args), LogLevel.Error), LogLevel.Error);

  public static void Fatal(FormattableString message)
    => LogWriteLine(LogFormat(message, LogLevel.Fatal), LogLevel.Fatal);

  public static void Fatal(string message, params object[] args)
    => LogWriteLine(LogFormat(FormattableStringFactory.Create(message, args), LogLevel.Fatal), LogLevel.Fatal);

}