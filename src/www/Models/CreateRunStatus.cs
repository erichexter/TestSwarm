namespace nTestSwarm.Models
{
    public class CreateRunStatus
    {
        public int RunId { get; set; }
        public int ClientId { get; set; }
        public int Fail { get; set; }
        public int Error { get; set; }
        public int Total { get; set; }
        public string Results { get; set; }

        public bool RepresentsAPassingRun()
        {
            return Total > 0 && Error == 0 && Fail == 0;
        }
    }
}