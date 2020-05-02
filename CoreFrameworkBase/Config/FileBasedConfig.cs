using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

namespace CoreFrameworkBase.Config
{
   public abstract class FileBasedConfig<C> where C : FileBasedConfigConfigurator
   {
      /// <summary>
      /// Configuration
      /// </summary>
      /// <remarks>
      /// This must be excluded while serializing/deserializing!!!
      /// </remarks>
      public abstract C Config { get; set; }

      /// <summary>
      /// Loads from file
      /// </summary>
      /// <param name="fileNotFoundAction">Determine what to do if the file doesn't exists</param>
      public void Load(LoadFileNotFoundAction fileNotFoundAction = LoadFileNotFoundAction.THROW_EX)
      {
         Contract.Assert(Config != null);
         Contract.Assert(Config.SavePath != null);

         if (File.Exists(Config.SavePath))
            PopulateFrom(File.ReadAllText(Config.SavePath));
         else
         {
            if (fileNotFoundAction == LoadFileNotFoundAction.THROW_EX)
               throw new FileNotFoundException($"Could not find file '{Config.SavePath}'");
            else if (fileNotFoundAction == LoadFileNotFoundAction.GENERATE_FILE)
               Save();
         }
      }

      /// <summary>
      /// Saves to file
      /// </summary>
      /// <param name="createifnotexists">creates the file if it doesn't exists</param>
      public virtual void Save(bool createifnotexists = true)
      {
         Contract.Assert(Config != null);
         Contract.Assert(Config.SavePath != null);

         if (createifnotexists)
         {
            string dir = Path.GetDirectoryName(Config.SavePath);
            if (!string.IsNullOrWhiteSpace(dir))
               Directory.CreateDirectory(dir);
         }
         File.WriteAllText(Config.SavePath, SerializeToFileContent());
      }

      /// <summary>
      /// Serializes the current object to file content
      /// </summary>
      /// <returns></returns>
      public abstract string SerializeToFileContent();

      /// <summary>
      /// Deserializes the <paramref name="filecontent"/> and populates the current config
      /// </summary>
      /// <param name="filecontent"></param>
      public abstract void PopulateFrom(string filecontent);
   }

   public enum LoadFileNotFoundAction
   {
      NONE,
      THROW_EX,
      GENERATE_FILE
   }
}
