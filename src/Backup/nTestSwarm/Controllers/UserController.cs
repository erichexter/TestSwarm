using System.Web.Mvc;
using nTestSwarm.Application.Commands.UpdateUser;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.RunStatus;
using nTestSwarm.Application.Queries.UserLookup;

namespace nTestSwarm.Controllers
{
    public class UserController:Controller
    {
        private readonly IBus _bus;

        public UserController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new NewUserInput());
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(NewUserInput input)
        {
            if(ModelState.IsValid)
            {
                var existsQuery = _bus.Request(new UserExistsQuery() {Username = input.Username});
                if(!existsQuery.Data.Exists)
                {
                    var result =
                        _bus.Request(new UpdateUser()
                                      {Username = input.Username, Password = input.Password});
                    if(!result.HasException)
                        return View("Success");
                    else
                    {
                        ModelState.AddModelError("save-error",result.Exception);
                    }
                }
                else
                {
                    ModelState.AddModelError("username","This username is already registered on the site.");
                }
            }
            return View(input);
        }

        [HttpGet]
        [Authorize]
        public ActionResult MyProfile()
        {
            Result<User> me = _bus.Request(new UserQuery{Username = User.Identity.Name});
            var model = new ViewUser(){ Username = me.Data.Username,ApiKey = me.Data.ApiKey.ToString()};
            return View(model);
        }
    }

    public class ViewUser
    {
        public string Username { get; set; }
        public string ApiKey { get; set; }
    }
}