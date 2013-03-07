using nTestSwarm.Application.NextRun;
using nTestSwarm.Models;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace nTestSwarm.Api
{
    public class ClientsController : ApiController
    {
        public void Put([ModelBinder(typeof(ClientModelBinder))] ClientInputModel input)
        {
        }

        [HttpGet]
        public NextRunResult NextRun(long id)
        {
            return null;
        }
    }
}
