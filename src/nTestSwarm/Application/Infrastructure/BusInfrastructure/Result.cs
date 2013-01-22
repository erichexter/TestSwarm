using System;

namespace nTestSwarm.Application.Infrastructure.BusInfrastructure
{
    public class Result<T> : Result
    {
        public T Data { get; set; }
    }

    public class Result
    {
        public Exception Exception { get; set; }

        public bool HasException { get { return Exception != null; } }
    }
}