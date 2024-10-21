using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using BulkyBookWeb.Repository.IRepository;

namespace BulkyBookWeb.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            //_db.Products.Update(obj);
            var dbObj = _db.Products.FirstOrDefault(i => i.Id == obj.Id);
            if (dbObj != null)
            {
                dbObj.Title = obj.Title;
                dbObj.Description = obj.Description;
                dbObj.ISBN = obj.ISBN;
                dbObj.Price = obj.Price;
                dbObj.ListPrice = obj.ListPrice;
                dbObj.Price50 = obj.Price50;
                dbObj.Price100 = obj.Price100;
                dbObj.ImageUrl = obj.ImageUrl;

            }
        }
    }
}
