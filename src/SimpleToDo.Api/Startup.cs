using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using SimpleToDo.WebApi.Models;
using SimpleToDo.Core.Repository;
using SimpleToDo.Core;
using SimpleToDo.Data.DataAccess;
using WebApi.Api.Handlers;
using SimpleToDo.Api.formatters;
using WebApi.Api.formatters;

[assembly: OwinStartup(typeof(SimpleToDo.Api.Startup))]

namespace SimpleToDo.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.MessageHandlers.Add(new HttpsHandler());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            config.Formatters.JsonFormatter.SerializerSettings = new DefaultJsonSerializerSettings();

            config.Formatters.Add(new BrowserJsonFormatter());

            app.UseWebApi(config); 

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
