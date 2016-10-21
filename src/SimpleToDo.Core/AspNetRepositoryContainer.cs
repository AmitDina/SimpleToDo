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
    public class AspNetRepositoryContainer : IRepositoryContainer
    {
        private readonly IDependencyResolver _ds;

        public AspNetRepositoryContainer()
        {
            _ds = GlobalConfiguration.Configuration.DependencyResolver;
        }

        public ISimpleToDoRepository ISimpleToDoRepository
        {
            get
            {
                return (ISimpleToDoRepository)_ds.GetService(typeof(ISimpleToDoRepository));
            }
        }
    }
}
