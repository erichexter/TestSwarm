using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Services;

namespace nTestSwarm.Application.Repositories
{
    public interface IUserAgentCache
    {
        UserAgent[] GetAll();
        void Invalidate();
    }

    public class UserAgentCache : IUserAgentCache
    {
        static readonly object locker = new object();
        readonly Action<UserAgent> _attacher = ua => { throw new NotImplementedException(); };
        readonly Action _refresh;
        IEnumerable<UserAgent> _list = Enumerable.Empty<UserAgent>();

        public UserAgentCache(Func<IDataBase> dataFunc)
        {
            _refresh = () =>
                           {
                               lock (locker)
                               {
                                   if (!_list.Any())
                                       _list = dataFunc().All<UserAgent>().ToArray();
                               }
                           };

            _attacher = userAgent =>
                            {
                                var data = dataFunc();
                                if (!data.All<UserAgent>().Local.Contains(userAgent))
                                {
                                    data.Entry(userAgent).State = EntityState.Unchanged;
                                }
                            };
        }

        public UserAgent[] GetAll()
        {
            _refresh();

            return _list.Yield(_attacher).ToArray();
        }

        public void Invalidate()
        {
            lock (locker)
                _list = Enumerable.Empty<UserAgent>();
        }
    }
}