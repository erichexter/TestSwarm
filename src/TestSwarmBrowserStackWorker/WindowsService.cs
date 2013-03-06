using System.ServiceProcess;

namespace TestSwarmBrowserStackWorker
{
    internal class WindowsService : ServiceBase
    {
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
        }
        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}