using System.Linq;
using System.Web;
using System.Web.Mvc;
using nTestSwarm.Application.Commands.JobCreation;
using nTestSwarm.Application.Commands.JobCreation.Copy;
using nTestSwarm.Application.Commands.JobCreation.Described;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Areas.Admin.Models;
using nTestSwarm.Areas.Api.Models;

namespace nTestSwarm.Areas.Api.Controllers
{
    public class JobController : Controller
    {
        readonly IBus _bus;

        public JobController(IBus bus)
        {
            _bus = bus;
        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpGet, ActionName("DescribeNew")]
        public ViewResult DescribeDocumentation()
        {
            return View();
        }

        [HttpPost]
        public ApiStringResult<CreateJobResult> DescribeNew(string url, string[] correlation, int? maxruns,string apikey,bool Private)
        {
            var request = new CreateJobFromDescription
                {
                    Url = url,
                    Correlation = correlation,
                    MaxRuns = maxruns,
                    ApiKey = apikey,
                    Private=Private
                };

            var result = _bus.Request(request);

            return GetResultString(result);
        }

        [HttpGet, ActionName("Create")]
        public ViewResult CreateDocumentation()
        {
            return View(new CreateJobInput());
        }

        [HttpPost]
        public ApiStringResult<CreateJobResult> Create(CreateJobInput input)
        {
            Validate();

            var createNewJob = new CreateJob
                {
                    Name = input.Name,
                    Runs = input.Runs.Select(x => new CreateJob.CreateNewRun {Name = x.Name, Url = x.Url}),
                    SuiteId = input.SuiteId,
                    ApiKey = input.ApiKey
                };

            Result<CreateJobResult> result = _bus.Request(createNewJob);

            return GetResultString(result);
        }

        void Validate()
        {
            if (!ModelState.IsValid)
            {
                string[] strings = ModelState.Select(x => x.Value).SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();
                string joined = string.Join(", ", strings);
                throw new HttpException(500, string.Format("errors: {0}", joined));
            }
        }

        static ApiStringResult<CreateJobResult> GetResultString(Result<CreateJobResult> result)
        {
            return new ApiStringResult<CreateJobResult>(result, t => t.GetStatusUrl());
        }

        [HttpPost]
        public ApiStringResult<CreateJobResult> Copy(JobCopyInputModel input)
        {
            Validate();

            var copyJob = new CopyJob(input);

            Result<CreateJobResult> result = _bus.Request(copyJob);

            return GetResultString(result);
        }
    }
}