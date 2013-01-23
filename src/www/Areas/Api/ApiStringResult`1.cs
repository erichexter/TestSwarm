using System;
using System.Web.Mvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Areas.Api
{
    public class ApiStringResult<T> : ApiResult
    {
        readonly Result<T> _result;
        readonly Func<T, string> _successString;

        public ApiStringResult(Result<T> result, Func<T, string> successString) : base(result)
        {
            _result = result;
            _successString = successString;
        }

        protected override void ExecuteSuccess(ControllerContext context)
        {
            if (_result != null && _successString != null)
            {
                var successString = _successString(_result.Data);
                context.HttpContext.Response.Write(successString);
            }
        }
    }
}