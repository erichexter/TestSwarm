using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace nTestSwarm.Application.Domain
{
    public class Program : Entity
    {
        public Program(string name, string jobDescriptionUrl, int defaultMaxRuns)
        {
            Name = name;
            JobDescriptionUrl = jobDescriptionUrl;
            UserAgentsToTest = new HashSet<UserAgent>();
            Jobs = new HashSet<Job>();
            DefaultMaxRuns = defaultMaxRuns;
        }

        protected Program()
        {
        }

        [Required]
        [StringLength(50)]
        public string Name { get; protected set; }
        public int DefaultMaxRuns { get; protected set; }
        public string JobDescriptionUrl { get; protected set; }
        public virtual ICollection<UserAgent> UserAgentsToTest { get; protected set; }
        public virtual ICollection<Job> Jobs { get; protected set; }
        public string LastJobStatus { get; protected set; }
        public string LastJobResult { get; protected set; }

        public Job AddJob(string name, string suiteID)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "name is required");
            }

            //TODO: Determine if we allow dups
            if (Jobs.Any(x => x.Name == name && x.SuiteID == suiteID))
            {
                throw new InvalidOperationException("A job with that name and suite already exists for this program.");
            }

            var job = new Job(name)
            {
                SuiteID = suiteID
            };

            Jobs.Add(job);

            return job;
        }
    }
}