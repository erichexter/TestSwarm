using System;

namespace nTestSwarm.Application.Domain
{
    public class Event : Entity
    {
        public DateTime? Processed { get; set; }
        public string SerializedData { get; set; }
        public string Type { get; set; }
    }
}