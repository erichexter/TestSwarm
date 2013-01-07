using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Should;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Queries.JobStatus;

namespace nTestSwarmTests.JobStatus
{
    public static class TestUtils
    {
        public static ResultDto ToResult(this RunUserAgent rua)
        {
            return new ResultDto
            {
                RunId = rua.Run.Id,
                RunName = rua.Run.Name,
                RunUrl = rua.Run.Url,
                JobId = rua.Run.Job.Id,
                JobName = rua.Run.Job.Name,
                UserAgentId = rua.UserAgent.Id,
                UserAgentBrowser = rua.UserAgent.Browser,
                UserAgentName = rua.UserAgent.Name,
                UserAgentVersion = rua.UserAgent.Version,
                ClientId = rua.Result.ClientId,
                CellContents = rua.Result.CellContents,
                StatusValue = rua.Result.StatusValue
            };
        }
    }

    [TestFixture]
    public class JobStatusTests : TestBase
    {
        JobStatusResult status;
        RunUserAgent runUserAgent;
        UserAgent userAgent;
        Run run;
        ResultDto result;

        [TestFixtureSetUp]
        public void When_there_is_one_run_and_one_user_agent_and_the_run_user_agent_has_not_started()
        {
            run = new Run(new Job("job"){Id = 4}, "run", "url"){Id = 1};
            userAgent = new UserAgent("chrome", "chrome", null) {Id = 2};
            runUserAgent = new RunUserAgent(run, userAgent, 1){Id = 3};

            result = runUserAgent.ToResult();
            var results = new[]
                {
                    result 
                };

            status = new JobStatusResult(results);
        }

        [Test]
        public void Should_map_browser()
        {
            var browser = status.Browsers.Single();
            browser.Browser.ShouldEqual(userAgent.Browser);
            browser.Name.ShouldEqual(userAgent.Name);
            browser.Id.ShouldEqual(userAgent.Id);
        }

        [Test]
        public void Should_map_results()
        {
            var runResult = status.RunResults.Single();
            runResult.RunId.ShouldEqual(run.Id);
            runResult.RunName.ShouldEqual(run.Name);
            runResult.RunUrl.ShouldEqual(run.Url);
        }

        [Test]
        public void Should_map_cells()
        {
            var cell = status.RunResults.Single().Cells.Single();
            cell.CellContents.ShouldEqual(result.CellContents);
            cell.Status.Value.ShouldEqual(result.StatusValue);
            cell.UserAgentBrowser.ShouldEqual(result.UserAgentBrowser);
            cell.UserAgentName.ShouldEqual(result.UserAgentName);
            cell.UserAgentVersion.ShouldEqual(result.UserAgentVersion);
            cell.ClientId.ShouldBeNull();
        }
    }

    [TestFixture]
    public class JobStatusTests_2 : TestBase
    {
        JobStatusResult status;
        UserAgent chrome;
        UserAgent ie6;
        UserAgent ie7;
        UserAgent ie8;
        Run run;
        RunUserAgent chrome_run;
        RunUserAgent ie6_run;
        RunUserAgent ie7_run;
        RunUserAgent ie8_run;
        ResultDto result1;
        ResultDto result2;
        ResultDto result3;
        ResultDto result4;

        [TestFixtureSetUp]
        public void When_there_is_one_runs_and_four_user_agents_and_the_run_user_agents_have_not_started()
        {
            run = new Run(new Job("job") { Id = 1 }, "run", "url") { Id = 2 };
            
            chrome = new UserAgent("chrome", "chrome", null) { Id = 3 };
            ie6 = new UserAgent("ie", "ie 6", 6) { Id = 11 };
            ie7 = new UserAgent("ie", "ie 7", 7) { Id = 12 };
            ie8 = new UserAgent("ie", "ie 8", 8) { Id = 13 };

            chrome_run = new RunUserAgent(run, chrome, 1) { Id = 101 };
            ie6_run = new RunUserAgent(run, ie6, 1) { Id = 102 };
            ie7_run = new RunUserAgent(run, ie7, 1) { Id = 103 };
            ie8_run = new RunUserAgent(run, ie8, 1) { Id = 104 };

            result1 = chrome_run.ToResult();
            result2 = ie6_run.ToResult();
            result3 = ie7_run.ToResult();
            result4 = ie8_run.ToResult();
            
            var results = new[]
                {
                    result3,
                    result2,
                    result4,
                    result1
                };

            status = new JobStatusResult(results);
        }

        [Test]
        public void Should_map_browser_1()
        {
            var browser = status.Browsers.First();
            browser.Browser.ShouldEqual(chrome.Browser);
            browser.Name.ShouldEqual(chrome.Name);
            browser.Id.ShouldEqual(chrome.Id);
        }

        [Test]
        public void Should_map_browser_2()
        {
            var browser = status.Browsers.ElementAt(1);
            browser.Browser.ShouldEqual(ie6.Browser);
            browser.Name.ShouldEqual(ie6.Name);
            browser.Id.ShouldEqual(ie6.Id);
        }

        [Test]
        public void Should_map_browser_3()
        {
            var browser = status.Browsers.ElementAt(2);
            browser.Browser.ShouldEqual(ie7.Browser);
            browser.Name.ShouldEqual(ie7.Name);
            browser.Id.ShouldEqual(ie7.Id);
        }

        [Test]
        public void Should_map_browser_4()
        {
            var browser = status.Browsers.ElementAt(3);
            browser.Browser.ShouldEqual(ie8.Browser);
            browser.Name.ShouldEqual(ie8.Name);
            browser.Id.ShouldEqual(ie8.Id);
        }

