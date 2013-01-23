using System.Web.Mvc;
using System.Web.Security;

namespace nTestSwarm.Application.Infrastructure.AspNetMvc
{
    public class LoginResult : ActionResult
    {
        readonly string _returnUrl;
        readonly string _username;

        public LoginResult(string returnUrl, string username)
        {
            _returnUrl = returnUrl;
            _username = username;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            FormsAuthentication.SetAuthCookie(_username, false);
            if (new UrlHelper(context.RequestContext).IsLocalUrl(_returnUrl) && _returnUrl.Length > 1 &&
                _returnUrl.StartsWith("/")
                && !_returnUrl.StartsWith("//") && !_returnUrl.StartsWith("/\\"))
            {
                new RedirectResult(_returnUrl).ExecuteResult(context);
            }
            else
            {
                new RedirectResult("~/").ExecuteResult(context);
            }
        }
    }
}