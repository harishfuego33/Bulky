using BulkyBookWeb.Models;

namespace BulkyBookWeb.Repository.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        public void Update(Category obj); 
    }
}
