using System;
using Newtonsoft.Json;

namespace nTestSwarm.Application.Infrastructure.BusInfrastructure
{
    [Serializable]
    public class BusException : Exception
    {
        public BusException(object messageOnBus, Exception innerException)
            : base(GetMessage(messageOnBus), innerException)
        {
            MessageOnBus = messageOnBus;
        }

        protected object MessageOnBus { get; private set; }

        static string GetMessage(object messageOnBus)
        {
            var messageType = messageOnBus.GetType().FullName;
            return string.Format("Error processing {0}", messageType);
        }

        public override string ToString()
        {
            var serialized = GetMessageOnBusRepresentation();

            return string.Format("Message on bus:{0}{1}{0}{2}", Environment.NewLine, serialized, base.ToString());
        }

        string GetMessageOnBusRepresentation()
        {
            if (MessageOnBus == null) return "NULL MESSAGE";
            return JsonConvert.SerializeObject(MessageOnBus);
        }
    }
}