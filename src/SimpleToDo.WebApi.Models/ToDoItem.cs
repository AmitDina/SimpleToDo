using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.WebApi.Models
{

    public class ToDoItem
    {
        public Guid ToDoItemId { get; set; }

        public String Description { get; set; }

        public int SortOrder { get; set; }

        public override int GetHashCode()
        {
            if (ToDoItemId == null) return 0;
            return ToDoItemId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ToDoItem other = obj as ToDoItem;
            return other != null && other.ToDoItemId == this.ToDoItemId;
        }

    }
}
