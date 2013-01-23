using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using nTestSwarm.Application.Domain;

namespace nTestSwarm.Application.Services
{
    public interface IDataBase
    {
        IDbSet<T> All<T>() where T : Entity;
        T Find<T>(long id) where T : Entity;
        void Add(Entity entity);
        void AddMany(params Entity[] items);
        void Remove(Entity entity);
        void SaveChanges();
        DbEntityEntry<T> Entry<T>(T entity) where T : Entity;
        IQueryable<T> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : Entity;
        void Remove<T>(long id) where T : Entity;
    }
}