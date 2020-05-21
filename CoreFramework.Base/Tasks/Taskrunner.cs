using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFramework.Base.Tasks
{
   /// <summary>
   /// Runs tasks
   /// </summary>
   public static class TaskRunner
   {
      /// <summary>
      /// Run the specified actions as tasks -> <see cref="RunTasks(Task[])"/>
      /// </summary>
      /// <param name="actions"></param>
      /// <seealso cref="RunTasks(Task[])"/>
      public static void RunTasks(params Action[] actions)
      {
         RunTasks(actions.Select(act => Task.Run(act)).ToArray());
      }

      /// <summary>
      /// Run the specified tasks and wait for them to finish
      /// </summary>
      /// <param name="tasks"></param>
      /// <exception cref="InvalidOperationException">One or more tasks failed</exception>
      public static void RunTasks(params Task[] tasks)
      {
         var tasklist = new List<Task>();

         foreach (var task in tasks)
            tasklist.Add(task);

         var mastertask = Task.WhenAll(tasklist);
         mastertask.Wait();

         if (!mastertask.IsCompletedSuccessfully)
            throw new InvalidOperationException("One or more tasks failed");
      }
   }
}
