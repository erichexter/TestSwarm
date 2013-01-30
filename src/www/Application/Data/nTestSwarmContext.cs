using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using nTestSwarm.Application.Domain;
using nTestSwarm.Application.Services;
using EntityState = System.Data.Entity.EntityState;

namespace nTestSwarm.Application.Data
{
    public class nTestSwarmContext : DbContext, IDataBase
    {
        public nTestSwarmContext()
        {
        }

        public nTestSwarmContext(string connection) : base(connection)
        {
        }

        public DbSet<Program> Programs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAgent> UserAgents { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<ClientRun> ClientRuns { get; set; }
        public DbSet<RunUserAgent> RunUserAgents { get; set; }
        public DbSet<RunUserAgentCompareResult> RunUserAgentCompareResults { get; set; }
        public DbSet<Event> Events { get; set; }

        public IDbSet<T> All<T>() where T : Entity
        {
            return Set<T>();
        }

        public T Find<T>(long id) where T : Entity
        {
            return Set<T>().Find(id);
        }

        public void Add(Entity item)
        {
            Set(item.GetUnproxiedType()).Add(item);
        }

        public void AddMany(params Entity[] items)
        {
            items.Each(item => Set(item.GetType()).Add(item));
        }

        public void Remove(Entity entity)
        {
            Set(entity.GetUnproxiedType()).Remove(entity);
        }

        void IDataBase.SaveChanges()
        {
            var saveChanges = SaveChanges();
        }

        DbEntityEntry<T> IDataBase.Entry<T>(T entity)
        {
            return Entry(entity);
        }

        public virtual IQueryable<T> AllIncluding<T>(params Expression<Func<T, object>>[] includeProperties)
            where T : Entity
        {
            IQueryable<T> query = All<T>();

            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public void Remove<T>(long id) where T : Entity
        {
            var found = Find<T>(id);
            Remove(found);
        }

        public override int SaveChanges()
        {
            foreach (var clientRun in ClientRuns.Local.ToList())
            {
                if (clientRun.Run == null)
                {
                    ClientRuns.Remove(clientRun);
                }
            }
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.Created = SystemTime.NowThunk();

                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                    entry.Entity.Updated = SystemTime.NowThunk();
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Program
            modelBuilder.Entity<Program>()
                .HasMany(p => p.UserAgentsToTest)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("ProgramUserAgents");
                    m.MapLeftKey("ProgramId");
                    m.MapRightKey("UserAgentId");
                });
        }
    }
}