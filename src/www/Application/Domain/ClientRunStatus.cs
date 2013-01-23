using System;

namespace nTestSwarm.Application.Domain
{
    public class ClientRunStatus : Enumeration
    {
        static readonly Func<int, int, int, string> StringEmpty = (fail, error, total) => string.Empty;

        public static ClientRunStatus NotStarted = new ClientRunStatus(0, "Not Started", StringEmpty, "notstarted notdone", RunStatusType.NotStarted);
        public static ClientRunStatus InProgress = new ClientRunStatus(1, "In Progress", StringEmpty, "progress notdone", RunStatusType.Running);
        public static ClientRunStatus Pass = new ClientRunStatus(2, "Pass", (f, e, t) => t.ToString());
        public static ClientRunStatus Fail = new ClientRunStatus(3, "Fail", (f, e, t) => string.Format("{0}/{1}", f, t));
        public static ClientRunStatus Error = new ClientRunStatus(4, "Error");
        public static ClientRunStatus Timeout = new ClientRunStatus(5, "Timeout");

        public ClientRunStatus()
        {
        }

        public ClientRunStatus(int value, string displayName, Func<int, int, int, string> func = null, 
            string css = null, RunStatusType type = RunStatusType.Finished) : base(value, displayName)
        {
            Css = css ?? displayName.ToLower();
            RunStatusCorrelation = type;
            GetContentsFunc = func ?? ((a, b, c) => DisplayName);
        }

        public string Css { get; set; }
        public RunStatusType RunStatusCorrelation { get; set; }
        private Func<int, int, int, string> GetContentsFunc { get; set; }

        public string GetContents(ClientRun clientRun)
        {
            return GetContentsFunc(clientRun.FailCount, clientRun.ErrorCount, clientRun.TotalCount);
        }

        static ClientRunStatus FromData(RunStatusType type, int failed, int errors, int total)
        {
            if (type == RunStatusType.NotStarted)
            {
                return NotStarted;
            }
            if (type == RunStatusType.Running)
            {
                return InProgress;
            }
            if (type == RunStatusType.Finished && failed == -1)
            {
                return Timeout;
            }
            if (type == RunStatusType.Finished && (errors > 0 || total == 0))
            {
                return Error;
            }
            return failed > 0 ? Fail : Pass;
        }

        public static ClientRunStatus FromClientRun(ClientRun clientRun)
        {
            return FromData(clientRun.RunStatus, clientRun.FailCount, clientRun.ErrorCount, clientRun.TotalCount);
        }
    }
}