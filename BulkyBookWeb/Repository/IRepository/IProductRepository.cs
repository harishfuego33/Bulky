using BulkyBookWeb.Models;
using Microsoft.Extensions.ObjectPool;

namespace BulkyBookWeb.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product Obj);
    }
}
