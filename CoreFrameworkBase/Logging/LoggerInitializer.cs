using Serilog;
using CoreFrameworkBase.Logging.LoggerExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Serilog.Events;
using CoreFrameworkBase.Logging.Initalizer;
using CoreFrameworkBase.Logging.Initalizer.Impl;

namespace CoreFrameworkBase.Logging
{
   [Obsolete("Use the new Initializationsystem -> CoreFrameworkBase.Logging.Initalizer")]
   public class LoggerInitializer
   {
      public static LoggerInitializer Current { get; set; } = new LoggerInitializer();

      protected static bool LoggerSet { get; set; } = false;

      protected string RelativeLogFileDirectory { get; set; } = "logs";
      protected string FileDateTimeFormat { get; set; } = "yyyy-MM-dd-HH-mm-ss";
      protected string LogFileExtension { get; set; } = ".log";
      public string LogfilePath { get; protected set; }

      public void InitLogger(bool writeFile = false)
      {
         if (LoggerSet)
         {
            Log.Debug("Logger was already initialized");
            return;
         }

         LoggerSet = true;

         CurrentLoggerInitializer.InitializeWith(new DefaultLoggerInitializer()
         {
            RelativeLogFileDirectory = this.RelativeLogFileDirectory,
            FileDateTimeFormat = this.FileDateTimeFormat,
            LogFileExtension = this.LogFileExtension,
            WriteFile = writeFile
         });
      }
     
   }
}
