using System;

namespace nTestSwarm.Application.Services
{
    public interface ISystemTime
    {
        DateTime Now { get; }
    }
}