using nTestSwarm.Application;
using nTestSwarm.Application.Commands.ProgramCreation;
using nTestSwarm.Application.Commands.ProgramUpdate;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.GetProgram;
using nTestSwarm.Application.Queries.GetProgramDetails;
using nTestSwarm.Application.Queries.ProgramList;
using nTestSwarm.Application.Queries.UserAgentList;
using nTestSwarm.Areas.Api.Models;
using nTestSwarm.Filters;
using System.Linq;
using System.Web.Mvc;
using www.Application.Commands.JobQueueing;

namespace nTestSwarm.Areas.Api.Controllers
{
    public class ProgramController : ApiController
    {

        private readonly IBus _bus;

        public ProgramController(IBus bus)
        {
            _bus = bus;
        }

        [OutputCache(Duration=2)]
        public ActionResult Index()
        {
            return HandleBusResult(_bus.Request(new ProgramListQuery()), result => 
                View(new ProgramsViewModel { Programs = result.Data }));
        }

        public ActionResult Create()
        {
            return HandleBusResult(_bus.Request(new UserAgentQuery()), r =>
            {
                var viewModel = new ProgramViewModel  
                {
                    UserAgents = r.Data.Select(ua => new SelectListItem { Value = ua.Item1.ToString(), Text = ua.Item2 }).ToArray()
                };

                return View(viewModel);
            });
        }

        [HttpPost]
        [MvcValidation]
        public ActionResult Create(CreateProgram command)
        {
            return HandleBusResult(_bus.Send(command), _ => RedirectToAction("Index"));
        }

        public ActionResult Details(long id)
        {
            return HandleBusResult(_bus.Request(new ProgramDetailsQuery { ProgramId = id }), 
                    r => View(r.Data));
        }

        public ActionResult Edit(int id)
        {
            var query = new ProgramQuery { ProgramId = id };

            return HandleBusResult(_bus.Request(query), result => View(result.Data));
        }

        [HttpPost]
        [MvcValidation]
        public ActionResult Edit(UpdateProgram command)
        {
            return HandleBusResult(_bus.Send(command), _ => RedirectToAction("Index"));
        }

        public ActionResult QueueJob()
        {
            return View();
        }

        [HttpPost]
        [MvcValidation]
        public ActionResult QueueJob(QueueJobForProgram command)
        {
            return HandleBusResult(_bus.Request(command), result =>
            {
                if (result.Data.HasErrors)
                {
                    result.Data.Errors.Each(x => ModelState.AddModelError(x.Key, x.Value));

                    return View();
                }
                else
                {
                    //TODO: verify redirect
                    return RedirectToAction("Index");
                }
            });
        }

    }
}
