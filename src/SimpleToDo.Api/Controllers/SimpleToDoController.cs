using SimpleToDo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleToDo.Api.Controllers
{
     [RoutePrefix("api/v1")]
    public class SimpleToDoController : SimpleToDoBaseApiController
    {
        [HttpGet]
        [Route("simpletodo")]
        public IHttpActionResult Get()
        {
            try
            {
                SimpleToDoResult<Object> todos = _repoCon.ISimpleToDoRepository.GetAllSimpleToDoItems();

                if (todos.Success)
                {
                    return Ok(todos.Items);
                }
                else
                {

                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                InternalServerError(ex);
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet]
        [Route("simpletodo/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                Guid ToDoItemId;

                if (Guid.TryParse(id, out ToDoItemId))
                {
                    SimpleToDoResult<Object> todos = _repoCon.ISimpleToDoRepository.GetSimpleToDoItems(ToDoItemId);

                    if (todos.Success)
                    {
                        return Ok(todos.Items);
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

        [HttpPost]
        [Route("simpletodo/add")]
        public IHttpActionResult Post([FromBody]ToDoItem<Object> todoItems)
        {

            try
            {

                if (todoItems != null)
                {
                    BaseResult res = _repoCon.ISimpleToDoRepository.AddSimpleTodoItem(todoItems);

                    if (res.Success)
                    {
                        return Ok(res.Message);
                    }
                    else
                    {

                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest("Invalid Todo Items");
                }

            }
            catch (Exception ex)
            {
                InternalServerError(ex);
            }

            return BadRequest("Something went wrongS");

        }

        [HttpPost]
        [Route("simpletodo/{id}/add")]
        public IHttpActionResult Post([FromUri] string id, [FromBody]SimpleTask<Object> value)
        {
            try
            {
                Guid ToDoItemId;

                if (Guid.TryParse(id, out ToDoItemId))
                {
                    if (value != null)
                    {
                        // Item<Object> task = new Item<Object>() { Type = (Object)Convert.ChangeType(value.Type, typeof(Object)), Title = "Hello" };
                        BaseResult res = new BaseResult();//= _repoCon.ISimpleToDoRepository.AddSimpleTodoItem(todoItems);

                        if (res.Success)
                        {
                            return Ok(res.Message);
                        }
                        else
                        {

                            return NotFound();
                        }
                    }
                    else
                    {
                        return BadRequest("Simple Task empty");
                    }
                }
                else
                {
                    return BadRequest("Invalid simple Task Id");
                }

            }
            catch (Exception ex)
            {
                InternalServerError(ex);
            }

            return BadRequest("Something went wrongS");

        }

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
