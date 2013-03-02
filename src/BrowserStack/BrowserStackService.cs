using System.ServiceProcess;

namespace BrowserStackWorker
{
    public partial class BrowserStackService : ServiceBase
    {
        private TestSwarmClient tsc;

        public BrowserStackService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            tsc = new TestSwarmClient(null,null);
            tsc.Start();
        }

        protected override void OnStop()
        {
            tsc.Stop();
        }
    }
}