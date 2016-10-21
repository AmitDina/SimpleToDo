using Newtonsoft.Json;
using SimpleToDo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.Data.DataAccess
{
    public class SimpleToDoFileDbDataAccess<T> : ISimpleToDoDataAccess<T>
    {

        private string _location;

        public SimpleToDoFileDbDataAccess(string location)
        {
            _location = location;
        }

        public SimpleToDoResult<T> GetAllSimpleToDoItems()
        {
            SimpleToDoResult<T> res = new SimpleToDoResult<T>();

            try
            {
                res.Items = new List<ToDoItem<T>>();

                using (StreamReader r = new StreamReader(_location))
                {
                    string json = r.ReadToEnd();

                    res.Items = JsonConvert.DeserializeObject<List<ToDoItem<T>>>(json);

                    res.Success = true;
                }

            }
            catch (Exception)
            {

                res.Success = false;

                throw;
            }

            return res;
        }

        public SimpleToDoResult<T> GetSimpleToDoItems(Guid toDoItemId)
        {
            throw new NotImplementedException();
        }

        public BaseResult AddSimpleTodoItems(List<ToDoItem<T>> items)
        {
            BaseResult res = new BaseResult();

            try
            {

                SimpleToDoResult<T> allToDoItems = GetAllSimpleToDoItems();

                if (allToDoItems != null && allToDoItems.Items != null && allToDoItems.Success)
                {
                    allToDoItems.Items.AddRange(items);
                }
                else
                {
                    allToDoItems = new SimpleToDoResult<T>();

                    allToDoItems.Items.AddRange(items);
                }

                string json = JsonConvert.SerializeObject(items);

                System.IO.File.WriteAllText(_location, json);

                res.Success = true;

            }
            catch (Exception)
            {

                res.Success = false;

                throw;
            }

            return res;
        }

        public BaseResult AddSimpleTodoItem(ToDoItem<T> toDoItem)
        {
            throw new NotImplementedException();
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
