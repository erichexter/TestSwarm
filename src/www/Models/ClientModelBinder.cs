using System;
using System.Linq;

namespace nTestSwarm.Models
{
    public class ClientModelBinder : System.Web.Http.ModelBinding.IModelBinder, System.Web.Mvc.IModelBinder
    {
        public bool BindModel(System.Web.Http.Controllers.HttpActionContext actionContext, System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }

        public object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            return BindModel(name => controllerContext.RequestContext.HttpContext.Request.Headers[name]);
        }

        private ClientInputModel BindModel(Func<string,string> getHeader)
        {
            throw new NotImplementedException();
        }
    }
}