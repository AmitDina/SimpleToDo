using SimpleToDo.Core;
using SimpleToDo.Core.Repository;
using SimpleToDo.Data.DataAccess;
using SimpleToDo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SimpleToDo.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            SimpleInjector.Container container = new SimpleInjector.Container();

            //can specify what sort of Db to use 
            string jsonDbPath =
                SimpleToDoConfig.LoadSetting<string>("DB", String.Format("{0}\\App_Data\\data.json", AppDomain.CurrentDomain.BaseDirectory));

            bool cache = SimpleToDoConfig.LoadSetting<bool>("CacheOnly", true);

            if (cache)
            {
                // cache Repo
                container.RegisterSingleton<ISimpleToDoRepository>(new SimpleToDoCacheRepository(new SimpleToDoFileDbDataAccess(jsonDbPath)));
            }
            else
            {

                //Db Repo 
                container.RegisterSingleton<ISimpleToDoRepository>(new SimpleToDoDBRepository(new SimpleToDoFileDbDataAccess(jsonDbPath)));
            }


            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);


            IRepositoryContainer repoCon = new AspNetRepositoryContainer();

        }
    }
}
