using nTestSwarm.Application;
using nTestSwarm.Application.Commands.JobQueueing;
using nTestSwarm.Application.Commands.ProgramCreation;
using nTestSwarm.Application.Commands.ProgramUpdate;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries;
using nTestSwarm.Application.Queries.GetProgram;
using nTestSwarm.Application.Queries.GetProgramDescriptors;
using nTestSwarm.Application.Queries.GetProgramDetails;
using nTestSwarm.Application.Queries.LatestJobForProgram;
using nTestSwarm.Application.Queries.ProgramList;
using nTestSwarm.Application.Queries.UserAgentList;
using nTestSwarm.Models;
using System.Collections.Generic;
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
            return Query(new ProgramListQuery(), null, ex => View(new ProgramListResult[0]));
        }

        public ActionResult Create()
        {
            return Query(new UserAgentQuery(), userAgents => View(new ProgramViewModel(userAgents)));
        }

        [HttpPost]
        public ActionResult Create(CreateProgram command)
        {
            if (ModelState.IsValid)
                return Send(command, () => RedirectToAction("Index"));
            else
                return Query(new UserAgentQuery(), userAgents => View(ToViewModel(command, userAgents)));
        }

        public ActionResult Details(long id)
        {
            return Query(new ProgramDetailsQuery { ProgramId = id });
        }

        public ActionResult Edit(long id)
        {
            return Query(new ProgramQuery(id));
        }

        [HttpPost]
        public ActionResult Edit(UpdateProgram command)
        {
            if (ModelState.IsValid)
                return Send(command, () => RedirectToAction("Index"));
            else
                return Query(new ProgramQuery(command.ProgramId), viewModel => View(ToViewModel(command, viewModel)));
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
                         jobId => {
                             if (jobId > 0)
                                return RedirectToAction("Details", "Job", new { id = jobId });
                             else 
                                return View("NoJob"); 
                         });
        }


        private ProgramViewModel ToViewModel(CreateProgram command, IEnumerable<Descriptor> userAgents)
        {
            userAgents.SelectedBy(command.UserAgentIds);

            var viewModel = new ProgramViewModel(userAgents)
            {
                Name = command.Name,
                JobDescriptionUrl = command.JobDescriptionUrl,
                DefaultMaxRuns = command.DefaultMaxRuns
            };

            return viewModel;
        }

        private ProgramViewModel ToViewModel(UpdateProgram command, ProgramViewModel viewModel)
        {
            viewModel.Name = command.Name;
            viewModel.JobDescriptionUrl = command.JobDescriptionUrl;
            viewModel.DefaultMaxRuns = command.DefaultMaxRuns;
            viewModel.UserAgentListItems
                        .ClearSelections()
                        .SelectedBy(command.UserAgentIds);

            return viewModel;
        }
    }
}
