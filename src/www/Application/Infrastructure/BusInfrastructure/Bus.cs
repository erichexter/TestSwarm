using System;
using System.ComponentModel;
using System.Web;
using Elmah;
using IContainer = StructureMap.IContainer;

namespace nTestSwarm.Application.Infrastructure.BusInfrastructure
{
    public class Bus : IBus
    {
        readonly IContainer _container;

        public Bus(IContainer container)
        {
            _container = container;
        }

        public Result<TResponse> Request<TResponse>(IRequest<TResponse> request)
        {
            var result = new Result<TResponse>();

            var handlerType = typeof (IHandler<,>).MakeGenericType(request.GetType(), typeof (TResponse));
            var handler = _container.TryGetInstance(handlerType);
            if (handler == null) throw new Exception(string.Format("handler not found for message of type {0}", request.GetType().Name));
            var helperType = typeof (Helper<,>).MakeGenericType(request.GetType(), typeof (TResponse));
            var helper = ((Helper) Activator.CreateInstance(helperType));

            try
            {
                result.Data = (TResponse) helper.ExecuteHandler(handler, request);
            }
            catch (Exception e)
            {
                result.Exception = e;
            }

            return result;
        }

        public Result Send<TMessage>(TMessage message)
        {
            var result = new Result();
            var allInstances = _container.GetAllInstances<IHandler<TMessage>>();

            foreach (var handler in allInstances)
            {
                try
                {
                    handler.Handle(message);
                }
                catch (Exception e)
                {
                    result.Exception = e;
                    LogException(message, e);
                }
            }

            return result;
        }

        public void AsyncRequest<TResponse>(IRequest<TResponse> request, Action<Result<TResponse>> onCompleted)
        {
            var worker = new BackgroundWorker {WorkerSupportsCancellation = false, WorkerReportsProgress = false};

            worker.RunWorkerCompleted += (sender, args) =>
            {
                if (onCompleted != null)
                {
                    onCompleted((Result<TResponse>) args.Result);
                }
            };

            worker.DoWork += (sender, args) => { args.Result = Request(request); };

            worker.RunWorkerAsync(request);
        }

        static void LogException<TMessage>(TMessage message, Exception e)
        {
            var exception = new BusException(message, e);
            ErrorLog.GetDefault(HttpContext.Current).Log(new Error(exception, HttpContext.Current));
        }

        interface Helper
        {
            object ExecuteHandler(object handler, object query);
        }

        class Helper<TRequest, TResponse> : Helper where TRequest : IRequest<TResponse>
        {
            public object ExecuteHandler(object handler, object query)
            {
                return ((IHandler<TRequest, TResponse>) handler).Handle((TRequest) query);
            }
        }
    }
}