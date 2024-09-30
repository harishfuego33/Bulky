using BulkyBookWeb.Data;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace BulkyBookWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db) {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public T Get(Expression<Func<T, bool>> Filter)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(Filter);
            return query.FirstOrDefault();
        }
        public void Add(T Entity)
        {
            dbSet.Add(Entity);
        }
        public void Remove(T Entity)
        {
            dbSet.Remove(Entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void RemoveRange(IEnumerable<T> Entity)
        {
            dbSet.RemoveRange(Entity);
        }

    }
}
