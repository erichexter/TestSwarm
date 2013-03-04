using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrowserStackWorker;

namespace TestSwarmBrowserStackWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            var testswarmUrl = ConfigurationManager.AppSettings["testswarmurl"];


            var tsc = new TestSwarmClient(started, finished, testswarmUrl);
            tsc.Start();
            while (true)
            {
                Thread.Sleep(1000);
            }
            tsc.Stop();
         
        }

        private static void finished(long obj)
        {
            Console.WriteLine("job finished " + obj);
        }

        private static async void started(long obj)
        {
            var testswarmUrl = ConfigurationManager.AppSettings["testswarmurl"];

            
            var client = new HttpClient();
            var result =
                await
                client.GetAsync(testswarmUrl + @"Api/neededclients/index")
                      .ContinueWith(t => t.Result.Content.ReadAsAsync<Rootobject>());
                      
            foreach (var useragent in result.Result.UserAgents)
            {
                Console.WriteLine(useragent.Browser);
            }

            foreach (var job in result.Result.Jobs)
            {
                Console.WriteLine("job: " + job);
            }


            //var bs = new BrowserStack.BrowserStack("eric_hexter@dell.com", "erichexter");
            //foreach (var b in bs.Browsers())
            //{
            //    Console.WriteLine("{0} {1}", b.BrowserName, b.BrowserVersion);
            //}
            
        }


        public class Rootobject
        {
            public Useragent[] UserAgents { get; set; }
            public int[] Jobs { get; set; }
        }

        public class Useragent
        {
            public string Browser { get; set; }
            public object Version { get; set; }
        }

    }
}
