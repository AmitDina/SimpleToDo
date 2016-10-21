using SimpleToDo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.Core
{
    public interface IRepositoryContainer<T>
    {
        ISimpleToDoRepository<T> ISimpleToDoRepository { get; }
    }
}
