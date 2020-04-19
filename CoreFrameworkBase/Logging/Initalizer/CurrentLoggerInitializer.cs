using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFrameworkBase.Logging.Initalizer
{
   public static class CurrentLoggerInitializer
   {
      static readonly object lockInit = new object();

      public static ILoggerInitializer Current { get; private set; }

      public static void InitializeWith(ILoggerInitializer loggerInitializer)
      {
         lock(lockInit)
         {
            Current = loggerInitializer;
            Current.Initialize();
         }
      }
   }
}
