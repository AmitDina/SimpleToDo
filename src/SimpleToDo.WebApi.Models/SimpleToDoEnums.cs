using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.WebApi.Models
{
    public enum Priority
    {
        Unknown = 0,
        High = 1,
        Low = 2
    }
    public enum Status
    {
        Unknown = 0,
        Started = 1,
        InProgress = 2,
        Onhold = 3,
        Finished = 4
    }
}
