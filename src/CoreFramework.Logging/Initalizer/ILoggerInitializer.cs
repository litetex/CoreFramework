using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFramework.Logging.Initalizer
{
   /// <summary>
   /// Describes a LoggerInitializer
   /// </summary>
   public interface ILoggerInitializer : IDisposable
   {
      /// <summary>
      /// Should initalize <see cref="Serilog.Log.Logger"/>
      /// </summary>
      void Initialize();
   }
}
