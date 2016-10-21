using SimpleToDo.Data;
using SimpleToDo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.Core.Repository
{
    public class SimpleToDoDBRepository<T> : ISimpleToDoRepository<T> where T : class
    {
        private ISimpleToDoDataAccess<T> _dbRepo;

        public SimpleToDoDBRepository(ISimpleToDoDataAccess<T> dbRepo)
        {
            _dbRepo = dbRepo;
        }

        public SimpleToDoResult<T> GetAllSimpleToDoItems()
        {
            return _dbRepo.GetAllSimpleToDoItems();
        }

        public SimpleToDoResult<T> GetSimpleToDoItems(Guid toDoItemId)
        {
            throw new NotImplementedException();
        }

        public BaseResult AddSimpleTodoItems(List<ToDoItem<T>> items)
        {
            return _dbRepo.AddSimpleTodoItems(items);
        }

        public BaseResult AddSimpleTodoItem(ToDoItem<T> toDoItem)
        {
           return _dbRepo.AddSimpleTodoItems(new List<ToDoItem<T>>() { toDoItem });
        }

        public BaseResult UpdateimpleTodoItems(List<ToDoItem<T>> items)
        {
            throw new NotImplementedException();
        }

        public BaseResult UpdateSimpleTodoItem(ToDoItem<T> toDoItem)
        {
            throw new NotImplementedException();
        }

        public BaseResult DeleteSimpleTodoItems(List<Guid> ToDoItems)
        {
            throw new NotImplementedException();
        }

        public BaseResult DeleteSimpleTodoItem(Guid toDoItem)
        {
            throw new NotImplementedException();
        }
    }
}
