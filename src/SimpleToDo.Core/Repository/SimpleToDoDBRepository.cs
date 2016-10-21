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
    public class SimpleToDoDBRepository : ISimpleToDoRepository
    {
        private ISimpleToDoDataAccess _dbRepo;

        public SimpleToDoDBRepository(ISimpleToDoDataAccess dbRepo)
        {
            _dbRepo = dbRepo;
        }

        SimpleToDoResult ISimpleToDoRepository.GetAllSimpleToDoItems()
        {
            return _dbRepo.GetAllSimpleToDoItems();

        }

        SimpleToDoResult ISimpleToDoRepository.GetSimpleToDoItems(Guid toDoItemId)
        {
            return _dbRepo.GetSimpleToDoItems(toDoItemId);
        }

        public BaseResult AddSimpleTodoItems(List<ToDoItem> items)
        {
            return _dbRepo.AddSimpleTodoItems(items);
        }

        public BaseResult AddSimpleTodoItem(ToDoItem toDoItem)
        {
            return _dbRepo.AddSimpleTodoItem(toDoItem);
        }

        public BaseResult AddSimpleTodoItemTask(Dictionary<ToDoItem, List<SimpleTask>> items)
        {
            return _dbRepo.AddSimpleTodoItemTask(items);
        }
    }
}
