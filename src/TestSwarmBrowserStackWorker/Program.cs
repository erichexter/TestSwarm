using System.Configuration;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using BrowserStackWorker;

namespace TestSwarmBrowserStackWorker
{
    public class ProcessRunner
    {
        public void Run()
        {
            string testswarmUrl = ConfigurationManager.AppSettings["testswarmurl"];
            var browserStack = new BrowserStackIntegration(testswarmUrl, "eric_hexter@dell.com", "erichexter");
            var tsc = new TestSwarmClient(browserStack.started, browserStack.finished, testswarmUrl);
            tsc.Start();
            while (true)
            {
                Thread.Sleep(1000);
            }
            tsc.Stop();            
        }
    }
    internal class Program
    {
        private static void Main(string[] args)
        {

            if (System.Environment.UserInteractive)
            {
                string parameter = string.Concat(args);
                switch (parameter)
                {
                    case "--install":
                        ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                        break;
                    case "--uninstall":
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                        break;
                    default:
                        {
                            new ProcessRunner().Run();                            
                        }
                        break;
                }
            }
            else
            {
                ServiceBase.Run(new WindowsService());
            }

        }
    }
}