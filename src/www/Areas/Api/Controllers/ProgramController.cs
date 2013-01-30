using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.ProgramList;
using nTestSwarm.Areas.Api;
using nTestSwarm.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            return View(inputModel);
        }

        public ActionResult Show(long id)
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

    }
}
