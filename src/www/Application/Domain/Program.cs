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
        public string Name { get; set; }
        public int DefaultMaxRuns { get; set; }
        public string JobDescriptionUrl { get; set; }
        public virtual ICollection<UserAgent> UserAgentsToTest { get; protected set; }
        public virtual ICollection<Job> Jobs { get; protected set; }
        public string LastJobStatus { get; protected set; }
        public string LastJobResult { get; protected set; }

        public Job AddJob(string name, string correlation)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "name is required");
            }

            var job = new Job(name)
            {
                Correlation = correlation
            };

            Jobs.Add(job);

            return job;
        }
    }
}