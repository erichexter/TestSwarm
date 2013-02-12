using System;

namespace nTestSwarm.Application.Infrastructure.Threading
{
    public interface IThreadPool
    {
        void Queue(Action action);
    }
}