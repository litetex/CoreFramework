using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CoreFrameworkBase.Config.JsonConfig;

namespace CoreFrameworkBase.Config
{
   /// <summary>
   /// Extend this class to create a custom Config
   /// </summary>
   public abstract class JsonConfig : FileBasedConfig<Configurator>
   {
      /// <summary>
      /// Configuration
      /// </summary>
      [JsonIgnore]
      public override Configurator Config { get; set; } = new Configurator();

      protected JsonConfig()
      { }

      public override string SerializeToFileContent()
      {
         return JsonConvert.SerializeObject(this, Formatting.Indented, Config.Settings);
      }

      public override void PopulateFrom(string filecontent)
      {
         JsonConvert.PopulateObject(filecontent, this, Config.Settings);
      }

      /// <summary>
      /// Outsourced class for Configuration
      /// </summary>
      public class Configurator : FileBasedConfigurator
      {
         public const string DEFAULT_SAVEPATH = "config.json";

         public Configurator()
         {

         }

         public Configurator(string savePath)
         {
            Contract.Requires(savePath != null);
            SavePath = savePath;
         }


         /// <summary>
         /// The Path where the file is saved; by default <see cref="DEFAULT_SAVEPATH"/> 
         /// </summary>
         /// <remarks>You shouldn't change it at runtime!</remarks>
         public override string SavePath { get; set; } = DEFAULT_SAVEPATH;

         /// <summary>
         /// Settings that are used, null = Default
         /// </summary>
         public JsonSerializerSettings Settings { get; set; } = new JsonSerializerSettings()
         {
            ObjectCreationHandling = ObjectCreationHandling.Replace
         };
      }

   }

}
