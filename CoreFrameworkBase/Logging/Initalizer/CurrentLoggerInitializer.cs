using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFrameworkBase.Logging.Initalizer
{
   /// <summary>
   /// Holds the current <see cref="ILoggerInitializer"/> which controls the <see cref="Serilog.Log.Logger"/> and through this the Logging used in <see cref="Log"/>
   /// </summary>
   public static class CurrentLoggerInitializer
   {
      static readonly object lockInit = new object();
      static readonly object lockReInitializationMode = new object();

      /// <summary>
      /// If you access it publically you should know what you are doing! <para/>
      /// Contains the Current <see cref="ILoggerInitializer"/>; may be null
      /// </summary>
      public static ILoggerInitializer Current { get; private set; }

      private static CurrentLoggerInitializationMode _reInitializationMode = CurrentLoggerInitializationMode.ALLOW_REINITIALIZE;
      /// <summary>
      /// Mode that describes, what happens if <see cref="CurrentLoggerInitializer.InitializeWith(ILoggerInitializer, CurrentLoggerInitializationMode)"/> is called more than once
      /// </summary>
      public static CurrentLoggerInitializationMode ReInitializationMode
      {
         get => _reInitializationMode;
         set
         {
            lock (lockReInitializationMode)
            {
               _reInitializationMode = value;
            }
         }
      }

      /// <summary>
      /// Initalizes <paramref name="initializationMode"/> and sets it to <see cref="Current"/> (behavior may differ through <paramref name="initializationMode"/>)
      /// </summary>
      /// <param name="loggerInitializer"><see cref="ILoggerInitializer"/> that should be initalized</param>
      /// <param name="initializationMode">See <see cref="CurrentLoggerInitializationMode"/>; default = <see cref="CurrentLoggerInitializationMode.ALLOW_REINITIALIZE"/> </param>
      public static void InitializeWith(ILoggerInitializer loggerInitializer, CurrentLoggerInitializationMode initializationMode = CurrentLoggerInitializationMode.ALLOW_REINITIALIZE)
      {
         ReInitializationMode = initializationMode;

         lock (lockReInitializationMode)
         {
            lock (lockInit)
            {
               if (Current != null)
               {
                  if (ReInitializationMode == CurrentLoggerInitializationMode.NO_REINITIALIZE)
                     return;
                  else if (ReInitializationMode == CurrentLoggerInitializationMode.ON_REINITIALIZE_THROW_EX)
                     throw new NotSupportedException($"{nameof(ReInitializationMode)}='{ReInitializationMode}' states that initalization " +
                        $"with already set and initalized {nameof(ILoggerInitializer)} is not allowed!");

                  Current.Dispose();
                  Current = null;
               }

               Current = loggerInitializer;
               Current.Initialize();
            }
         }
      }

      /// <summary>
      /// Mode of the Initalization
      /// </summary>
      public enum CurrentLoggerInitializationMode
      {
         /// <summary>
         /// Allow reinitalize/override existing <see cref="ILoggerInitializer"/>
         /// </summary>
         ALLOW_REINITIALIZE,
         /// <summary>
         /// Don't allow reinitalize/override existing <see cref="ILoggerInitializer"/>
         /// </summary>
         NO_REINITIALIZE,
         /// <summary>
         /// Don't allow reinitalize/override existing <see cref="ILoggerInitializer"/>, if done throw exception
         /// </summary>
         ON_REINITIALIZE_THROW_EX
      }
   }
}
