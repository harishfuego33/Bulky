using Microsoft.AspNetCore.Mvc.Formatters;
using System.Linq.Expressions;

namespace BulkyBookWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string ?includeProperty = null);
        T Get(Expression<Func<T, bool>> filter,string ?includeProperty=null);
        void Add(T Entity);
        void Remove(T Entity);
        void RemoveRange(IEnumerable<T> Entity);


    }
}
