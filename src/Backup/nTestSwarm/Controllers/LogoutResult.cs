using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace nTestSwarm.Controllers
{
    public class LogoutResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            FormsAuthentication.SignOut();
            new RedirectResult(new UrlHelper(context.RequestContext).Content("~/")).ExecuteResult(context);
        }
    }
}