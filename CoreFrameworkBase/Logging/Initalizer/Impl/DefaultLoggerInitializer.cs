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
   public class DefaultLoggerInitializer : BaseLoggerInitializer
   {
      public LogEventLevel MinimumLogEventLevel { get; set; } = LogEventLevel.Information;

      public bool WriteInitalizerInfo { get; set; } = true;

      #region ConsoleProps

      public bool WriteConsole { get; set; } = true;

      public LogEventLevel? MinimumLogEventLevelConsole { get; set; }

      public string OutputTemplateConsole { get; set; } = "{Timestamp:HH:mm:ss,fff} {Level:u3} {ThreadId,-2} {Message:lj}{NewLine}{Exception}";

      #endregion ConsoleProps

      #region FileProps

      public bool WriteFile { get; set; } = false;

      public LogEventLevel? MinimumLogEventLevelFile { get; set; }

      public string OutputTemplateFile { get; set; } = "{Timestamp:HH:mm:ss,fff} {Log4NetLevel} {ThreadId,-2} {Message:lj}{NewLine}{Exception}";

      public string RelativeLogFileDirectory { get; set; } = "logs";
      public string FileDateTimeFormat { get; set; } = "yyyy-MM-dd-HH-mm-ss";
      public string LogFileExtension { get; set; } = ".log";

      #endregion FileProps

      public string LogfilePath { get; protected set; }

      protected override void DoInitialization()
      {
         var baseConf = GetBaseLoggerConfig();
         SetLogEventLevelToLoggerConfiguration(baseConf);

         if(WriteConsole)
            DoWriteConsole(baseConf);

         if (WriteFile)
         {
            LogfilePath = GetLogFilePath();
            DoWriteFile(baseConf);
         }

         Serilog.Log.Logger = baseConf.CreateLogger();

         if (WriteInitalizerInfo)
            FinalizeInfo();
      }

      protected LoggerConfiguration GetBaseLoggerConfig()
      {
         return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.With<Log4NetLevelMapperEnricher>()
            .Enrich.WithThreadId();
      }

      protected LoggerConfiguration SetLogEventLevelToLoggerConfiguration(LoggerConfiguration loggerConfiguration)
      {
         return MinimumLogEventLevel switch
         {
            LogEventLevel.Verbose => loggerConfiguration.MinimumLevel.Verbose(),
            LogEventLevel.Debug => loggerConfiguration.MinimumLevel.Debug(),
            LogEventLevel.Information => loggerConfiguration.MinimumLevel.Information(),
            LogEventLevel.Warning => loggerConfiguration.MinimumLevel.Warning(),
            LogEventLevel.Error => loggerConfiguration.MinimumLevel.Error(),
            LogEventLevel.Fatal => loggerConfiguration.MinimumLevel.Fatal(),
            _ => loggerConfiguration.MinimumLevel.Information(),
         };
      }
      protected void DoWriteConsole(LoggerConfiguration baseConf)
      {
         baseConf.WriteTo.Console(
            outputTemplate: OutputTemplateConsole,
            restrictedToMinimumLevel: MinimumLogEventLevelConsole ?? MinimumLogEventLevel);
      }

      protected void DoWriteFile(LoggerConfiguration baseConf)
      {
         baseConf.WriteTo.File(
            LogfilePath,
            outputTemplate: OutputTemplateFile,
            restrictedToMinimumLevel: MinimumLogEventLevelFile ?? MinimumLogEventLevel);
      }

      protected string GetLogFilePath() => $"{RelativeLogFileDirectory}{Path.DirectorySeparatorChar}{DateTime.Now.ToString(FileDateTimeFormat)}{LogFileExtension}";

      protected void FinalizeInfo()
      {
         Log.Info($"****** {Assembly.GetEntryAssembly().GetName().Name} {Assembly.GetEntryAssembly().GetName().Version} ******");
         Log.Info($"****** {Assembly.GetExecutingAssembly().GetName().Name} {Assembly.GetExecutingAssembly().GetName().Version} ******");

         if (WriteFile)
            Log.Info($"Logging to file: '{LogfilePath}'");
      }
   }
}