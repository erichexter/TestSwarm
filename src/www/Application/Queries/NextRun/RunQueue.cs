using System;
using System.Collections.Concurrent;
using System.Linq;
using nTestSwarm.Application.Data;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Repositories;

namespace nTestSwarm.Application.Queries.NextRun
{
    public class CachingRunQueue : IRunQueue
    {
        readonly INextRunCache _cache;
        readonly nTestSwarmContext _context;

        public CachingRunQueue(INextRunCache cache, nTestSwarmContext context)
        {
            _cache = cache;
            _context = context;
        }

        public Run GetNext(Client client)
        {
            var userAgentId = client.UserAgentId;

            var run = _cache[userAgentId];

            if (run == null)
            {
                return null;
            }

            var databaseRun = GetRun(run);
            databaseRun.BeginClientRun(client);
            _context.SaveChanges();

            return databaseRun;
        }

        Run GetRun(Run run)
        {
            return _context.AllIncluding<Run>(x => x.RunUserAgents.Select(r => r.UserAgent), x => x.ClientRuns, x => x.Job).Single(x => x.Id == run.Id);
        }
    }

    public class NextRunQueue
    {
        readonly Func<Run[]> _enqueue;
        readonly ConcurrentQueue<Run> _innerQueue;

        public NextRunQueue(Func<Run[]> enqueue)
        {
            _enqueue = enqueue;
            _innerQueue = new ConcurrentQueue<Run>();
        }

        public Run GetNext()
        {
            Run run;
            if (_innerQueue.TryDequeue(out run))
            {
                return run;
            }
            var freshRuns = _enqueue();
            if (freshRuns != null && freshRuns.Length > 0)
                freshRuns.Where(x => x != null).Each(_innerQueue.Enqueue);
            else
            {
                return null;
            }
            return GetNext();
        }
    }

    public class NextRunCache : INextRunCache
    {
        readonly ConcurrentDictionary<long, NextRunQueue> _innerHash;

        public NextRunCache()
        {
            _innerHash = new ConcurrentDictionary<long, NextRunQueue>();
        }

        public Run this[long i]
        {
            get
            {
                if (_innerHash.ContainsKey(i))
                {
                    return _innerHash[i].GetNext();
                }
                return null;
            }
        }
        
        public void Set(long key, Func<Run[]> lazyRuns)
        {
            _innerHash.AddOrUpdate(key, new NextRunQueue(lazyRuns), (k, q) => new NextRunQueue(lazyRuns));
        }
    }

    public interface INextRunCache
    {
        Run this[long i] { get; }
        void Set(long key, Func<Run[]> lazyRuns);
    }

    public class NextRunCachePrimingSingleton
    {
        readonly Func<nTestSwarmContext> _db;
        readonly LazyRunFunction _function;
        readonly IUserAgentCache _userAgents;

        public NextRunCachePrimingSingleton(IUserAgentCache userAgents, Func<nTestSwarmContext> db)
        {
            _userAgents = userAgents;
            _db = db;
            _function = new LazyRunFunction();
        }

        public NextRunCache NewCache()
        {
            var userAgentIds = _userAgents.GetAll().Select(x => x.Id);

            var cache = new NextRunCache();
            
            userAgentIds.Each(id => cache.Set(id, _function.GetHydrationFunction(id, _db)));
            
            return cache;
        }

        class LazyRunFunction
        {
            public Func<Run[]> GetHydrationFunction(long id, Func<nTestSwarmContext> db)
            {
                return () =>
                {
                    var dataBase = db();

                    var runss = (from runs in dataBase.Runs.AsNoTracking()
                                 join rua in dataBase.RunUserAgents.AsNoTracking() on runs.Id equals rua.RunId
                                 where rua.RemainingRuns > 0
                                       && rua.UserAgentId == id
                                 orderby rua.RunStatus, runs.Name, runs.Created
                                 select runs).Take(10);

                    Run[] array = runss.ToArray();
                    return array;
                };
            }
    }

    
    }
}