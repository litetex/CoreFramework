using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFrameworkBase.Logging.Initalizer
{
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
   }
}
