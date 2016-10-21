using SimpleToDo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.Core.Repository
{
    public interface ISimpleToDoRepository<T>
    {
        SimpleToDoResult<T> GetAllSimpleToDoItems();

        SimpleToDoResult<T> GetSimpleToDoItems(Guid toDoItemId);

        BaseResult AddSimpleTodoItems(List<ToDoItem<T>> items);

        BaseResult AddSimpleTodoItem(ToDoItem<T> toDoItem);

        BaseResult UpdateimpleTodoItems(List<ToDoItem<T>> items);

        BaseResult UpdateSimpleTodoItem(ToDoItem<T> toDoItem);

        BaseResult DeleteSimpleTodoItems(List<Guid> ToDoItems);

        BaseResult DeleteSimpleTodoItem(Guid toDoItem);
    }
}
