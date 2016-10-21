using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using Newtonsoft.Json;
using WebApi.Api.formatters;
using SimpleToDo.Core.Repository;
using SimpleToDo.Data.DataAccess;
using WebApi.Api.Handlers;
using SimpleToDo.Core;
using System.Net.Http;
using System.Net.Http.Formatting;
using SimpleToDo.WebApi.Models;
using System.Linq;

namespace SimpleTodo.Tests.IngetrationTests
{
    [TestClass]
    public class SimpleTodoIngerationTest
    {
        private static HttpServer _server;

        private static string _baseUrl = "http://localhost:51573/";

        public SimpleTodoIngerationTest()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new HttpsHandler());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();

            config.Formatters.Add(new BrowserJsonFormatter());

            SimpleInjector.Container container = new SimpleInjector.Container();

            string jsonDbPath = 
                SimpleToDoConfig.LoadSetting<string>("DB", String.Format("{0}\\App_Data\\data.json", AppDomain.CurrentDomain.BaseDirectory));

            container.RegisterSingleton<ISimpleToDoRepository>(new SimpleToDoCacheRepository(new SimpleToDoFileDbDataAccess(jsonDbPath)));

            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            IRepositoryContainer repoCon = new AspNetRepositoryContainer();

            _server = new HttpServer(config);
        }


        public HttpRequestMessage createRequest(string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage();

            request.RequestUri = new Uri(_baseUrl + url);
           
            request.Method = method;

            return request;
        }

        public HttpRequestMessage createRequest<T>(string url, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        {
            HttpRequestMessage request = createRequest(url, mthv, method);
            request.Content = new ObjectContent<T>(content, formatter);

            return request;
        }

        [TestMethod]
        public void GetAllToDos()
        {
            var client = new HttpClient(SimpleTodoIngerationTest._server);

            var request = createRequest("api/v1/simpletodo", "application/json", HttpMethod.Get);

            using (HttpResponseMessage response = client.SendAsync(request).Result)
            {
                Assert.IsNotNull(response.Content);

                Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
            }

            request.Dispose();

            _server.Dispose();
        }



    }
}

