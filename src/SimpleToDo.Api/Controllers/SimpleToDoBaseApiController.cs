using SimpleToDo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace SimpleToDo.Api.Controllers
{
    public abstract class SimpleToDoBaseApiController : ApiController
    {
        protected internal IRepositoryContainer<Object> _repoCon;

        public SimpleToDoBaseApiController()
        {
            _repoCon = new AspNetRepositoryContainer<Object>();
        }

        protected internal virtual StatusCodeResult Forbidden()
        {
            return new StatusCodeResult(HttpStatusCode.Forbidden, this);
        }
    }
}