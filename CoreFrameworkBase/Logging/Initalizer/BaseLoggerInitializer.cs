using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFrameworkBase.Logging.Initalizer
{
   /// <summary>
   /// A basic LoggerInitalizer:<para/>
   /// Predefined:
   /// <list type="bullet">
   ///   <item><description>Only allows initalization once -> <see cref="Initialize"/>/<see cref="DoInitialization"/></description></item>
   ///   <item><description>Default NOP for Dispose ->  <see cref="Dispose"/>/<see cref="Dispose(bool)"/></description></item>
   /// </list>
   /// </summary>
   public abstract class BaseLoggerInitializer : ILoggerInitializer
   {
      protected bool Initialized { get; set; } = false;

      protected object lockIsInitialized = new object();

      public void Initialize()
      {
         if (Initialized)
            return;

         lock (lockIsInitialized)
         {
            if (Initialized)
               return;

            DoInitialization();

            Initialized = true;
         }
      }

      protected abstract void DoInitialization();

      /// <seealso cref="https://rules.sonarsource.com/csharp/RSPEC-3881"/>
      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Can be called more than once
      /// </summary>
      /// <param name="disposing">origin of call: true = Dispose, false = Deconstructor</param>
      /// <seealso cref="https://rules.sonarsource.com/csharp/RSPEC-3881"/>
      protected virtual void Dispose(bool disposing)
      {
         //Do nothing here
      }

      /// <seealso cref="https://rules.sonarsource.com/csharp/RSPEC-3881"/>
      ~BaseLoggerInitializer()
      {
         Dispose(false);
      }
   }
}
