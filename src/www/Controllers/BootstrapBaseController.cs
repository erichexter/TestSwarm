using BootstrapSupport;
using System.Collections.Generic;
using System.Web.Mvc;

namespace nTestSwarm.Controllers
{
    public class BootstrapBaseController : Controller
    {
        public void Attention(string message)
        {
            TempData.Add(Alerts.ATTENTION, message);
        }

        public void Success(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        public void Information(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        public void Error(string message)
        {
            TempData.Add(Alerts.ERROR, message);
        }

        public void Errors(IEnumerable<string> errors)
        {
            TempData.Add(Alerts.ERROR, string.Join("<br/>", errors));
        }
    }
}
