using SimpleToDo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace SimpleToDo.Core
{
    public class AspNetRepositoryContainer<T> : IRepositoryContainer<T>
    {
        private readonly IDependencyResolver _ds;

        public AspNetRepositoryContainer()
        {
            _ds = GlobalConfiguration.Configuration.DependencyResolver;
        }

        public ISimpleToDoRepository<T> ISimpleToDoRepository
        {
            get
            {
                return (ISimpleToDoRepository<T>)_ds.GetService(typeof(ISimpleToDoRepository<T>));
            }
        }
    }
}
