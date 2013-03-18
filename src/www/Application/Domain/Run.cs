using nTestSwarm.Application.Services;
using System.Collections.Generic;
using System.Linq;

namespace nTestSwarm.Application.Domain
{
    public class Run : Entity, INamedEntity
    {
        public Run(Job job, string name, string url)
        {
            Job = job;
            Url = url;
            Name = name;
            RunStatus = RunStatusType.NotStarted;
            ClientRuns = new HashSet<ClientRun>();
            RunUserAgents = new HashSet<RunUserAgent>();
        }

        public Run() : this(null, string.Empty, string.Empty)
        {
        }

        public virtual Job Job { get; protected set; }
        public long JobId { get; protected set; }
        public string Name { get; protected set; }
        public string Url { get; protected set; }
        public int Status { get; protected set; }

        public RunStatusType RunStatus
        {
            get { return (RunStatusType) Status; }
            private set { Status = (int) value; }
        }

        public virtual ICollection<ClientRun> ClientRuns { get; protected set; }
        public virtual ICollection<RunUserAgent> RunUserAgents { get; protected set; }

        public void Pass(ClientRun passingRun)
        {
            RemovePreviousFailuresForThisUserAgent(passingRun.Client.UserAgent);
            PassRunUserAgent(passingRun);
            RefreshStatusBasedOnRunUserAgents();
        }

        public void Fail(ClientRun failure)
        {
            RemoveTimedOutClients(exception: failure);
            FailRunUserAgent(failure);
            RefreshStatusBasedOnRunUserAgents();
        }

        public void Reset()
        {
            RunStatus = RunStatusType.NotStarted;
        }

        public void ContinueRun()
        {
            RunStatus = RunStatusType.Running;
            Job.StartOrContinue();
        }

        public bool IsFinished()
        {
            return RunStatus == RunStatusType.Finished;
        }

        public void BeginClientRun(Client client)
        {
            client.Updated = SystemTime.NowThunk();

            if (RunStatus == RunStatusType.NotStarted)
            {
                RunStatus = RunStatusType.Running;
            }

            var clientRun = new ClientRun(client, this);
            clientRun.Start();

            ClientRuns.Add(clientRun);

            RunUserAgent runUserAgent = GetRunUserAgent(client);
            if (runUserAgent != null) runUserAgent.StartOrContinue(client);

            Job.StartOrContinue();
        }

        void RemovePreviousFailuresForThisUserAgent(UserAgent userAgent)
        {
            ClientRuns.ToArray()
                .Where(x => x.Client.UserAgent.Id == userAgent.Id && (x.TotalCount <= 0 || x.ErrorCount > 0 || x.FailCount > 0))
                .Each(RemoveClientRun);
        }

        void PassRunUserAgent(ClientRun passingRun)
        {
            Client client = passingRun.Client;
            RunUserAgent runUserAgent = GetRunUserAgent(client);
            runUserAgent.Pass(passingRun);
        }

        void FailRunUserAgent(ClientRun failure)
        {
            RunUserAgent runUserAgent = GetRunUserAgent(failure.Client);
            runUserAgent.Fail(failure);
        }

        void RemoveTimedOutClients(params ClientRun[] exception)
        {
            ClientRuns.ToArray().Except(exception)
                .Where(x => x.TotalCount < 0)
                .Each(RemoveClientRun);
        }

        void RemoveClientRun(ClientRun clientRun)
        {
            clientRun.RemoveFromRun();
            ClientRuns.Remove(clientRun);
        }

        RunUserAgent GetRunUserAgent(Client client)
        {
            return RunUserAgents.SingleOrDefault(rua => Equals(rua.UserAgent, client.UserAgent));
        }


        void RefreshStatusBasedOnRunUserAgents()
        {
            RunStatus = RunStatusType.Running;

            if (RunUserAgents.All(x => x.IsFinished()))
            {
                RunStatus = RunStatusType.Finished;
            }

            Job.RefreshStatusBasedOnRuns();
           
        }
    }
}