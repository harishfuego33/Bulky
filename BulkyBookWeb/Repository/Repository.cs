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
            _db.Products.Include(u => u.Category);
        }                                                                                                               
        public T Get(Expression<Func<T, bool>> Filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(Filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var i in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(i);
                }
            }
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

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void RemoveRange(IEnumerable<T> Entity)
        {
            dbSet.RemoveRange(Entity);
        }

    }
}
