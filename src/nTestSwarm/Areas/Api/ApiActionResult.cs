using System;
using System.Web.Mvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Areas.Api
{
    public class ApiActionResult<T> : ApiResult
    {
        readonly Result<T> _result;

        public ApiActionResult(Result<T> result, Func<T, ActionResult> success) : base(result)
        {
            _result = result;
            Success = success;
        }

        public Func<T, ActionResult> Success { get; set; }

        protected override void ExecuteSuccess(ControllerContext context)
        {
            Success(_result.Data).ExecuteResult(context);
        }
    }
}