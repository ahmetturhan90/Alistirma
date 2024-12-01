using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Alistirma.Infrastructure.Repository
{
    public interface IGenericRepository<T>
       where T : class
    {
        T FindById(object EntityId);
        IEnumerable<T> Select(Expression<Func<T, bool>> Filter = null);
        void Insert(T Entity);
        void Update(T Entity);
        void Delete(object EntityId);
        void Delete(T Entity);
    }
    public interface IEntity
    {

    }
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null, params Expression<Func<T, IEntity>>[] include);
        T Get(Expression<Func<T, bool>> filter, params Expression<Func<T, IEntity>>[] include);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, IEntity>>[] include);

        Task<T> GetAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, IEntity>>[] include);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()
    {
        public TEntity Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, IEntity>>[] include)
        {
            using (var context = new TContext())
            {
                var enttiy = GetirList(context.Set<TEntity>(), filter);
                for (int i = 0; i < include.Length; i++)
                {
                    enttiy = enttiy.Include(include[i]);
                }
                return enttiy.SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, IEntity>>[] include)
        {
            using (var context = new TContext())
            {
                var enttiy = GetirList(context.Set<TEntity>(), filter);
                for (int i = 0; i < include.Length; i++)
                {
                    enttiy = enttiy.Include(include[i]);
                }

                return enttiy.ToList();
            }
        }
        public IQueryable<TEntity> GetirList(DbSet<TEntity> entity, Expression<Func<TEntity, bool>> filter = null) => (filter == null ? entity : entity.Where(filter));

        public TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, IEntity>>[] include)
        {
            using (var context = new TContext())
            {
                var enttiy = GetirList(context.Set<TEntity>(), filter);
                for (int i = 0; i < include.Length; i++)
                {
                    enttiy = enttiy.Include(include[i]);
                }

                return await enttiy.ToListAsync();
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, IEntity>>[] include)
        {
            using (var context = new TContext())
            {
                var enttiy = GetirList(context.Set<TEntity>(), filter);
                for (int i = 0; i < include.Length; i++)
                {
                    enttiy = enttiy.Include(include[i]);
                }
                return await enttiy.SingleOrDefaultAsync(filter);
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }
    }
    public class Repository<T>
        : IGenericRepository<T>
        where T : class
    {
        private DbContext _context;
        private DbSet<T> _dbSet;
        public Repository(DbContext Context)
        {
            _context = Context;
            _dbSet = _context.Set<T>();
        }
        public virtual T FindById(object EntityId)
        {
            return _dbSet.Find(EntityId);
        }
        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
            {
                return _dbSet.Where(Filter);
            }
            return _dbSet;
        }
        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }
        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void Delete(object EntityId)
        {
            T entityToDelete = _dbSet.Find(EntityId);
            Delete(entityToDelete);
        }
        public virtual void Delete(T Entity)
        {
            if (_context.Entry(Entity).State == EntityState.Detached) //Concurrency için 
            {
                _dbSet.Attach(Entity);
            }
            _dbSet.Remove(Entity);
        }
    }
}
