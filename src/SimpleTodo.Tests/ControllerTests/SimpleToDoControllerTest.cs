using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using SimpleToDo.WebApi.Models;
using SimpleToDo.Core.Repository;
using SimpleToDo.Core;
using SimpleToDo.Data.DataAccess;
using SimpleToDo.Api.Controllers;
using System.Net.Http;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace SimpleTodo.Tests.ControllerTests
{
    [TestClass]
    public class SimpleToDoControllerTest
    {

        protected internal IRepositoryContainer _repoCon;


        public SimpleToDoControllerTest()
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            SimpleInjector.Container container = new SimpleInjector.Container();

            string jsonDbPath = SimpleToDoConfig.LoadSetting<string>("DB", String.Format("{0}\\data.json", AppDomain.CurrentDomain.BaseDirectory));

            container.RegisterSingleton<ISimpleToDoRepository>(new SimpleToDoCacheRepository(new SimpleToDoFileDbDataAccess(jsonDbPath)));

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            _repoCon = new AspNetRepositoryContainer();

        }



        [TestMethod]
        public async Task WhenCreatingNewSimpleToDOAndAddTasks_ShouldReturnCreated()
        {
            var controller = new SimpleToDoController(_repoCon);

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            //Test Data
            ToDoItem testTodoItem =
                SimpleToDoTestHelpers.GetDemoToDoItem();

            //Post Data
            var resultPostTodoTask =
                await controller.Post(testTodoItem);

            Assert.IsNotNull(resultPostTodoTask);
            Assert.AreEqual(HttpStatusCode.Created, resultPostTodoTask.StatusCode);

            //Test Data to add Task in above ite,
            List<SimpleTask> testTodoTaks =
                SimpleToDoTestHelpers.GetDemoTasks(testTodoItem.ToDoItemId);

            var resultTodoTasks =
                controller.Post(testTodoItem.ToDoItemId.ToString(), testTodoTaks);

            Assert.IsNotNull(resultTodoTasks);
            Assert.AreEqual(HttpStatusCode.Created, resultPostTodoTask.StatusCode);


        }

        [TestMethod]
        public async Task WhenCreatingNewSimpleToDOAndInputNotincorrectformatBadRequest()
        {
            var controller = new SimpleToDoController(_repoCon);

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            //Test Data
            ToDoItem testTodoItem = null;

            //Post Data
            var resultPostTodoTask =
                await controller.Post(testTodoItem);

            Assert.IsNotNull(resultPostTodoTask);
            Assert.AreEqual(HttpStatusCode.BadRequest, resultPostTodoTask.StatusCode);

        }

        [TestMethod]
        public void WhenGettingTodoItemsShouldReturnWithOKStatusCode()
        {
            var controller = new SimpleToDoController(_repoCon);

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();


            SimpleToDoResult repoResult =
                _repoCon.ISimpleToDoRepository.GetAllSimpleToDoItems();

            SimplePagedToDoResult<SimpleToDoObject> expected = new SimplePagedToDoResult<SimpleToDoObject>();

            expected.Items = repoResult.ToDoItem.ToList();

            expected.TotalCount = repoResult.ToDoItem.Count();

            expected.Success = true;

            IHttpActionResult actionResult =
                controller.GetAll();

            var contentResult =
                actionResult as OkNegotiatedContentResult<SimplePagedToDoResult<SimpleToDoObject>>;

            SimplePagedToDoResult<SimpleToDoObject> output = (SimplePagedToDoResult<SimpleToDoObject>)contentResult.Content;

            Assert.IsNotNull(output);
            Assert.ReferenceEquals(expected, output);

        }

        [TestMethod]
        public async Task WhenCreatingNewToDoAndGetting_ShouldReturnOK()
        {
            var controller = new SimpleToDoController(_repoCon);

            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();

            //Test Data
            ToDoItem testTodoItem =
                SimpleToDoTestHelpers.GetDemoToDoItem();

            //Post Data
            var resultPostTodoTask =
                await controller.Post(testTodoItem);

            Assert.IsNotNull(resultPostTodoTask);
            Assert.AreEqual(HttpStatusCode.Created, resultPostTodoTask.StatusCode);


            SimpleToDoResult expected =
                 _repoCon.ISimpleToDoRepository.GetSimpleToDoItems(testTodoItem.ToDoItemId);

            IHttpActionResult actionResult =
                controller.GetSimpleToDoById(testTodoItem.ToDoItemId.ToString());


            var contentResult =
                actionResult as OkNegotiatedContentResult<SimpleToDoResult>;

            SimpleToDoResult output = (SimpleToDoResult)contentResult.Content;

            Assert.IsNotNull(output);
            Assert.ReferenceEquals(expected, output);


        }

    }
}
