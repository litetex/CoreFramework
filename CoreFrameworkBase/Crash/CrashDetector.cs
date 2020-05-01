using CoreFrameworkBase.Logging.Initalizer;
using CoreFrameworkBase.Logging.Initalizer.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFrameworkBase.Crash
{
   public class CrashDetector
   {
      public ILoggerInitializer FallBackLoggerInitializer { get; set; } = new DefaultLoggerInitializer()
      {
         Config = new DefaultLoggerInitializerConfig()
         {
            WriteFile = true
         }
      };

      /// <summary>
      /// Returns the <see cref="ILoggerInitializer"/> that should be used<para/>
      /// Default is <see cref="CrashDetector.FallBackLoggerInitializer"/>
      /// </summary>
      public Func<ILoggerInitializer> SupplyLoggerInitalizer { get; set; }

      public CrashDetector() 
      {
         SupplyLoggerInitalizer = () => FallBackLoggerInitializer;
      }

      public void Init()
      {
         Contract.Requires(FallBackLoggerInitializer != null);
         Contract.Requires(SupplyLoggerInitalizer != null);

         AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
      }

      protected void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs ev)
      {
         try
         {
            ILoggerInitializer loggerInitializer = SupplyLoggerInitalizer?.Invoke() ?? FallBackLoggerInitializer;

            CurrentLoggerInitializer.Set(loggerInitializer, CurrentLoggerInitializer.CurrentLoggerReSetMode.ALLOW);
            CurrentLoggerInitializer.InitLogging();

            Log.Fatal("Detected UnhandledException");
            if (ev.ExceptionObject is Exception ex)
               Log.Fatal("Run into unhandled error", ex);
            else
               Log.Fatal($"Run into unhandled error: {ev.ExceptionObject}");
         }
         catch(Exception ex)
         {
            Console.Error.WriteLine(ex);
         }

      }

   }
}
