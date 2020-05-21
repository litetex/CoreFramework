using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFramework.Base.Util
{
   public class PropertySetter
   {
      public string SetLog { get; set; } = "Set";
      public string SetFaultyLog { get; set; } = "SetFaulty";

      public Action<string> Log { get; set; } = null;
      public Action<string> FaultyLog { get; set; } = null;


      public void SetString(Func<string> getFrom, Action<string> setInto, string nameofSetPar, bool logInput = true)
      {
         var input = getFrom();

         if (string.IsNullOrWhiteSpace(input))
            return;

         Log?.Invoke($"{SetLog}: {nameofSetPar}='{(logInput ? input : "****")}'");
         setInto(input);
      }

      public void SetStringSecret(Func<string> getFrom, Action<string> setInto, string nameofSetPar) => SetString(getFrom, setInto, nameofSetPar, false);

      public void SetEnum<E>(Func<string> getFrom, Action<E> setInto, string nameofSetPar) where E : struct, Enum
      {
         var input = getFrom();

         if (string.IsNullOrWhiteSpace(input))
            return;

         if (Enum.TryParse(input, true, out E parsedEnum))
         {
            Log?.Invoke($"{SetLog}: {nameofSetPar}='{parsedEnum}'(from '{input}')");
            setInto(parsedEnum);
         }
         else
            FaultyLog?.Invoke($"{SetFaultyLog}: {nameofSetPar}='{input}' was set but couldn't be parsed to {typeof(E).Name}");
      }

      public void SetBool(Func<string> getFrom, Action<bool> setInto, string nameofSetPar)
      {
         var input = getFrom();

         if (string.IsNullOrWhiteSpace(input))
            return;

         var fixedInput = input.Trim().ToLower();
         bool parsed = false;

         if (bool.TryParse(fixedInput, out bool parsedValue))
            parsed = true;
         else if (int.TryParse(fixedInput, out int parsedIntValue) && (parsedIntValue == 0 || parsedIntValue == 1))
         {
            parsedValue = Convert.ToBoolean(parsedIntValue);
            parsed = true;
         }

         if (parsed)
         {
            Log?.Invoke($"{SetLog}: {nameofSetPar}='{parsedValue}'(from '{input}')");
            setInto(parsedValue);
         }
         else
            FaultyLog?.Invoke($"{SetFaultyLog}: {nameofSetPar}='{input}' was set but couldn't be parsed to {nameof(Boolean)}");
      }
   }
}
