using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            
        }

        private static void started(long obj)
        {
            var bs = new BrowserStack.BrowserStack("eric_hexter@dell.com", "erichexter");
            foreach (var b in bs.Browsers())
            {
                Console.WriteLine("{0} {1}", b.BrowserName, b.BrowserVersion);
            }
            
        }
    }
}
