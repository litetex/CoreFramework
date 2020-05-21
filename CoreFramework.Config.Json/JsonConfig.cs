using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework.Config
{
   /// <summary>
   /// Extend this class to create a custom Config
   /// </summary>
   public abstract class JsonConfig : JsonConfig<JsonConfigConfigurator>
   {
      protected JsonConfig()
      {
         Config = new JsonConfigConfigurator();
      }
   }

   public abstract class JsonConfig<C> : FileBasedConfig<C> where C : JsonConfigConfigurator
   {
      /// <summary>
      /// Configuration
      /// </summary>
      [JsonIgnore]
      public override C Config { get; set; }

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
   }

   /// <summary>
   /// Outsourced class for Configuration
   /// </summary>
   public class JsonConfigConfigurator : FileBasedConfigConfigurator
   {
      public const string DEFAULT_SAVEPATH = "config.json";

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
