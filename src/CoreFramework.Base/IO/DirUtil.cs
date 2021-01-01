using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CoreFramework.Base.IO
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
      /// <param name="onCreating"></param>
      /// <param name="onCreated"></param>
      /// <exception cref="DirectoryNotFoundException">Directory couldn't created</exception>
      /// <exception cref="Exception">Any exception that could be thrown using IO(<see cref="Directory.CreateDirectory(string)"/>) or Threading(<see cref="Thread.Sleep(int)"/>)</exception>
      /// <seealso cref="Directory.CreateDirectory(string)"/>
      public static void EnsureCreated(string path, int dirExistsWaitIntervalMS = 200, int initalDirExistsWaitIntervalMS = 50, Action<string> onCreating = null, Action<string> onCreated = null)
      {
         if (Directory.Exists(path))
            return;

         onCreating?.Invoke(path);

         Directory.CreateDirectory(path);

         if (!Directory.Exists(path))
         {
            Thread.Sleep(initalDirExistsWaitIntervalMS);

            for (int i = 0; i < 10 && !Directory.Exists(path); i++)
               Thread.Sleep(dirExistsWaitIntervalMS);

            if (!Directory.Exists(path))
               throw new DirectoryNotFoundException("Failed to create directory");
         }
         onCreated?.Invoke(path);
      }

      /// <summary>
      /// Ensures that a directory is created and clean<para/>
      /// If the directory exist, it will be deleted and recreated <para/>
      /// If the directory doesn't exist, it tries to create it -> <see cref="DirUtil.EnsureCreated(string, int, int, bool)"/> <para/>
      /// </summary>
      /// <param name="path"></param>
      /// <param name="dirDeletedWaitIntervalMS"></param>
      /// <param name="initalDirDeletedWaitIntervalMS"></param>
      /// <param name="onDeleteing"></param>
      /// <param name="onDeleted"></param>
      /// <exception cref="DirectoryNotFoundException">Directory couldn't be cleaned or created</exception>
      /// <exception cref="Exception">Any exception that could be thrown using IO(<see cref="Directory.CreateDirectory(string)"/>) or Threading(<see cref="Thread.Sleep(int)"/>)</exception>
      /// <seealso cref="DirUtil.EnsureCreated(string, int, int, bool)"/>
      /// <seealso cref="Directory.Delete(string)"/>
      public static void EnsureCreatedAndClean(string path, int dirDeletedWaitIntervalMS = 200, int initalDirDeletedWaitIntervalMS = 50, Action<string> onDeleteing = null, Action<string> onDeleted = null)
      {
         if (Directory.Exists(path))
         {
            onDeleteing?.Invoke(path);

            Directory.Delete(path, true);

            if (Directory.Exists(path))
            {
               Thread.Sleep(initalDirDeletedWaitIntervalMS);

               for (int i = 0; i < 10 && Directory.Exists(path); i++)
                  Thread.Sleep(dirDeletedWaitIntervalMS);

               if (Directory.Exists(path))
                  throw new DirectoryNotFoundException("Failed to clean directory");
            }
            onDeleted?.Invoke(path);
         }
         EnsureCreated(path);
      }

      /// <summary>
      /// Copy a directory
      /// </summary>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="overwrite">Overwrite already existing files; if false and duplicate file: Exception</param>
      public static void Copy(string source, string target, bool overwrite = true)
      {
         var sourceDI = new DirectoryInfo(source);
         var targetDI = new DirectoryInfo(target);

         Copy(sourceDI, targetDI, overwrite);
      }

      /// <summary>
      /// Copy a directory
      /// </summary>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="overwrite">Overwrite already existing files; if false and duplicate file: Exception</param>
      public static void Copy(DirectoryInfo source, DirectoryInfo target, bool overwrite = true)
      {
         Directory.CreateDirectory(target.FullName);

         // Copy each file into the new directory.
         foreach (FileInfo fi in source.GetFiles())
            fi.CopyTo(Path.Combine(target.FullName, fi.Name), overwrite);

         // Copy each subdirectory using recursion.
         foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            Copy(diSourceSubDir, target.CreateSubdirectory(diSourceSubDir.Name), overwrite);
      }

      /// <summary>
      /// Similar to <see cref="Directory.Delete(string, bool)"/> but also deletes read-only files
      /// </summary>
      /// <param name="directory"></param>
      public static void DeleteSafe(string directory)
      {
         foreach (string subdirectory in Directory.EnumerateDirectories(directory))
            DeleteSafe(subdirectory);

         foreach (string fileName in Directory.EnumerateFiles(directory))
         {
            var fileInfo = new FileInfo(fileName)
            {
               Attributes = FileAttributes.Normal
            };
            fileInfo.Delete();
         }

         //Something was not fast enough to delete, so let's wait a moment
         if (Directory.EnumerateFiles(directory).Any())
         {
            Thread.Sleep(10);

            //Again? - Wait a moment longer
            if (Directory.EnumerateFiles(directory).Any())
            {
               Thread.Sleep(100);
            }
         }

         int[] waitMs = new int[] { 10, 100, 1000, 5000 };
         var waitStartIndex = 0;
         while (Directory.EnumerateFiles(directory).Any() || Directory.EnumerateDirectories(directory).Any())
         {
            if (waitStartIndex > waitMs.Length - 1)
               throw new TimeoutException($"Failed to delete '{directory}' in given time");
            Thread.Sleep(waitMs[waitStartIndex]);
            waitStartIndex++;
         }

         Directory.Delete(directory, true);
      }
   }
}
