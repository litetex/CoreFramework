using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFramework.Config.Util
{
   public static class PropertyCopier
   {
      public static void Copy(object parent, object child)
      {
         var parentProperties = parent.GetType().GetProperties();
         var childProperties = child.GetType().GetProperties();

         foreach (var parentProperty in parentProperties)
         {
            foreach (var childProperty in childProperties)
            {
               if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
               {
                  childProperty.SetValue(child, parentProperty.GetValue(parent));
                  break;
               }
            }
         }
      }
   }
}
