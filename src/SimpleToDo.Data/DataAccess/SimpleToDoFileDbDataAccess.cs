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
    public class SimpleToDoFileDbDataAccess : ISimpleToDoDataAccess
    {

        private string _location;

        public SimpleToDoFileDbDataAccess(string location)
        {
            _location = location;
        }


        public SimpleToDoResult GetAllSimpleToDoItems()
        {
            SimpleToDoResult res = new SimpleToDoResult();

            try
            {

                using (StreamReader r = new StreamReader(_location))
                {
                    string json = r.ReadToEnd();

                    List<SimpleToDoObject> simpleToDoDbObjects
                        = JsonConvert.DeserializeObject<List<SimpleToDoObject>>(json);

                    res.SimpleToDoItems = new Dictionary<ToDoItem, List<SimpleTask>>();

                    if (simpleToDoDbObjects != null)
                    {
                        foreach (SimpleToDoObject simpleToDoDbObject in simpleToDoDbObjects)
                        {
                            res.SimpleToDoItems.Add(simpleToDoDbObject.ToDoItem, simpleToDoDbObject.SimpleTasks);
                        }
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }

            return res;
        }

        public SimpleToDoResult GetSimpleToDoItems(Guid toDoItemId)
        {
            SimpleToDoResult res = new SimpleToDoResult();

            try
            {
                res.SimpleToDoItems = new Dictionary<ToDoItem, List<SimpleTask>>();

                using (StreamReader r = new StreamReader(_location))
                {
                    string json = r.ReadToEnd();

                    res.SimpleToDoItems =
                        JsonConvert.DeserializeObject<Dictionary<ToDoItem, List<SimpleTask>>>(json).Where(o => o.Key.ToDoItemId == toDoItemId).ToDictionary(o => o.Key, y => y.Value);

                }

            }
            catch (Exception)
            {

                throw;
            }

            return res;
        }

        public BaseResult AddSimpleTodoItems(List<ToDoItem> items)
        {
            BaseResult res = new BaseResult();

            try
            {
                foreach (ToDoItem item in items)
                {
                    res = AddSimpleTodoItem(item);
                }
            }
            catch (Exception)
            {

                res.Success = false;

                throw;
            }

            return res;
        }


        public BaseResult AddSimpleTodoItem(ToDoItem toDoItem)
        {
            BaseResult res = new BaseResult();

            try
            {

                SimpleToDoResult allToDoItems = this.GetAllSimpleToDoItems();

                if (allToDoItems != null && allToDoItems.SimpleToDoItems != null && !allToDoItems.SimpleToDoItems.ContainsKey(toDoItem))
                {
                    allToDoItems.SimpleToDoItems.Add(toDoItem, new List<SimpleTask>());
                }
                else
                {
                    allToDoItems = new SimpleToDoResult();

                    allToDoItems.SimpleToDoItems = new Dictionary<ToDoItem, List<SimpleTask>>();

                    allToDoItems.SimpleToDoItems.Add(toDoItem, new List<SimpleTask>());
                }

                List<SimpleToDoObject> simpleToDoDbObjects = new List<SimpleToDoObject>();


                // for serialising purpsoe
                foreach (KeyValuePair<ToDoItem, List<SimpleTask>> item in allToDoItems.SimpleToDoItems)
                {
                    SimpleToDoObject simpleToDoDbObject = 
                        new SimpleToDoObject() { ToDoItem = item.Key, SimpleTasks = item.Value };

                    simpleToDoDbObjects.Add(simpleToDoDbObject);
                }



                string json = JsonConvert.SerializeObject(simpleToDoDbObjects);

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

        public BaseResult AddSimpleTodoItemTask(Dictionary<ToDoItem, List<SimpleTask>> items)
        {
            BaseResult res = new BaseResult();

            try
            {

                SimpleToDoResult allToDoItems = GetAllSimpleToDoItems();

                foreach (KeyValuePair<ToDoItem, List<SimpleTask>> toDoItem in items)
                {
                    if (allToDoItems == null)
                        continue;

                    if (!allToDoItems.SimpleToDoItems.ContainsKey(toDoItem.Key))
                    {
                        allToDoItems.SimpleToDoItems.Add(toDoItem.Key, toDoItem.Value);
                    }
                    else
                    {
                        allToDoItems.SimpleToDoItems[toDoItem.Key].AddRange(toDoItem.Value);
                    }

                    List<SimpleToDoObject> simpleToDoDbObjects = new List<SimpleToDoObject>();

                    foreach (KeyValuePair<ToDoItem, List<SimpleTask>> item in allToDoItems.SimpleToDoItems)
                    {
                        SimpleToDoObject simpleToDoDbObject = new SimpleToDoObject()
                        {
                            ToDoItem = item.Key,
                            SimpleTasks = item.Value
                        };

                        simpleToDoDbObjects.Add(simpleToDoDbObject);
                    }

                    string json = JsonConvert.SerializeObject(simpleToDoDbObjects);


                    System.IO.File.WriteAllText(_location, json);

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



    }

}
