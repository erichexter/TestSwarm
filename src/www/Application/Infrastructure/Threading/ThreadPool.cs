using System;

namespace nTestSwarm.Application.Infrastructure.Threading
{
    public class ThreadPool : IThreadPool
    {
        public void Queue(Action action)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(state => action());
        }
    }
}