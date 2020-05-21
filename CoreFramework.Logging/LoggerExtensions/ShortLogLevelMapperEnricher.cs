using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace CoreFramework.Logging.LoggerExtensions
{
   public class ShortLogLevelMapperEnricher : ILogEventEnricher
   {
      public const string PROPERTY_NAME = "ShortLogLevel";

      public const string
         DEBUG = "DEBUG",
         ERROR = "ERROR",
         FATAL = "FATAL",
         INFO = "INFO ",
         TRACE = "TRACE",
         WARN = "WARN ";

      public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
      {
         var logLevel = string.Empty;

         switch (logEvent.Level)
         {
            case LogEventLevel.Verbose:
               logLevel = TRACE;
               break;

            case LogEventLevel.Debug:
               logLevel = DEBUG;
               break;

            case LogEventLevel.Warning:
               logLevel = WARN;
               break;

            case LogEventLevel.Information:
               logLevel = INFO;
               break;

            case LogEventLevel.Error:
               logLevel = ERROR;
               break;

            case LogEventLevel.Fatal:
               logLevel = FATAL;
               break;
         }

         logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PROPERTY_NAME, logLevel));
      }

   }
}
