using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System;
using System.Web.Mvc;

namespace nTestSwarm.Areas.Api
{
    public class ApiController : AsyncController
    {
        protected ApiStringResult ApiString(Result result, string success)
        {
            return new ApiStringResult(result, success);
        }

        protected ApiVoidResult ApiVoid(Result result)
        {
            return new ApiVoidResult(result);
        }

        protected ActionResult HandleBusResult<TResult>(TResult result) where TResult : Result
        {
            return HandleBusResult(result, null);
        }

        protected ActionResult HandleBusResult<TResult>(TResult result, Func<TResult, ActionResult> successAction) where TResult : Result
        {
            //TODO: revisit result handling
            if (successAction == null)
                successAction = _ => View();

            if (result.HasException)
                return ApiVoid(result);
            else
                return successAction(result);
        }
    }
}