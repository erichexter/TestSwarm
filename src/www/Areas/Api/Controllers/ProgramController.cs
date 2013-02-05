using nTestSwarm.Application;
using nTestSwarm.Application.Commands.ProgramCreation;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.ProgramList;
using nTestSwarm.Areas.Api.Models;
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

        //
        // GET: /Api/Program/
        [OutputCache(Duration=2)]
        public ActionResult Index()
        {
            var queryResults = _bus.Request(new ProgramListQuery());

            return View(new ProgramsViewModel { Programs = queryResults.Data });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProgramInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var message = new CreateProgram
                {
                    Name = inputModel.Name,
                    JobDescriptionUrl = inputModel.JobDescriptionUrl,
                    DefaultMaxRuns = inputModel.DefaultMaxRuns
                };

                var result = _bus.Send(message);

                if (result.HasException)
                {
                    //TODO: determine handling
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(inputModel);
            }
        }

        public ActionResult Details(long id)
        {
            return View();
        }

        public ActionResult Edit(long id)
        {

            return View(new ProgramInputModel());
        }

        [HttpPost]
        public ActionResult Edit(ProgramInputModel inputModel)
        {
            return View();
        }

        public ActionResult QueueJob()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QueueJob(int programId, string[] correlation)
        {
            if (programId < 1)
            {
                ModelState["programId"].Errors.Add("Program is required.");
            }

            var request = new QueueJobForProgram { ProgramId = programId, Correlation = correlation };
            var result = _bus.Request(request);

            if (result.HasException)
            {
                //TODO: determine handling
            }
            
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
        }
    }
}
