using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.WebApi.Models
{
    public class BaseResult
    {
        public Boolean Success { get; set; }

        public String Message { get; set; }
    }

}
