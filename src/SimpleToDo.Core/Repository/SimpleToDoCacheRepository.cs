using SimpleToDo.Data;
using SimpleToDo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.Core.Repository
{
    public class SimpleToDoCacheRepository : ISimpleToDoRepository 
    {
        private const string TODOCACHEIDENT = "TODOCACHE";

        private MemoryCache _toDoCache;

        private ISimpleToDoDataAccess _dbRepo;

        public SimpleToDoCacheRepository(ISimpleToDoDataAccess dbRepo)
        {
            NameValueCollection cacheConfig = new NameValueCollection();

            cacheConfig.Add("pollingInterval", "06:00:00");

            _toDoCache = new MemoryCache(TODOCACHEIDENT, cacheConfig);

            _dbRepo = dbRepo;

            PreCache();
        }

        public void PreCache()
        {
            CacheSimpleToDo();
        }

        public Dictionary<ToDoItem, List<SimpleTask>> CacheSimpleToDo()
        {

            if (!_toDoCache.Contains(TODOCACHEIDENT))
            {

                SimpleToDoResult allSimpleTodos = _dbRepo.GetAllSimpleToDoItems();

                if (allSimpleTodos != null && allSimpleTodos.SimpleToDoItems != null)
                {
                    CacheItemPolicy _policy = new CacheItemPolicy();

                    _policy.Priority = System.Runtime.Caching.CacheItemPriority.NotRemovable;
                    _policy.AbsoluteExpiration = DateTime.Now.AddHours(6);

                    _toDoCache.Set(TODOCACHEIDENT, allSimpleTodos.SimpleToDoItems, _policy);

                    return allSimpleTodos.SimpleToDoItems;
                }
                else
                    return new Dictionary<ToDoItem,List<SimpleTask>>();
            }
            else
            {
                return (Dictionary<ToDoItem, List<SimpleTask>>)_toDoCache[TODOCACHEIDENT];
            }

        }

        public ToDoItem GetCachedToDoItem(ToDoItem todoItem)
        {
            if (CacheSimpleToDo().ContainsKey(todoItem))
            {
                return CacheSimpleToDo().Keys.Where(o => o.ToDoItemId == todoItem.ToDoItemId).FirstOrDefault();
            }

            return null;
        }


        public List<ToDoItem> GetCachedToDoItems()
        {
            return CacheSimpleToDo().Keys.ToList();
        }

        public List<SimpleTask> GetCachedToDoItemTasks(ToDoItem todoItem)
        {
            if (CacheSimpleToDo().ContainsKey(todoItem))
            {
                return CacheSimpleToDo()[todoItem];
            }

            return null;
        }

        public SimpleToDoResult GetAllSimpleToDoItems()
        {

            SimpleToDoResult res = new SimpleToDoResult();

            Dictionary<ToDoItem, List<SimpleTask>> cacheContent = CacheSimpleToDo();

            if (cacheContent != null)
            {
                res.SimpleToDoItems = cacheContent;
            }


            return res;
        }

        public SimpleToDoResult GetSimpleToDoItems(Guid toDoItemId)
        {

            SimpleToDoResult res = new SimpleToDoResult();

            res.SimpleToDoItems = GetAllSimpleToDoItems().SimpleToDoItems.Where(o => o.Key.ToDoItemId == toDoItemId).ToDictionary(o => o.Key, y => y.Value);

            return res;
        }

        public BaseResult AddSimpleTodoItems(List<ToDoItem> items)
        {
            BaseResult res = new BaseResult();

            foreach (ToDoItem item in items)
            {
                res = AddSimpleTodoItem(item);
            }

            return res;
        }

        public BaseResult AddSimpleTodoItem(ToDoItem toDoItem)
        {
            BaseResult res = new BaseResult();

            if (GetCachedToDoItem(toDoItem) == null)
            {
                res = _dbRepo.AddSimpleTodoItem(toDoItem);

                CacheSimpleToDo().Add(toDoItem, new List<SimpleTask>());
            }

            return res;
        }

        public BaseResult AddSimpleTodoItemTask(Dictionary<ToDoItem, List<SimpleTask>> items)
        {
            BaseResult res = new BaseResult();
            res = _dbRepo.AddSimpleTodoItemTask(items);

            foreach (KeyValuePair<ToDoItem, List<SimpleTask>> item in items)
            {
                if (GetCachedToDoItemTasks(item.Key) == null)
                {

                    CacheSimpleToDo().Add(item.Key, item.Value);
                }

            }


            return res;
        }



    }
}
