using SimpleToDo.Core;
using SimpleToDo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SimpleToDo.Api.Controllers
{
    [RoutePrefix("api/v1")]
    public class SimpleToDoController : SimpleToDoBaseApiController
    {
        #region "constructor"
        public SimpleToDoController()
        {
        }

        public SimpleToDoController(IRepositoryContainer repoCon)
        {
            _repoCon = repoCon;
        }

        #endregion

        #region "GET"
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            try
            {

                SimplePagedToDoResult<SimpleToDoObject> res = new SimplePagedToDoResult<SimpleToDoObject>();

                SimpleToDoResult todos = _repoCon.ISimpleToDoRepository.GetAllSimpleToDoItems();

                if (todos != null)
                {

                    if (todos.ToDoItem != null && todos.ToDoItem.Any())
                    {

                        res.Items = todos.ToDoItem.ToList();

                        res.TotalCount = todos.ToDoItem.Count();

                        res.Success = true;

                        return Ok(res);
                    }
                    else
                    {
                        res.Message = "Simple Todo empty";

                        return Ok(res);
                    }


                }
                else
                {

                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("simpletodo")]
        public IHttpActionResult GetSimpleToDo()
        {
            try
            {

                SimplePagedToDoResult<ToDoItem> res = new SimplePagedToDoResult<ToDoItem>();

                SimpleToDoResult todos = _repoCon.ISimpleToDoRepository.GetAllSimpleToDoItems();

                if (todos != null )
                {
                     if (todos.ToDoItem != null && todos.ToDoItem.Any())
                    {
                    res.Items = todos.ToDoItems.ToList();

                    res.TotalCount = todos.ToDoItems.Count();

                    res.Success = true;

                    return Ok(res);
                    }
                     else
                     {
                         res.Message = "Simple Todo empty";

                         return Ok(res);
                     }
                }
                else
                {

                    return NotFound();
                }
            }
            catch (Exception ex)
            {
              return  InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("simpletodo/{id}")]
        public IHttpActionResult GetSimpleToDoById(string id)
        {
            try
            {
                Guid ToDoItemId;

                if (Guid.TryParse(id, out ToDoItemId))
                {
                    SimpleToDoResult todos = _repoCon.ISimpleToDoRepository.GetSimpleToDoItems(ToDoItemId);

                    if (todos != null)
                    {
                        return Ok(todos);
                    }
                    else
                    {

                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest("Invalid todo Id");
                }

            }
            catch (Exception ex)
            {
                InternalServerError(ex);
            }

            return BadRequest("Something went wrongS");
        }

        [HttpGet]
        [Route("simpletodo/{id}/tasks")]
        public IHttpActionResult GetSimpleToDoTasks(string id)
        {
            try
            {
                Guid ToDoItemId;

                if (Guid.TryParse(id, out ToDoItemId))
                {
                    SimpleToDoResult todos = _repoCon.ISimpleToDoRepository.GetSimpleToDoItems(ToDoItemId);

                    if (todos != null)
                    {
                        return Ok(todos);
                    }
                    else
                    {

                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest("Invalid todo Id");
                }

            }
            catch (Exception ex)
            {
                InternalServerError(ex);
            }

            return BadRequest("Something went wrongS");
        }

        #endregion

        #region "POST"
        [HttpPost]
        [Route("simpletodo/add")]
        public async Task<HttpResponseMessage> Post([FromBody]ToDoItem todoItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                if (todoItem != null)
                {
                    BaseResult res = _repoCon.ISimpleToDoRepository.AddSimpleTodoItem(todoItem);

                    if (res.Success)
                    {
                        return new HttpResponseMessage(HttpStatusCode.Created);

                        return new HttpResponseMessage(HttpStatusCode.Created);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.Forbidden);
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }


            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }


        }

        [HttpPost]
        [Route("simpletodo/{id}/add")]
        public async Task<HttpResponseMessage> Post([FromUri] string id, [FromBody]List<SimpleTask> values)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

                Guid ToDoItemId;

                if (Guid.TryParse(id, out ToDoItemId))
                {
                    if (values != null)
                    {

                        SimpleToDoResult todos = _repoCon.ISimpleToDoRepository.GetSimpleToDoItems(ToDoItemId);

                        if (todos != null)
                        {
                            Dictionary<ToDoItem, List<SimpleTask>> itemsToadd = new Dictionary<ToDoItem, List<SimpleTask>>();

                            itemsToadd.Add(todos.ToDoItems.FirstOrDefault(), values);

                            BaseResult res = _repoCon.ISimpleToDoRepository.AddSimpleTodoItemTask(itemsToadd);

                            if (res.Success)
                            {
                                return new HttpResponseMessage(HttpStatusCode.Created);

                            }
                            else
                            {

                                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                            }


                        }
                        else
                        {
                            return new HttpResponseMessage(HttpStatusCode.NotFound);
                        }

                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                InternalServerError(ex);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);

        }

        #endregion

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]string value)
        {
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            return StatusCode(HttpStatusCode.NotImplemented);
        }

    }
}