        [Test]
        public void Should_map_results()
        {
            var runResult = status.RunResults.Single();
            runResult.RunId.ShouldEqual(run.Id);
            runResult.RunName.ShouldEqual(run.Name);
            runResult.RunUrl.ShouldEqual(run.Url);
        }

        [Test]
        public void Should_map_cells()
        {
            var cell = status.RunResults.Single().Cells.First();
            cell.CellContents.ShouldEqual(result1.CellContents);
            cell.Status.Value.ShouldEqual(result1.StatusValue);
            cell.UserAgentBrowser.ShouldEqual(result1.UserAgentBrowser);
            cell.UserAgentName.ShouldEqual(result1.UserAgentName);
            cell.UserAgentVersion.ShouldEqual(result1.UserAgentVersion);
            cell.ClientId.ShouldBeNull();
        }

        [Test]
        public void Should_map_cells_2()
        {
            var cell = status.RunResults.Single().Cells.ElementAt(1);
            cell.CellContents.ShouldEqual(result2.CellContents);
            cell.Status.Value.ShouldEqual(result2.StatusValue);
            cell.UserAgentBrowser.ShouldEqual(result2.UserAgentBrowser);
            cell.UserAgentName.ShouldEqual(result2.UserAgentName);
            cell.UserAgentVersion.ShouldEqual(result2.UserAgentVersion);
            cell.ClientId.ShouldBeNull();
        }
    }

    [TestFixture]
    public class JobStatusTests_3 : TestBase
    {
        JobStatusResult status;
        UserAgent chrome;
        UserAgent ie6;
        UserAgent ie7;
        UserAgent ie8;
        Run run1;
        Run run2;
        Run run3;
        Run run4;
        
        [TestFixtureSetUp]
        public void When_there_are_three_runs_and_four_user_agents_and_the_run_user_agents_have_not_started()
        {
            run1 = new Run(new Job("job") { Id = 1 }, "run a", "url a") { Id = 2 };
            run2 = new Run(new Job("job") { Id = 1 }, "run b", "url b") { Id = 3 };
            run3 = new Run(new Job("job") { Id = 1 }, "run c", "url c") { Id = 4 };
            run4 = new Run(new Job("job") { Id = 1 }, "run d", "url d") { Id = 5 };

            chrome = new UserAgent("chrome", "chrome", null) { Id = 14 };
            ie6 = new UserAgent("ie", "ie 6", 6) { Id = 11 };
            ie7 = new UserAgent("ie", "ie 7", 7) { Id = 12 };
            ie8 = new UserAgent("ie", "ie 8", 8) { Id = 13 };

            status = new JobStatusResult(GetResults(new[] { run4, run2, run1, run3, }, new[] { chrome, ie6, ie7, ie8 }));
        }

        public IEnumerable<ResultDto> GetResults(IEnumerable<Run> runs, IEnumerable<UserAgent> allUserAgents)
        {
            return from run in runs from userAgent in allUserAgents select new RunUserAgent(run, userAgent, 1).ToResult();
        }

        [Test]
        public void Should_group_browsers()
        {
            status.Browsers.Count().ShouldEqual(4);
        }

        [Test]
        public void Should_sort_and_map_browser_1()
        {
            var browser = status.Browsers.First();
            browser.Browser.ShouldEqual(chrome.Browser);
            browser.Name.ShouldEqual(chrome.Name);
            browser.Id.ShouldEqual(chrome.Id);
        }

        [Test]
        public void Should_sort_and_map_browser_2()
        {
            var browser = status.Browsers.ElementAt(1);
            browser.Browser.ShouldEqual(ie6.Browser);
            browser.Name.ShouldEqual(ie6.Name);
            browser.Id.ShouldEqual(ie6.Id);
        }

        [Test]
        public void Should_sort_and_map_browser_3()
        {
            var browser = status.Browsers.ElementAt(2);
            browser.Browser.ShouldEqual(ie7.Browser);
            browser.Name.ShouldEqual(ie7.Name);
            browser.Id.ShouldEqual(ie7.Id);
        }

        [Test]
        public void Should_sort_and_map_browser__4()
        {
            var browser = status.Browsers.ElementAt(3);
            browser.Browser.ShouldEqual(ie8.Browser);
            browser.Name.ShouldEqual(ie8.Name);
            browser.Id.ShouldEqual(ie8.Id);
        }

        [Test]
        public void Should_group_results__one_for_each_run()
        {
            status.RunResults.Count().ShouldEqual(4);
        }

        [Test]
        public void Should_map_results()
        {
            var runResult = status.RunResults.First();
            runResult.RunId.ShouldEqual(run1.Id);
            runResult.RunName.ShouldEqual(run1.Name);
            runResult.RunUrl.ShouldEqual(run1.Url);
        }

        [Test]
        public void Should_sort_results()
        {
            status.RunResults.ElementAt(0).RunId.ShouldEqual(run1.Id);
            status.RunResults.ElementAt(1).RunId.ShouldEqual(run2.Id);
            status.RunResults.ElementAt(2).RunId.ShouldEqual(run3.Id);
            status.RunResults.ElementAt(3).RunId.ShouldEqual(run4.Id);
        }

        [Test]
        public void Should_group_cells()
        {
            status.RunResults.ElementAt(0).Cells.Count().ShouldEqual(4);
            status.RunResults.ElementAt(1).Cells.Count().ShouldEqual(4);
            status.RunResults.ElementAt(2).Cells.Count().ShouldEqual(4);
            status.RunResults.ElementAt(3).Cells.Count().ShouldEqual(4);
        }
    }
}