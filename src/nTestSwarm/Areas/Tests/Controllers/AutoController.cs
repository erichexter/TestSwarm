using System.Web.Mvc;

namespace nTestSwarm.Areas.Tests.Controllers
{
    public class AutoController : Controller
    {
        public ViewResult Pass()
        {
            var model = new AutoTestModel
                {
                    Error = 0, 
                    Fail = 0, 
                    Total = 1, 
                    TestDescription = "passing"
                };

            return View("AutoTest", model);
        }

        public ViewResult Fail()
        {
            var model = new AutoTestModel
            {
                Error = 0,
                Fail = 4,
                Total = 10,
                TestDescription = "failing"
            };

            return View("AutoTest", model);
        }

        public ViewResult Error()
        {
            var model = new AutoTestModel
            {
                Error = 1,
                Fail = 0,
                Total = 1,
                TestDescription = "error"
            };

            return View("AutoTest", model);
        }

        public ViewResult Timeout()
        {
            var model = new AutoTestModel
            {
                Total = -1,
                Fail = -1,
                TestDescription = "timeout"
            };

            return View("AutoTest", model);
        }
    }

    public class AutoTestModel
    {
        public int Fail { get; set; }
        public int Error { get; set; }
        public int Total { get; set; }
        public string TestDescription { get; set; }
    }
}