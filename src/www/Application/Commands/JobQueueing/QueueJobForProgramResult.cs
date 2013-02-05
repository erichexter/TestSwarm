using System.Collections.Generic;

namespace www.Application.Commands.JobQueueing
{
    public class QueueJobForProgramResult
    {

        private readonly IDictionary<string, string> _errors = new Dictionary<string, string>();

        public IDictionary<string, string> Errors
        {
            get { return _errors; }
        }

        public bool HasErrors
        {
            get { return Errors.Count > 0; }
        }
    }
}