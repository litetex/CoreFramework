using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreFramework.Base.IO
{
   /// <summary>
   /// Builds Paths, e.g. for configuration purposes
   /// </summary>
   public static class PathBuilder
   {
      /// <summary>
      /// Builds a path from <paramref name="relativeParent"/> + <paramref name="path"/> (if <paramref name="path"/> is relative)
      /// </summary>
      /// <param name="name">Just for documentation purposes</param>
      /// <param name="path">Not null</param>
      /// <param name="relativeParent"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">Path is not set, no full path could be resolved or a failure of the underlying operation</exception>
      public static string BuildPath(string name, string path, string relativeParent)
      {
         if (path == null)
            throw new ArgumentException($"{name} is not set");

         //May throw exception
         if (Path.IsPathRooted(path))
            return path;

         //May throw exception
         if (Path.GetFullPath(path) == null)
            throw new ArgumentException($"Weird stuff happens; The path[='{path}'] of '{name}' couldn't be resolved to a full path (was null)");

         return Path.Combine(relativeParent, path);
      }
   }
}
