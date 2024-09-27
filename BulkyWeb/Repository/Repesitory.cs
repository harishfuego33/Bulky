using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace BulkyWeb.Repository
{
    public class Repesitory<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repesitory(ApplicationDbContext db) {
            _db = db;
            dbSet = _db.Set<T>();
        }
        public IEnumerable GetAll()
        {
            
        }
        public T Get(Expression<Func<T, bool>> Filter)
        {

        }
        public void Add(T Entity)
        {
            dbSet.Add(Entity);
        }
        public void Remove(T Entity)
        { 
        }
        public void RemoveRange(IEnumerable<T> Entity)
        {

        }
    }
}
