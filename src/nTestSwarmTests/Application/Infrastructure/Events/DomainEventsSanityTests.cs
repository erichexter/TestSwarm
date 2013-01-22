using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using nTestSwarm.Application.Infrastructure.DomainEventing;

namespace nTestSwarmTests.Application.Infrastructure.Events
{
    [TestFixture]
    public class DomainEventsSanityTests
    {
        [Test]
        public void All_domain_events_should_not_have_complex_type_member()
        {
            var det = from type in Assembly.GetAssembly(typeof(IDomainEvent)).GetTypes()
                      where typeof(IDomainEvent).IsAssignableFrom(type)
                      select new { type, members = type.GetProperties(BindingFlags.Public | BindingFlags.Instance) };

            foreach (var d in det)
            {
                var failures = new List<string>();

                foreach (var member in d.members)
                {
                    var isSimple = TypeDescriptor.GetConverter(member.PropertyType).CanConvertFrom(typeof(string));

                    if (!isSimple)
                    {

                        failures.Add(string.Format("{0}.{1} is a complex type, and it needs to be simple for json serialization", d.type.Name, member.Name));
                    }

                    if (d.type.GetConstructor(new Type[] { }) == null)
                    {
                        failures.Add(string.Format("{0} must have a public parameter-less constructor for deserialization", d.type.Name));
                    }
                }

                if (failures.Any())
                    Assert.Fail(string.Join("\n\t", failures.ToArray()));
            }
        }
    }
}