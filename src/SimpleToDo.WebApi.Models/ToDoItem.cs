using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.WebApi.Models
{
    public class ToDoItem<T>
    {
        public Guid ToDoItemId { get; set; }

        public String Description { get; set; }

        public List<SimpleTask<T>> SimpleTasks { get; set; }
    }
}
