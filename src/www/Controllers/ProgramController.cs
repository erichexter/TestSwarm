using nTestSwarm.Application.Commands.JobQueueing;
using nTestSwarm.Application.Commands.ProgramCreation;
using nTestSwarm.Application.Commands.ProgramUpdate;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.GetProgram;
using nTestSwarm.Application.Queries.GetProgramDescriptors;
using nTestSwarm.Application.Queries.GetProgramDetails;
using nTestSwarm.Application.Queries.LatestJobForProgram;
using nTestSwarm.Application.Queries.ProgramList;
using nTestSwarm.Application.Queries.UserAgentList;
using nTestSwarm.Models;
using System.Linq;
using System.Web.Mvc;

namespace nTestSwarm.Controllers
{
    public class ProgramController : BusController
    {
        public ProgramController(IBus bus) : base(bus) { }

        [OutputCache(Duration=2)]
        public ActionResult Index()
        {
            return Query(new ProgramListQuery());
        }

        public ActionResult Create()
        {
            return Query(new UserAgentQuery(), r => View(new ProgramViewModel { UserAgents = r }));
        }

        [HttpPost]
        public ActionResult Create(CreateProgram command)
        {
            //TODO: check for invalid state
            return Send(command, () => RedirectToAction("Index"));
        }

        public ActionResult Details(long id)
        {
            return Query(new ProgramDetailsQuery { ProgramId = id });
        }

        public ActionResult Edit(int id)
        {
            return Query(new ProgramQuery { ProgramId = id });
        }

        [HttpPost]
        public ActionResult Edit(UpdateProgram command)
        {
            //TODO: check for invalid state
            return Send(command, () => RedirectToAction("Index"));
        }

        public ActionResult QueueJob(long? id)
        {
            return Query(new ProgramDescriptorsQuery(), 
                         r => View(new QueueJobForProgramViewModel(id ?? 0, r)));
        }

        [HttpPost]
        public ActionResult QueueJob(QueueJobForProgram command)
        {
            if (ModelState.IsValid)
            {
                return Query(command, result =>
                {
                    if (result.HasErrors)
                    {
                        Errors(result.Errors.Select(e => e.Value));
                        
                        return RedirectToAction("QueueJob");
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
            return Query(new LatestJobForProgramQuery(id), 
                         r => RedirectToAction("Details", "Job", new { id = r.JobId }),
                         ex => View("NoJob"));
        }
    }
}
