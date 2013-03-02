using System;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace BrowserStackWorker
{
    public class TestSwarmClient
    {
        private readonly Action<long> _started;
        private readonly Action<long> _finished;

        private HubConnection _connection;
        private IHubProxy _job;
        private readonly string _testswarmUrl = "http://localhost:27367/";

        public TestSwarmClient(Action<long> started,Action<long> finished,string testswarmUrl)
        {
            _started = started;
            _finished = finished;
            _testswarmUrl = testswarmUrl;
        }

        public void Start()
        {
            _connection = new HubConnection(_testswarmUrl);
            _job=_connection.CreateHubProxy("JobStatusHub");   
            _job.On("started",onStarted);
            _job.On("finished", onFinished);
            _connection.Start();
        }
        private void onFinished(dynamic d)
        {
            _finished(d);
        }
        private void onStarted(dynamic d)
        {
            _started(d);
            
        }

        public void Stop()
        {
            _connection.Stop();
            
        }
    }
}