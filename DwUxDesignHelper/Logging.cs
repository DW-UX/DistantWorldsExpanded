using System.Diagnostics;
using System.Text;

namespace DistantWorlds.Controls.Design;

internal static class Logging {

#if LOG_TO_FILE
  private static readonly FileInfo LogFile = new(Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile, Environment.SpecialFolderOption.DoNotVerify),
    "DwUxDesignHelper.log"));

  private static readonly FileStream LogFileStream = LogFile.Open
  (new FileStreamOptions {
    Access = FileAccess.Write,
    Mode = FileMode.Append,
    BufferSize = 4096,
    Options = FileOptions.SequentialScan | FileOptions.Asynchronous,
    Share = FileShare.ReadWrite | FileShare.Delete
  });

  private static readonly StreamWriter LogWriter = new(LogFileStream, Encoding.UTF8, 4096, true);
#else
  private static readonly TextWriter LogWriter = Console.Out;
#endif

  [ThreadStatic]
  private static StringBuilder? _logSb;

  internal static void LogWriteLine(StringBuilder message, LogLevel level = LogLevel.Info) {
    Console.Out.WriteLine(message);
    LogWriter.WriteLine(message);
    LogWriter.Flush();
#if LOG_TO_FILE
    LogFileStream.Flush();
#endif
    message.Clear();
  }

  internal static StringBuilder LogFormat(FormattableString message, LogLevel level = LogLevel.Info) {
    var sb = _logSb ??= new();
#if LOG_TO_FILE
    sb.AppendFormat(Helpers.InvariantCulture, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {LogLevelToString(level)} ");
#else
    sb.AppendFormat(Helpers.InvariantCulture, $"{LogLevelToString(level)} ");
#endif
    sb.AppendFormat(Helpers.InvariantCulture, message.Format, message.GetArguments());
    return sb;
  }

  private static string LogLevelToString(LogLevel level) => level switch {
    LogLevel.Debug => "DBG",
    LogLevel.Verbose => "VRB",
    LogLevel.Info => "IFO",
    LogLevel.Warning => "WRN",
    LogLevel.Error => "ERR",
    LogLevel.Fatal => "FTL",
    _ => ((int)level).ToString("000", Helpers.InvariantCulture)[..3]
  };

}