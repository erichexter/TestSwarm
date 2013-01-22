using System;
using NUnit.Framework;

namespace nTestSwarmTests.Remote
{
    // NOTE AppHarbor runs Explicit tests
    //http://support.appharbor.com/discussions/problems/805-nunit-explicit-tests-not-being-ignored
//    [TestFixture]
//    public class Calling_descriptor_services : TestBase
//    {
//        [Test, Explicit]
//        public void TEST()
//        {
//            var client = new JobDescriptionClient();
//
//            var result = client.GetFrom("http://localhost:4422/testdetails/", new[] {"one"});
//
//            Console.WriteLine(result.Name);
//            foreach (var run in result.Runs)
//            {
//                Console.WriteLine("{0} {1}", run.Name, run.Url);
//            }
//        }
//    }
}