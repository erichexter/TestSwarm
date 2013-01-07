using System.Web.Mvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Areas.Api
{
    public class ApiJsonResult<T> : ApiActionResult<T> where T: class 
    {
        public ApiJsonResult(Result<T> result)
            : base(result, r => new JsonResult {Data = r ?? (object) "", JsonRequestBehavior = JsonRequestBehavior.AllowGet})
        {
        }
    }
}