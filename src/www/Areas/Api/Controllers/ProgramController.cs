using nTestSwarm.Application;
using nTestSwarm.Application.Commands.ProgramCreation;
using nTestSwarm.Application.Commands.ProgramUpdate;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.ProgramList;
using nTestSwarm.Areas.Api.Models;
using nTestSwarm.Filters;
using System;
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
            return View();
        }

        [HttpPost]
        [MvcValidation]
        public ActionResult Create(CreateProgram command)
        {
            return HandleBusResult(_bus.Send(command), _ => RedirectToAction("Index"));
        }

        public ActionResult Details(int id)
        {

            return View();
        }

        public ActionResult Edit(int id)
        {

            return View(new ProgramInputModel() { Id = id });
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

        protected ActionResult HandleBusResult<TResult>(TResult result) where TResult : Result
        {
            return HandleBusResult(result, null);
        }

        protected ActionResult HandleBusResult<TResult>(TResult result, Func<TResult,ActionResult> successAction) where TResult : Result
        {
            //TODO: revisit result handling
            if (successAction == null)
                successAction = _ => View();

            if (result.HasException)
                return ApiVoid(result);
            else
                return successAction(result);
        }
    }
}
