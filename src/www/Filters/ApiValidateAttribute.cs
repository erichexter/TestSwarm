using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace nTestSwarm.Filters
{
    public class ApiValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (modelState != null && !modelState.IsValid)
            {
                var response = new JObject();

                foreach (var pair in modelState)
                {
                    var key = pair.Key;
                    var errors = pair.Value.Errors;

                    if (errors.Any())
                    {
                        response[key] = new JArray(errors.Select(x => x.ErrorMessage).ToArray());
                    }
                }

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }
    }
}