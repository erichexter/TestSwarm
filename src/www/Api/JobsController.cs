using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace nTestSwarm.Api
{
    public class JobsController : ApiController
    {
        public void Delete(long id)
        {
        }

        [HttpGet]
        public void Remove(long id)
        {
            Delete(id);
        }

        [AcceptVerbs("PUT","GET")]
        public void Reset(long id)
        {
        }
    }
}
