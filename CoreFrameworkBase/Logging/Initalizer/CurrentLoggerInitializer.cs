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

      private static CurrentLoggerReSetMode _reInitializationMode = CurrentLoggerReSetMode.ALLOW;
      /// <summary>
      /// Mode that describes, what happens if <see cref="CurrentLoggerInitializer.Set(ILoggerInitializer, CurrentLoggerReSetMode)"/> is called more than once
      /// </summary>
      public static CurrentLoggerReSetMode ReInitializationMode
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
      /// Sets <paramref name="loggerInitializer"/> to <see cref="Current"/> (behavior may differ through <paramref name="setMode"/>)
      /// </summary>
      /// <param name="loggerInitializer"><see cref="ILoggerInitializer"/> that should be set</param>
      /// <param name="setMode">See <see cref="CurrentLoggerReSetMode"/>; default = <see cref="CurrentLoggerReSetMode.ALLOW"/> </param>
      public static void Set(ILoggerInitializer loggerInitializer, CurrentLoggerReSetMode setMode = CurrentLoggerReSetMode.ALLOW)
      {
         ReInitializationMode = setMode;

         lock (lockReInitializationMode)
         {
            lock (lockInit)
            {
               if (Current != null)
               {
                  if (ReInitializationMode == CurrentLoggerReSetMode.NO_ALLOW)
                     return;
                  else if (ReInitializationMode == CurrentLoggerReSetMode.ON_RESET_THROW_EX)
                     throw new NotSupportedException($"{nameof(ReInitializationMode)}='{ReInitializationMode}' states that initalization " +
                        $"with already set and initalized {nameof(ILoggerInitializer)} is not allowed!");

                  Current.Dispose();
                  Current = null;
               }

               Current = loggerInitializer;
            }
         }
      }

      /// <summary>
      /// Initalizes logging with <see cref="CurrentLoggerInitializer.Current"/>
      /// </summary>
      /// <param name="initAction">Action to execute on init, e.g. enable writing to a logfile</param>
      public static void InitLogging(Action<ILoggerInitializer> initAction = null)
      {
         if (Current == null)
            throw new InvalidOperationException($"{nameof(Current)} must be set!");

         initAction?.Invoke(Current);
         Current.Initialize();
      }

      /// <summary>
      /// Mode of the Initalization
      /// </summary>
      public enum CurrentLoggerReSetMode
      {
         /// <summary>
         /// Allow setting <see cref="ILoggerInitializer"/> again
         /// </summary>
         ALLOW,
         /// <summary>
         /// Don't allow setting <see cref="ILoggerInitializer"/>again
         /// </summary>
         NO_ALLOW,
         /// <summary>
         /// Don't allow setting <see cref="ILoggerInitializer"/> again, if done throw exception
         /// </summary>
         ON_RESET_THROW_EX
      }
   }
}
