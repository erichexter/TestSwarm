using System;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace BrowserStack
{
    public class TestSwarmClient
    {
        private HubConnection _connection;
        private IHubProxy _job;

        public TestSwarmClient()
        {
        }

        public void Start()
        {
            _connection = new HubConnection("http://localhost:27367/");
            _job=_connection.CreateHubProxy("JobStatusHub");   
            _job.On("started",d=>{ Console.WriteLine("started {0}",d);
            });

            _job.On("finished", d => Console.WriteLine("finised {0}",d));
            _connection.Start();
        }
        public void Stop()
        {
            _connection.Stop();
            
        }
    }
}