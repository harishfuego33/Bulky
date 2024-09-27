using System.Linq.Expressions;

namespace BulkyWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T Entity);
        void Remove(T Entity);
        void RemoveRang(IEnumerable<T> Entity);


    }
}
