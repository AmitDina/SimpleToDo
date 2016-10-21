using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.WebApi.Models
{
    [DataContract()]
    public class SimpleToDoResult<T> : BaseResult
    {
        [DataMember()]
        public List<ToDoItem<T>> Items { get; set; }

        [DataMember()]
        public int TotalCount { get; set; }

    }
}
