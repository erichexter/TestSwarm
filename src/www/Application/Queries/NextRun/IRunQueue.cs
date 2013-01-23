using nTestSwarm.Application.Domain;

namespace nTestSwarm.Application.Queries.NextRun
{
    public interface IRunQueue
    {
        Run GetNext(Client client);
    }
}