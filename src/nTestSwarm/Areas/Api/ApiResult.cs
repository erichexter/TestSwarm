using System.Net;
using System.Text;
using System.Web.Mvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Areas.Api
{
    public abstract class ApiResult : ActionResult
    {
        readonly Result _result;

        protected ApiResult(Result result)
        {
            _result = result;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "text/plain";
            response.ContentEncoding = Encoding.UTF8;

            if (_result.HasException)
            {
                response.ClearHeaders();
                response.ClearContent();
                response.TrySkipIisCustomErrors = true;
                response.StatusCode = (int) HttpStatusCode.InternalServerError;

                response.Write(_result.Exception.Message);
            }
            else
            {
                ExecuteSuccess(context);
            }
        }

        protected abstract void ExecuteSuccess(ControllerContext context);
    }
}