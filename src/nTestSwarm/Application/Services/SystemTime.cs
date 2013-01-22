using System;

namespace nTestSwarm.Application.Services
{
    public class SystemTime : ISystemTime
    {
        public static Func<DateTime> NowThunk = () => DateTime.Now;

        DateTime ISystemTime.Now { get { return NowThunk(); } }
    }
}