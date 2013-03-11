using nTestSwarm.Application;
using nTestSwarm.Application.Infrastructure.BusInfrastructure;
using System;
using System.Web.Mvc;

namespace nTestSwarm.Controllers
{
    public class BusController : BootstrapBaseController
    {
        private readonly IBus _bus;

        public BusController(IBus bus)
        {
            _bus = bus;
        }

        protected IBus Bus
        {
            get { return _bus; }
        }

        protected ActionResult Send<TMessage>() where TMessage : class, new()
        {
            return Send(new TMessage());
        }

        protected ActionResult Send<TMessage>(TMessage message, Func<ActionResult> successAction = null, Func<Exception,ActionResult> failureAction = null)
        {
            if (successAction == null)
                successAction = View;

            if (failureAction == null)
                failureAction = ex =>
                {
                    Error(ex.GetAllMessages());
                    return View();
                };

            var result = _bus.Send(message);

            if (result.HasException)
                return failureAction(result.Exception);

            return successAction();
        }

        protected ActionResult Query<TResult>(IRequest<TResult> message, Func<TResult,ActionResult> successAction = null, Func<Exception, ActionResult> failureAction = null)
        {
            if (successAction == null)
                successAction = data => View(data);

            if (failureAction == null)
                failureAction = ex =>
                {
                    Error(ex.GetAllMessages());
                    return View();
                };

            var result = _bus.Request(message);

            if (result.HasException)
                return failureAction(result.Exception);

            return successAction(result.Data);
        }
    }
}