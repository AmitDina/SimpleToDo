using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace SimpleToDo.WebApi.Models
{
    public class SimplePagedToDoResult<T> : BaseResult
    {
        public IEnumerable<T> Items { get; set; }

        public int FilteredCount { get; set; }

        public int TotalCount { get; set; }
       
    }
}
