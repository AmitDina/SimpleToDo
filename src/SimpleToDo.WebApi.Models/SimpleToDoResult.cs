using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.WebApi.Models
{
    public class SimpleToDoResult
    {
        public IEnumerable<ToDoItem> ToDoItems
        {
            get
            {
                return SimpleToDoItems.Keys;
            }
            set
            {
            }
        }

        public IEnumerable<List<SimpleTask>> SimpleTasks
        {
            get
            {
                return SimpleToDoItems.Values;
            }

            set
            {

            }

        }

        public IEnumerable<SimpleToDoObject> ToDoItem
        {
            get
            {
                return SimpleToDoItems.Select(o => new SimpleToDoObject() { ToDoItem = o.Key, SimpleTasks = o.Value });
            }
            set
            {
            }
        }

        [JsonIgnore]
        [IgnoreDataMember]
        public Dictionary<ToDoItem, List<SimpleTask>> SimpleToDoItems = new Dictionary<ToDoItem, List<SimpleTask>>();
    }

    public class SimpleToDoObject
    {
        public ToDoItem ToDoItem { get; set; }

        public List<SimpleTask> SimpleTasks { get; set; }

    }
}
