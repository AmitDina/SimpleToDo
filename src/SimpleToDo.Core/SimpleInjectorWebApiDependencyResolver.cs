using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace SimpleToDo.Core
{
    public sealed class SimpleInjectorWebApiDependencyResolver : IDependencyResolver
    {

        private readonly Container _container;


        public SimpleInjectorWebApiDependencyResolver(Container container)
        {

            _container = container;

        }

        [DebuggerStepThrough()]
        public IDependencyScope BeginScope()
        {

            return this;

        }

        [DebuggerStepThrough()]
        public object GetService(Type serviceType)
        {

            return ((IServiceProvider)_container).GetService(serviceType);

        }

        [DebuggerStepThrough()]
        public IEnumerable<object> GetServices(Type serviceType)
        {

            try
            {
                return _container.GetAllInstances(serviceType);
            }
            catch (Exception ex)
            {
                return new List<object>();
            }

        }

        [DebuggerStepThrough()]
        public void Dispose()
        {

        }

    }
}
