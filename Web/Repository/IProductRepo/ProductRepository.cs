using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository.IProductRepo
{
    public class ProductRepository : IProductRepo<Product>
    {
        private ShopContext DB;
        public ProductRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<Product> Get(int id)
        {
            return await DB.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await DB.Products.ToListAsync();
        }

        public async Task<bool> Update(Product modified)
        {
            Product Original = DB.Products.Find(modified.ProductId);
            Original.AddDate = modified.AddDate;
            Original.Count = modified.Count;
            Original.Description = modified.Description;
            Original.Discount = modified.Discount;
            Original.DiscountDate = modified.DiscountDate;
            Original.Name = modified.Name;
            Original.Photo = modified.Photo;
            Original.Price = modified.Price;
            try
            {
                await DB.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}