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
      public DefaultLoggerInitializerConfig Config { get; set; } = new DefaultLoggerInitializerConfig();

      public DefaultLoggerInitializer(DefaultLoggerInitializerConfig config = null)
      {
         if(config != null)
            Config = config;
      }

      public string LogfilePath { get; protected set; }

      public bool WritingToFile { get; protected set; } = false;

      public bool WritingToConsole { get; protected set; } = false;

      protected override void DoInitialization()
      {
         var baseConf = GetBaseLoggerConfig();
         SetLogEventLevelToLoggerConfiguration(baseConf);

         // Console
         if (Config.WriteConsole && !WritingToConsole)
            WriteConsoleInitInfo();
         if (Config.WriteConsole)
            DoWriteConsole(baseConf);

         // File
         if (Config.CreateLogFilePathOnStartup)
            LogfilePath ??= GetLogFilePath();

         if (Config.WriteFile && !WritingToFile)
            WriteFileInitInfo();
         if (Config.WriteFile)
         {
            LogfilePath ??= GetLogFilePath();
            DoWriteFile(baseConf);
         }

         Serilog.Log.Logger = baseConf.CreateLogger();

         FinalizeInfo();

         AppDomain.CurrentDomain.ProcessExit -= OnExit;
         if(Config.FlushOnExit)
            AppDomain.CurrentDomain.ProcessExit += OnExit;

         WritingToConsole = Config.WriteConsole;
         WritingToFile = Config.WriteFile;
      }

      protected virtual LoggerConfiguration GetBaseLoggerConfig()
      {
         return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.With<Log4NetLevelMapperEnricher>()
            .Enrich.WithThreadId();
      }

      protected virtual LoggerConfiguration SetLogEventLevelToLoggerConfiguration(LoggerConfiguration loggerConfiguration)
      {
         return Config.MinimumLogEventLevel switch
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
      protected virtual void DoWriteConsole(LoggerConfiguration baseConf)
      {
         baseConf.WriteTo.Console(
            outputTemplate: Config.OutputTemplateConsole,
            restrictedToMinimumLevel: Config.MinimumLogEventLevelConsole ?? Config.MinimumLogEventLevel);
      }

      protected virtual void DoWriteFile(LoggerConfiguration baseConf)
      {
         baseConf.WriteTo.File(
            LogfilePath,
            shared: Config.FileShared,
            outputTemplate: Config.OutputTemplateFile,
            restrictedToMinimumLevel: Config.MinimumLogEventLevelFile ?? Config.MinimumLogEventLevel);
      }

      protected virtual string GetLogFilePath() => $"{Config.RelativeLogFileDirectory}{Path.DirectorySeparatorChar}{DateTime.Now.ToString(Config.FileDateTimeFormat)}{Config.LogFileExtension}";

      protected virtual void OnExit(object s, EventArgs ev)
      {
         Log.Info("Shutting down logger; Flushing...");
         Serilog.Log.CloseAndFlush();
      }

      protected virtual void FinalizeInfo()
      {
         if (!Initialized)
            Log.Info("Initalized logging");
         else
            Log.Info("Reinitalized logging");

         if (Config.WriteFile && !WritingToFile)
            Log.Info($"Logging to file: '{LogfilePath}'");
      }

      protected virtual void WriteConsoleInitInfo()
      {
         Console.WriteLine($"****** {Assembly.GetEntryAssembly().GetName().Name} {Assembly.GetEntryAssembly().GetName().Version} ******");
         Console.WriteLine($"****** {Assembly.GetExecutingAssembly().GetName().Name} {Assembly.GetExecutingAssembly().GetName().Version} ******");
      }

      protected virtual void WriteFileInitInfo()
      {
         File.AppendAllLines(LogfilePath, new string[]
         {
            $"****** {Assembly.GetEntryAssembly().GetName().Name} {Assembly.GetEntryAssembly().GetName().Version} ******",
            $"****** {Assembly.GetExecutingAssembly().GetName().Name} {Assembly.GetExecutingAssembly().GetName().Version} ******",
         });
      }
   }
}