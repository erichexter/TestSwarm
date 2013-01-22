using System.Web.Mvc;

namespace nTestSwarm.Models
{
    public class RunInput
    {
        public int Run_id { get; set; }
        public int Client_Id { get; set; }

        public int Fail { get; set; }
        public int Error { get; set; }
        public int Total { get; set; }

        [AllowHtml]
        public string Results { get; set; }

        public bool RepresentsAPassingRun()
        {
            return Total > 0 && Error == 0 && Fail == 0;
        }
    }
}