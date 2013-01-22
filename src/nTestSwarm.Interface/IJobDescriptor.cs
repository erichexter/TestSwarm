using System.Collections.Generic;

namespace nTestSwarm.Interface
{
    public interface IJobDescriptor
    {
        string Name { get; }
        IEnumerable<IRunDescriptor> Runs { get; }
        string SuiteId { get; set; }
    }

    public interface IRunDescriptor
    {
        string Url { get; }
        string Name { get; }
    }
}