using System.Linq;
using NUnit.Framework;
using Should;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Events;
using nTestSwarm.Application;
using nTestSwarm.Application.Events.JobCompletion;

namespace nTestSwarmTests.Application.JobReset
{
    [TestFixture]
    public class ResultsJoinerTests
    {
        [Test]
        public void Should_base_comparison_on_target_by_ignoring_non_matching_source_results()
        {
            var resultsJoiner = new ResultsJoiner();

            var sourceInput = new RunResult
            {
                RunName = "A",
                UserAgentName = "U",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var sourceResults = new[]
            {
                sourceInput
            };

            var targetResults = Enumerable.Empty<RunResult>();

            var correlatedResults = resultsJoiner.Join(sourceResults, targetResults);

            correlatedResults.Any().ShouldBeFalse();

            
        }

        [Test]
        public void Should_correlate_empties_with_null_source_results()
        {
            var resultsJoiner = new ResultsJoiner();

            var targetInput = new RunResult
            {
                RunName = "A",
                UserAgentName = "U",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var targetResults = new[]
            {
                targetInput
            };

            var sourceResults = Enumerable.Empty<RunResult>();

            var correlatedResults = resultsJoiner.Join(sourceResults, targetResults);

            var sourceResult = correlatedResults.Single().Source;
            var targetResult = correlatedResults.Single().Target;

            targetResult.ShouldBeSameAs(targetInput);
            sourceResult.ShouldBeType<NullResult>();
        }

        [Test]
        public void Should_handle_two_empty_input_sets()
        {
            var resultsJoiner = new ResultsJoiner();

            var correlatedResults = resultsJoiner.Join(new RunResult[] {}, new RunResult[] {});

            correlatedResults.Count().ShouldEqual(0);
        }

        [Test]
        public void Should_correlate_based_on_run_name()
        {
            var resultsJoiner = new ResultsJoiner();

            var targetInput1 = new RunResult
            {
                RunName = "A",
                UserAgentName = "U",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var targetInput2 = new RunResult
            {
                RunName = "B",
                UserAgentName = "U",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var targetInput3 = new RunResult
            {
                RunName = "C",
                UserAgentName = "U",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var sourceInput1 = new RunResult
            {
                RunName = "A",
                UserAgentName = "U",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var sourceInput2 = new RunResult
            {
                RunName = "B",
                UserAgentName = "U",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var targetResults = new[]
            {
                targetInput1, targetInput2, targetInput3
            };

            var sourceResults = new[]
            {
                sourceInput2, sourceInput1
            };
            

            var correlatedResults = resultsJoiner.Join(sourceResults, targetResults);

            correlatedResults.Single(x => x.Source == sourceInput1).Target.ShouldEqual(targetInput1);
            correlatedResults.Single(x => x.Source == sourceInput2).Target.ShouldEqual(targetInput2);
            correlatedResults.Single(x => x.Target == targetInput3).Source.ShouldBeType<NullResult>();
        }

        [Test]
        public void Should_correlate_based_on_useragent_name()
        {
            var resultsJoiner = new ResultsJoiner();

            var targetInput1 = new RunResult
            {
                RunName = "A",
                UserAgentName = "U",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var targetInput2 = new RunResult
            {
                RunName = "A",
                UserAgentName = "V",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var targetInput3 = new RunResult
            {
                RunName = "A",
                UserAgentName = "W",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var sourceInput1 = new RunResult
            {
                RunName = "A",
                UserAgentName = "U",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var sourceInput2 = new RunResult
            {
                RunName = "A",
                UserAgentName = "V",
                RunUserAgentResult = new RunUserAgentResult()
            };

            var targetResults = new[]
            {
                targetInput1, targetInput2, targetInput3
            };

            var sourceResults = new[]
            {
                sourceInput2, sourceInput1
            };


            var correlatedResults = resultsJoiner.Join(sourceResults, targetResults);

            correlatedResults.Single(x => x.Source == sourceInput1).Target.ShouldEqual(targetInput1);
            correlatedResults.Single(x => x.Source == sourceInput2).Target.ShouldEqual(targetInput2);
            correlatedResults.Single(x => x.Target == targetInput3).Source.ShouldBeType<NullResult>();
        }
    }
}