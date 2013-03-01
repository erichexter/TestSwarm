using System.ServiceProcess;

namespace BrowserStack
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
            tsc = new TestSwarmClient();
            tsc.Start();
        }

        protected override void OnStop()
        {
            tsc.Stop();
        }
    }
}