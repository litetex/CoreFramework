using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace CoreFramework.Config
{
   public class FileBasedConfigConfigurator
   {
      public FileBasedConfigConfigurator()
      {

      }

      public FileBasedConfigConfigurator(string savePath)
      {
         Contract.Requires(savePath != null);
         SavePath = savePath;
      }

      public virtual string SavePath { get; set; }
   }
}
