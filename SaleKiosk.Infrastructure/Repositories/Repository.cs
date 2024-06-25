using CarRental.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRental.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }
        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }
        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public virtual IList<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression).ToList();
        }
        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
