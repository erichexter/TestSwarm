using System.Text;
using System.Web.Mvc;
using nTestSwarm.Application;
using nTestSwarm.Application.Commands.ClientCreation;
using nTestSwarm.Application.Commands.CompletedRun;
using nTestSwarm.Application.Commands.RunResetting;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.NextRun;
using nTestSwarm.Application.Queries.RunStatus;
using nTestSwarm.Areas.Api;
using nTestSwarm.Models;

namespace nTestSwarm.Controllers
{
    public class RunController : ApiController
    {
        readonly IBus _bus;

        public RunController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var model = _bus.Request(new CreateClient
                {
                    Browser = Request.Browser.Browser,
                    Version = Request.Browser.MajorVersion,
                    IpAddress = Request.GetIpAddress(),
                    OperatingSystem = Request.Browser.Platform
                });

            return View(model.Data);
        }

        //[HttpPost]
        //public void GetRunAsync(long client_id)
        //{
        //    AsyncManager.OutstandingOperations.Increment();

        //    _bus.AsyncRequest(new NextRunQuery(client_id), result =>
        //    {
        //        AsyncManager.Parameters["result"] = result;
        //        AsyncManager.OutstandingOperations.Decrement();
        //    });
        //}

        //public ActionResult GetRunCompleted(Result<NextRunResult> result)
        //{
        //    return ApiJson(result);
        //}

        [HttpPost]
        public string SaveRun(CreateRunStatus input)
        {
            _bus.Send(new CompleteRun
                {
                    Client_Id = input.ClientId,
                    Run_id = input.RunId,
                    Total = input.Total,
                    Results = input.Results,
                    Fail = input.Fail,
                    Error = input.Error
                });

            return "<script>window.top.done();</script>";
        }

        //TODO: outputcache
        [HttpGet]
        public ContentResult Status(RunStatusQuery input)
        {
            Result<RunStatusResult> result = _bus.Request(input);

            return Content(result.Data.Results, "text/html", Encoding.UTF8);
        }

        [HttpPost]
        public ApiVoidResult Reset(ResetRun input)
        {
            Result result = _bus.Send(input);
            return ApiVoid(result);
        }
    }
}