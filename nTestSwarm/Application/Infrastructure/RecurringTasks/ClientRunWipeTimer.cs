using System;
using System.Collections.Generic;
using System.Threading;
using StructureMap;
using WebActivator;
using nTestSwarm.Application.Commands.DataCleanup;
using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.Infrastructure.RecurringTasks;

[assembly: PostApplicationStartMethod(typeof (ClientRunWipeTimer), "Start")]
//[assembly: PostApplicationStartMethod(typeof(JobCompletedEventTimer), "Start")]

namespace nTestSwarm.Application.Infrastructure.RecurringTasks
{
    // http://haacked.com/archive/2011/10/16/the-dangers-of-implementing-recurring-background-tasks-in-asp-net.aspx
    public static class ClientRunWipeTimer
    {
        static readonly Timer _timer = new Timer(OnTimerElapsed);
        static readonly JobHost _jobHost = new JobHost();

        public static void Start()
        {
            _timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(5000));
        }

        static void OnTimerElapsed(object sender)
        {
            _jobHost.DoWork(() =>
            {
                var bus = ObjectFactory.GetInstance<IBus>();
                bus.Send(new Wipe());
            });
        }
    }

    public static class JobCompletedEventTimer
    {
        static readonly Timer _timer = new Timer(OnTimerElapsed);
        static readonly JobHost _jobHost = new JobHost();
        
        public static void Start()
        {
            _timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(10000));
        }

        static void OnTimerElapsed(object state)
        {
            _jobHost.DoWork(() =>
            {
                var distributor = ObjectFactory.GetInstance<IJobCompletedEventDistributor>();
                distributor.DistributeAccumlatedJobCompletedEvents();
            });
        }
    }
}