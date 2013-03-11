using System.Web.Mvc;

namespace nTestSwarm.Filters
{
    public class MvcValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var viewData = filterContext.Controller.ViewData;
            var modelState = viewData.ModelState;

            if (!modelState.IsValid)
            {
                filterContext.Result = new ViewResult
                {
                    ViewData = viewData,
                    TempData = filterContext.Controller.TempData
                };
            }

            base.OnActionExecuting(filterContext);
        }
    }
}