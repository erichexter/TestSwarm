using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrowserStackWorker
{
    static class Program
    {
       // private static List<Worker> workers;
        private static BrowserStack.BrowserStack bs;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[] 
            //{ 
            //    new BrowserStackService() 
            //};
           
                
          
                  bs = new BrowserStack.BrowserStack("eric_hexter@dell.com", "erichexter");
            var tsc = new TestSwarmClient(started, finished);
            tsc.Start();
            while (true)
            {
                Thread.Sleep(1000);
            }
            tsc.Stop();
            //ServiceBase.Run(ServicesToRun);
        }

        private static void finished(long d)
        {
            Console.WriteLine("finished {0}", d); 
           
            
        }

        private static void started(long d)
        {
            Console.WriteLine("started {0}", d);
            var chrome = bs.Browsers().Where(b => b.BrowserName == "chrome").FirstOrDefault();
            bs.CreateWorker(chrome, "http://localhost:27367/run/index");
        }
    }
}
 