using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System.Web.Mvc;

namespace nTestSwarm.Areas.Api
{
    public class ApiController : AsyncController
    {
        protected ApiStringResult ApiString(Result result, string success)
        {
            return new ApiStringResult(result, success);
        }

        protected ApiJsonResult<T> ApiJson<T>(Result<T> result) where T : class
        {
            return new ApiJsonResult<T>(result);
        }

        protected ApiVoidResult ApiVoid(Result result)
        {
            return new ApiVoidResult(result);
        }
    }
}