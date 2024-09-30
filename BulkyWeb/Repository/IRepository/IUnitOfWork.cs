
using BulkyBookWeb.Repository.IRepository;

namespace BulkyBookWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category{ get; }
        public void Save();
    }
}
