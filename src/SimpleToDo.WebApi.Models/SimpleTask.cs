using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.WebApi.Models
{
    public class SimpleTask<T>
    {
        public T SimpleTaskType { get; set; }

        public Guid SimpleTaskId { get; set; }

        public string Title { get; set; }

        public Status Status { get; set; }

        public DateTime? DateAdded { set; get; }

        public DateTime? LastUpdated { set; get; }

        public DateTime? DueDate { set; get; }

        public string Notes { set; get; }
    }
}
