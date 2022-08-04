using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess.EntityFrameWork
{
    public class GenericRepository<T> where T : class
    {
        private DbSet<T> _entities;
        private DbContext _dbContext;

        public DbContext Context
        {
            get
            {
                return _dbContext;
            }

        }

        public GenericRepository(DbContext dbContext)
        {
            _entities = dbContext.Set<T>();
            _dbContext = dbContext;
        }

        internal IQueryable<T> GetAll()
        {
            return from current in _entities select current;
        }

        public IQueryable<T> Get(
           Expression<Func<T, bool>>? filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = _entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            
            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }


        public void Insert(T instance)
        {
            instance = _entities.Add(instance).Entity;
        }

        public void Delete(long id)
        {
            T? instance = _entities.Find(id);
            if (instance != null)
                _entities.Remove(instance);
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            _entities.RemoveRange(_entities.Where(predicate));
        }

        public void Delete(T entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _entities.Attach(entityToDelete);
            }
            _entities.Remove(entityToDelete);
        }

        public void Update(T entityToUpdate, T value)
        {
            _entities.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(value);
        }
    }
}
