using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFrameworkBase.Config
{
   public abstract class FileBasedConfigurator
   {
      public virtual string SavePath { get; set; }
   }
}
