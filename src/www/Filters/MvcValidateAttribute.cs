using System.Web.Mvc;

namespace nTestSwarm.Filters
{
    /// <summary>
    /// Enforces validation of incoming MVC models.
    /// </summary>
    /// <remarks>
    /// Do not apply globally as it does not fit all scenarios.
    /// </remarks>
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