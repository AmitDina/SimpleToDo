using SimpleToDo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTodo.Tests
{
    public static class SimpleToDoTestHelpers
    {

        public static ToDoItem GetDemoToDoItem()
        {
            return new ToDoItem()
            {
                ToDoItemId = Guid.NewGuid(),
                Description = string.Format("SIMPLE TODO - {0}", GetRandomNumber()),
                SortOrder = GetRandomNumber()
            };
        }


        public static SimpleTask GetDemoTask(Guid toDoItemId)
        {
            return new SimpleTask()
            {
                ToDoItemId = toDoItemId,
                SimpleTaskId = Guid.NewGuid(),
                Title = string.Format("[{0}] Task", GetRandomNumber()),
                DateAdded = DateTime.Now,
                Notes = "Added using unit test",
            };
        }

        public static List<SimpleTask> GetDemoTasks(Guid toDoItemId)
        {
            return new List<SimpleTask>() {
               GetDemoTask(toDoItemId),
               GetDemoTask(toDoItemId),
               GetDemoTask(toDoItemId)
           };
        }

        public static int GetRandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(100);
        }
    }
}
