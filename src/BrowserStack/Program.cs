using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrowserStack
{
    static class Program
    {
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
            var tsc = new TestSwarmClient();
            tsc.Start();
            while (true)
            {
                Thread.Sleep(1000);
            }
            tsc.Stop();
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
 