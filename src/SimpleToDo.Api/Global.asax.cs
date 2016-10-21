using SimpleToDo.Core;
using SimpleToDo.Core.Repository;
using SimpleToDo.Data.DataAccess;
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

            string jsonDbPath = LoadSetting<string>("DB", String.Format("{0}\\App_Data\\data.json", AppDomain.CurrentDomain.BaseDirectory));

            bool cache = LoadSetting<bool>("CacheOnly", false);

            container.RegisterSingleton<ISimpleToDoRepository<Object>>(new SimpleToDoCacheRepository<Object>(new SimpleToDoFileDbDataAccess<Object>(jsonDbPath)));

            // container.RegisterSingleton<IToDoRepository<int>>(new WebApi.Core.Repository.ToDoRepository<int>());

            //  container.Register<IToDoRepository<String>, WebApi.Core.Repository.ToDoRepository<String>>();

            //container.RegisterDecorator(typeof(ICustomerRepository),
            //        typeof(CustomerDBRepository));

            //container.RegisterW
            //container.RegisterSingleton(typeof(IToDoRepository<>), new[] { typeof(ToDoRepository<>) });



            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            // DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            IRepositoryContainer<Object> repoCon = new AspNetRepositoryContainer<Object>();

            //  IRepositoryContainer<String> repoCon2 = new AspNetRepositoryContainer<String>();
        }

        #region Configuration

        public T TryParse<T>(object value)
        {

            try
            {

                if (value == null)
                {
                    return default(T);
                }

                return (T)(System.ComponentModel.TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value));

            }
            catch (Exception)
            {
                throw new Exception("Error trying to parse setting - " + value.ToString());

            }

        }

        private T LoadSetting<T>(string SettingName, T defaultVal)
        {

            if ((GetAppSettingString(SettingName)) != null)
            {

                return TryParse<T>(GetAppSettingString(SettingName));

            }
            else
            {

                return defaultVal;

            }

        }

        private string GetAppSettingString(string SettingName)
        {

            return System.Configuration.ConfigurationManager.AppSettings[SettingName];

        }

        #endregion
    }
}
