using nTestSwarm.Interface;
using System.Collections.Generic;

namespace $rootnamespace$.Models
{
    public class JobDescriptor : IJobDescriptor
    {
        private readonly List<IRunDescriptor> _runs = new List<IRunDescriptor>();

        public string Name
        {
            get { return "$rootnamespace$"; }
        }

        public IEnumerable<IRunDescriptor> Runs
        {
            get { return _runs; }
        }

        public IRunDescriptor AddRun(string name, string url)
        {
            var run = new RunDescriptor(name, url);
            
            _runs.Add(run);
            
            return run;
        }
    }
}