using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Services;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace nTestSwarm.Application.Repositories
{
    // adapted from Steve Sanderson's scaffolded Repository
    public class Repository<T> : IRepository<T> where T : Entity
    {
        readonly IDataBase _db;

        public Repository(IDataBase db)
        {
            _db = db;
        }

        public virtual IQueryable<T> All { get { return _db.All<T>(); } }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _db.All<T>();

            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual T Find(long id)
        {
            return _db.Find<T>(id);
        }

        public virtual void InsertOrUpdate(T user)
        {
            _db.Add(user);
        }

        public virtual void Delete(long id)
        {
            var found = _db.Find<T>(id);
            _db.Remove(found);
        }

        public virtual void Save()
        {
            _db.SaveChanges();
        }
    }
}