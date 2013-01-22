using System.Web.Mvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Areas.Api
{
    public class ApiStringResult : ApiResult
    {
        readonly string _success;

        public ApiStringResult(Result result, string success) : base(result)
        {
            _success = success;
        }

        protected override void ExecuteSuccess(ControllerContext context)
        {
            context.HttpContext.Response.Write(_success);
        }
    }
}