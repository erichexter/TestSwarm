using nTestSwarm.Application.Domain;
using System.Collections.Generic;

namespace nTestSwarm.Application.Domain
{
    public class Program : Entity
    {
        public Program(string name, string jobDescriptionUrl)
        {
            Name = name;
            JobDescriptionUrl = jobDescriptionUrl;
            UserAgentsToTest = new HashSet<UserAgent>();
            Jobs = new HashSet<Job>();
        }

        protected Program()
        {
        }

        public string Name { get; protected set; }
        public int DefaultMaxRuns { get; protected set; }
        public string JobDescriptionUrl { get; protected set; }
        public virtual ICollection<UserAgent> UserAgentsToTest { get; protected set; }
        public virtual ICollection<Job> Jobs { get; protected set; }
        public string LastJobStatus { get; protected set; }
        public string LastJobResult { get; protected set; }
    }
}