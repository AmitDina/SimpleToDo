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
    public class SimpleToDoCacheRepository<T> : ISimpleToDoRepository<T> where T : class
    {
        private const string TODOCACHEIDENT = "TODOCACHE";

        private MemoryCache _toDoCache;

        private ISimpleToDoDataAccess<T> _dbRepo;

        public SimpleToDoCacheRepository(ISimpleToDoDataAccess<T> dbRepo)
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

        public List<ToDoItem<T>> CacheSimpleToDo()
        {

            if (!_toDoCache.Contains(TODOCACHEIDENT))
            {

                SimpleToDoResult<T> allSimpleTodos = _dbRepo.GetAllSimpleToDoItems();

                if (allSimpleTodos != null && allSimpleTodos.Items != null && allSimpleTodos.Success)
                {
                    CacheItemPolicy _policy = new CacheItemPolicy();

                    _policy.Priority = System.Runtime.Caching.CacheItemPriority.NotRemovable;
                    _policy.AbsoluteExpiration = DateTime.Now.AddHours(6);

                    _toDoCache.Set(TODOCACHEIDENT, allSimpleTodos.Items, _policy);

                    return allSimpleTodos.Items;
                }
                else
                    return null;
            }
            else
            {
                return (List<ToDoItem<T>>)_toDoCache[TODOCACHEIDENT];
            }

        }

        public List<ToDoItem<T>> GetCachedToDoItems(ToDoItem<T> todoItem)
        {
            if (CacheSimpleToDo().Contains(todoItem))
            {
                return CacheSimpleToDo().Where(o => o.ToDoItemId == todoItem.ToDoItemId).ToList();
            }


            return null;
        }

        public SimpleToDoResult<T> GetAllSimpleToDoItems()
        {

            SimpleToDoResult<T> res = new SimpleToDoResult<T>();

            List<ToDoItem<T>> cacheContent = CacheSimpleToDo();

            if (cacheContent != null)
            {
                res.Items = cacheContent;

                res.Success = true;
            }


            return res;
        }

        public SimpleToDoResult<T> GetSimpleToDoItems(Guid toDoItemId)
        {
            throw new NotImplementedException();
        }

        public BaseResult AddSimpleTodoItems(List<ToDoItem<T>> items)
        {
            throw new NotImplementedException();

        }

        public BaseResult AddSimpleTodoItem(ToDoItem<T> toDoItem)
        {
            BaseResult res = new BaseResult();

            if (GetCachedToDoItems(toDoItem) == null)
            {
                res = _dbRepo.AddSimpleTodoItems(new List<ToDoItem<T>>() { toDoItem });

                CacheSimpleToDo().Add(toDoItem);
            }

            return res;
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
