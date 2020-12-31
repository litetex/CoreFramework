using CoreFramework.Config.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;

namespace CoreFramework.Config
{
   public class YamlConfig : YamlConfig<YamlConfigConfigurator>
   {
      public YamlConfig()
      {
         Config = new YamlConfigConfigurator();
      }
   }

   public class YamlConfig<C> : FileBasedConfig<C> where C : YamlConfigConfigurator
   {
      [YamlIgnore]
      public override C Config { get; set; }

      public override void PopulateFrom(string filecontent)
      {
         Type t = this.GetType();

         var obj = new DeserializerBuilder()
                  .Build()
                  .Deserialize(new StringReader(filecontent), t);

         PropertyCopier.Copy(obj, this);
      }

      public override string SerializeToFileContent()
      {
         return 
            new SerializerBuilder()
               .Build()
               .Serialize(
                 this);
      }
   }

   public class YamlConfigConfigurator : FileBasedConfigConfigurator
   {
      public const string DEFAULT_SAVEPATH = "config.yml";

      /// <summary>
      /// The Path where the file is saved; by default <see cref="DEFAULT_SAVEPATH"/> 
      /// </summary>
      /// <remarks>You shouldn't change it at runtime!</remarks>
      public override string SavePath { get; set; } = DEFAULT_SAVEPATH;
   }
}
