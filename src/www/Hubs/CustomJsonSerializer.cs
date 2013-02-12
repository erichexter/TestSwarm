using Microsoft.AspNet.SignalR.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace nTestSwarm.Hubs
{
    public class CustomJsonSerializer : IJsonSerializer
    {
        public object Parse(string json, Type targetType)
        {
            var serializer = Create();

            return serializer.Deserialize(new StringReader(json), targetType);
        }

        public void Serialize(object value, TextWriter writer)
        {
            var serializer = Create();

            serializer.Serialize(writer, value);
        }

        private JsonSerializer Create()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            return JsonSerializer.Create(settings);
        }
    }
}