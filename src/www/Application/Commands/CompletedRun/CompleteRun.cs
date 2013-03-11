﻿namespace nTestSwarm.Application.Commands.CompletedRun
{
    public class CompleteRun
    {
        public long RunId { get; set; }
        public long ClientId { get; set; }
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