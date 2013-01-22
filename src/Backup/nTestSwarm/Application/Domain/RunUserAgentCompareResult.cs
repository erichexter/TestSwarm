using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace nTestSwarm.Application.Domain
{
    public class RunUserAgentCompareResult : Entity
    {
        public long SourceJobId { get; set; }
        public string SourceJobName { get; set; }
        public long TargetJobId { get; set; }
        public string TargetJobName { get; set; }
        public string RunName { get; set; }
        public string TargetRunUrl { get; set; }
        public string UserAgentName { get; set; }
        public int TransitionValue { get; set; }
        public long? ClientId { get; set; }
        public long RunId { get; set; }

        [NotMapped]
        public RunStatusTransition Transition
        {
            get {return (RunStatusTransition) Enumeration.FromValueOrDefault(typeof (RunStatusTransition), TransitionValue); } 
            set { TransitionValue = value.Value; }
        }
    }

    public class RunStatusTransition : Enumeration
    {
        public string Css { get; set; }
        readonly Func<ClientRunStatus, bool> _source;
        readonly Func<ClientRunStatus, bool> _target;

        public static RunStatusTransition NewlyFailing = new RunStatusTransition(1, "Newly Failing", x => x != ClientRunStatus.Fail, x => x == ClientRunStatus.Fail, "gradeX");
        public static RunStatusTransition NewlyErrored = new RunStatusTransition(2, "Newly Erroring", x => true, x => x == ClientRunStatus.Error, "gradeX");
        public static RunStatusTransition NewlyTimingOut = new RunStatusTransition(3, "Newly Timing Out", x => true, x => x == ClientRunStatus.Timeout, "gradeX");
        public static RunStatusTransition StillFailing = new RunStatusTransition(4, "Still Failing", x => x == ClientRunStatus.Fail, x => x == ClientRunStatus.Fail, "gradeC");
        public static RunStatusTransition NewlyPassing = new RunStatusTransition(5, "Newly Passing", x => x != ClientRunStatus.Pass, x => x == ClientRunStatus.Pass, "gradeA");
        public static RunStatusTransition StillPassing = new RunStatusTransition(6, "Still Passing", x => x == ClientRunStatus.Pass, x => x == ClientRunStatus.Pass, "gradeA");
        public static RunStatusTransition NoChange = new RunStatusTransition(7, "No Change", x => false, x => false, "gradeU");

        public RunStatusTransition()
        {
        }


        public RunStatusTransition(int value, string displayName, Func<ClientRunStatus, bool> source, Func<ClientRunStatus, bool> target, string css)
            : base(value, displayName)
        {
            Css = css;
            _source = source;
            _target = target;
        }

        public static RunStatusTransition GetTransition(RunUserAgentResult source, RunUserAgentResult target)
        {
            if (source == null) return new RunStatusTransition(0, "No Result For Source", x => false, x => false, "gradeU");
            if (target == null) return new RunStatusTransition(0, "No Result For Target", x => false, x => false, "gradeU");

            return GetAll<RunStatusTransition>().Where(x => x._source(source.Status) && x._target(target.Status)).SingleOrDefault() ?? NoChange;
        }
    }
}