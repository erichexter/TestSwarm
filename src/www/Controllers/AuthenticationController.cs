using System.Web.Mvc;
using nTestSwarm.Application.Commands.ValidateLogin;
using nTestSwarm.Application.Infrastructure.AspNetMvc;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;

namespace nTestSwarm.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IBus _bus;

        public AuthenticationController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new LoginInput());
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(LoginInput model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Result<bool> result =
                    _bus.Request(new ValidateLoginMessage {Username = model.Username, Password = model.Password});
                if (result.Data)
                    return LoginResult(returnUrl, model.Username);
                ModelState.AddModelError("model", "Username or Password is incorrect.");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            return new LogoutResult();
        }

        private LoginResult LoginResult(string returnUrl, string username)
        {
            return new LoginResult(returnUrl, username);
        }
    }
}