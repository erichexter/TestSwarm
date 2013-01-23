using System.Web.Mvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Areas.Api
{
    public class ApiVoidResult : ApiResult
    {
        public ApiVoidResult(Result result) : base(result)
        {
        }

        protected override void ExecuteSuccess(ControllerContext context)
        {
        }
    }
}