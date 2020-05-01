using CoreFrameworkBase.Logging.LoggerExtensions;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CoreFrameworkBase.Logging.Initalizer.Impl
{
   public class DefaultLoggerInitializerConfig
   {
      public virtual LogEventLevel MinimumLogEventLevel { get; set; } = LogEventLevel.Information;

      public virtual bool FlushOnExit { get; set; } = true;

      #region ConsoleProps

      public virtual bool WriteConsole { get; set; } = true;

      public virtual LogEventLevel? MinimumLogEventLevelConsole { get; set; }

      public virtual string OutputTemplateConsole { get; set; } = "{Timestamp:HH:mm:ss,fff} {Level:u3} {ThreadId,-2} {Message:lj}{NewLine}{Exception}";

      #endregion ConsoleProps

      #region FileProps

      public virtual bool WriteFile { get; set; } = false;

      public virtual bool FileShared { get; set; } = true;

      public virtual LogEventLevel? MinimumLogEventLevelFile { get; set; }

      public virtual string OutputTemplateFile { get; set; } = "{Timestamp:HH:mm:ss,fff} {Log4NetLevel} {ThreadId,-2} {Message:lj}{NewLine}{Exception}";

      public virtual string RelativeLogFileDirectory { get; set; } = "logs";
      public virtual string FileDateTimeFormat { get; set; } = "yyyy-MM-dd-HH-mm-ss-fff";
      public virtual string LogFileExtension { get; set; } = ".log";

      public virtual bool CreateLogFilePathOnStartup { get; set; } = true;

      #endregion FileProps
   }
}