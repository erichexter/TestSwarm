using nTestSwarm.Application;
using nTestSwarm.Application.Commands.JobQueueing;
using nTestSwarm.Application.Commands.ProgramCreation;
using nTestSwarm.Application.Commands.ProgramUpdate;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.GetProgram;
using nTestSwarm.Application.Queries.GetProgramDescriptors;
using nTestSwarm.Application.Queries.GetProgramDetails;
using nTestSwarm.Application.Queries.ProgramList;
using nTestSwarm.Application.Queries.UserAgentList;
using nTestSwarm.Areas.Api.Models;
using nTestSwarm.Filters;
using System.Linq;
using System.Web.Mvc;

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
                    UserAgents = r.Data
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

        public ActionResult QueueJob(long? id)
        {
            return HandleBusResult(_bus.Request(new ProgramDescriptorsQuery()), r =>
            {
                var programs = r.Data.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name, Selected = x.Id == id })
                                    .ToList();

                programs.Insert(0, new SelectListItem { Text = "(Select)" });

                var viewModel = new QueueJobForProgramViewModel 
                {
                    ProgramId = id ?? 0,
                    Programs = programs
                };

                return View(viewModel);
            });
        }

        [HttpPost]
        public ActionResult QueueJob(QueueJobForProgram command)
        {
            if (ModelState.IsValid)
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
            else
            {
                return QueueJob((long?)null);
            }
        }

        public ActionResult LatestJob(int id)
        {

            return View();
        }

    }
}
