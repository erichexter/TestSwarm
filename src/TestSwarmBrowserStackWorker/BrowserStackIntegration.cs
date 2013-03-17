using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BrowserStack;
using Newtonsoft.Json;

namespace TestSwarmBrowserStackWorker
{
    public class BrowserStackIntegration
    {
        private readonly string _password;
        private readonly string _testSwarmUrl;
        private readonly string _username;
        private readonly List<Worker> _workers = new List<Worker>();

        public BrowserStackIntegration(string testSwarmUrl, string username, string password)
        {
            _testSwarmUrl = testSwarmUrl;
            _username = username;
            _password = password;
        }

        public async void started(long obj)
        {
            var client = new HttpClient();
            Task<JobClientsNeeded> result =
                await
                client.GetAsync (_testSwarmUrl + @"api/neededclients")
                      .ContinueWith(t => t.Result.Content.ReadAsAsync<JobClientsNeeded>());

            var bs = new BrowserStack.BrowserStack(_username, _password);
            List<Useragent> agentsNeeded =
                bs.Browsers()
                  .Select(b => new Useragent {Browser = b.BrowserName, Version = b.BrowserVersion})
                  .Intersect(result.Result.UserAgents)
                  .ToList();

            foreach (Useragent b in agentsNeeded)
            {
                Console.WriteLine("{0} {1}", b.Browser, b.Version);
                Browser browser =
                    bs.Browsers()
                      .First(f => f.BrowserName == b.Browser && f.BrowserVersion == b.Version && f.OsName == "windows");
                _workers.Add(bs.CreateWorker(browser, result.Result.ClientUrl));
            }
        }

        public void finished(long obj)
        {
            foreach (Worker worker in _workers)
            {
                worker.Terminate();
            }
        }
    }
}