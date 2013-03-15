﻿using nTestSwarm.Application.Commands.JobCreation;
using nTestSwarm.Application.Commands.JobCreation.Described;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Queries.JobDetails;
using nTestSwarm.Application.Queries.JobStatus;
using nTestSwarm.Models;
using System.Web.Mvc;
using System.Web.UI;

namespace nTestSwarm.Controllers
{
    public class JobController : BusController
    {
        public JobController(IBus bus) : base(bus) { }

        [OutputCache(CacheProfile = "jobstatus", Location = OutputCacheLocation.Server)]
        public ActionResult Details(JobDetailsQuery query)
        {
            return Query(query, null, ex => View("NoJob", ex));
        }

        /// <summary>
        /// Determines the last job that was run (or is running) and redirects 
        /// to the Details action for the given job.
        /// </summary>
        public ActionResult Latest()
        {
            return Query(new LatestJobStatusQuery(), 
                         r => RedirectToAction("Details", new { id = r.JobId }), 
                         ex => View("NoJob"));
        }

        public ViewResult Create()
        {
            return View(new CreateJobInput());
        }

        [HttpPost]
        public ActionResult Create(CreateJob input)
        {
            return Query(input);
        }

        /// <summary>
        /// Allows a job to be created by using a job description url 
        /// that describes the structure of the job.
        /// </summary>
        public ViewResult DescribeNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DescribeNew(CreateJobFromDescription input)
        {
            return Query(input);
        }

        // empty route used for nav routes
        public ActionResult Nullo()
        {
            return View();
        }
    }
}