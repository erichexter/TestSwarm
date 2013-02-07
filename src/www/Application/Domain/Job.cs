using System;
using System.Collections.Generic;
using System.Linq;
using nTestSwarm.Application.Events;
using nTestSwarm.Application.Events.JobCompletion;
using nTestSwarm.Application.Infrastructure.DomainEventing;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Domain
{
    public class Job : Entity
    {
        public Job(string name)
        {
            Name = name;
            Status = JobStatusType.Created;
            Runs = new HashSet<Run>();
        }

        public Job() : this(string.Empty)
        {
        }
        
        public string Name { get; protected set; }
        public DateTime? Started { get; protected set; }
        public DateTime? Finished { get; protected set; }
        public string SuiteID { get; set; }
        public JobStatusType Status { get; protected set; }
        public Program Program { get; set; }
        public string Correlation { get; protected set; }

        public virtual ICollection<Run> Runs { get; protected set; }

        public void Reset()
        {
            Status = JobStatusType.Created;
            Started = null;
            Finished = null;
        }

        void Complete()
        {
            Status = JobStatusType.Complete;
            Finished = SystemTime.NowThunk();
            DomainEvents.Raise(new JobCompleted(this));
        }

        void Running()
        {
            Status = JobStatusType.Running;
            Finished = null;
        }

        public void RefreshStatusBasedOnRuns()
        {
            Running();
            if (Runs.All(x => x.IsFinished()))
                Complete();
        }

        public void StartOrContinue()
        {
            Running();

            Started = Started ?? SystemTime.NowThunk();
        }

        public TimeSpan GetDuration()
        {
            if (Started == null && Finished == null)
                return new TimeSpan(0L);

            if (Started != null && Finished == null)
                return SystemTime.NowThunk() - Started.Value;

            throw new Exception("Job has finished but not started");
        }

        public bool IsComplete()
        {
            return Status == JobStatusType.Complete;
        }
    }
}