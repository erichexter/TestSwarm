using System;
using System.Linq;
using System.Linq.Expressions;
using nTestSwarm.Application.Domain;

namespace nTestSwarm.Application.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(long id);
        void InsertOrUpdate(T user);
        void Delete(long id);
        void Save();
    }
}