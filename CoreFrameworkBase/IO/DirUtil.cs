using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace CoreFrameworkBase.IO
{
   /// <summary>
   /// Utility for directories
   /// </summary>
   public static class DirUtil
   {
      /// <summary>
      /// Ensures that a directory is created<para/>
      /// If the directory doesn't exist, it tries to create it
      /// </summary>
      /// <param name="path"></param>
      /// <param name="dirExistsWaitIntervalMS"></param>
      /// <param name="initalDirExistsWaitIntervalMS"></param>
      /// <param name="uselogging"></param>
      /// <exception cref="DirectoryNotFoundException">Directory couldn't created</exception>
      /// <exception cref="Exception">Any exception that could be thrown using IO(<see cref="Directory.CreateDirectory(string)"/>) or Threading(<see cref="Thread.Sleep(int)"/>)</exception>
      /// <seealso cref="Directory.CreateDirectory(string)"/>
      public static void EnsureCreated(string path, int dirExistsWaitIntervalMS = 200, int initalDirExistsWaitIntervalMS = 50, bool uselogging = true)
      {
         if (Directory.Exists(path))
            return;

         if (uselogging)
            Log.Debug($"Creating '{path}'");
         Directory.CreateDirectory(path);

         if (!Directory.Exists(path))
         {
            Thread.Sleep(initalDirExistsWaitIntervalMS);

            for (int i = 0; i < 10 && !Directory.Exists(path); i++)
               Thread.Sleep(dirExistsWaitIntervalMS);

            if (!Directory.Exists(path))
               throw new DirectoryNotFoundException("Failed to create directory");
         }
         if (uselogging)
            Log.Verbose($"Created '{path}'");
      }

      /// <summary>
      /// Ensures that a directory is created and clean<para/>
      /// If the directory exist, it will be deleted and recreated <para/>
      /// If the directory doesn't exist, it tries to create it -> <see cref="DirUtil.EnsureCreated(string, int, int, bool)"/> <para/>
      /// </summary>
      /// <param name="path"></param>
      /// <param name="dirDeletedWaitIntervalMS"></param>
      /// <param name="initalDirDeletedWaitIntervalMS"></param>
      /// <param name="uselogging"></param>
      /// <exception cref="DirectoryNotFoundException">Directory couldn't be cleaned or created</exception>
      /// <exception cref="Exception">Any exception that could be thrown using IO(<see cref="Directory.CreateDirectory(string)"/>) or Threading(<see cref="Thread.Sleep(int)"/>)</exception>
      /// <seealso cref="DirUtil.EnsureCreated(string, int, int, bool)"/>
      /// <seealso cref="Directory.Delete(string)"/>
      public static void EnsureCreatedAndClean(string path, int dirDeletedWaitIntervalMS = 200, int initalDirDeletedWaitIntervalMS = 50, bool uselogging = true)
      {
         if (Directory.Exists(path))
         {
            if(uselogging)
               Log.Debug($"Deleting '{path}'");
            Directory.Delete(path, true);

            if (Directory.Exists(path))
            {
               Thread.Sleep(initalDirDeletedWaitIntervalMS);

               for (int i = 0; i < 10 && Directory.Exists(path); i++)
                  Thread.Sleep(dirDeletedWaitIntervalMS);

               if (Directory.Exists(path))
                  throw new DirectoryNotFoundException("Failed to clean directory");
            }
            if (uselogging)
               Log.Verbose($"Deleted '{path}'");
         }
         EnsureCreated(path);
      }
   }
}
