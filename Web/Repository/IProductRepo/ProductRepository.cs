using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;

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
            return await DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Accessory).Include(o => o.Type)
                .Include(o => o.Notebook).ThenInclude(o => o.OS).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.Processor).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.ScreenType)
                .Include(o => o.Smartphone).ThenInclude(o => o.OS).Include(o => o.Smartphone).ThenInclude(o => o.Processor).Include(o => o.Smartphone).ThenInclude(o => o.ChargingType).Include(o => o.Smartphone).ThenInclude(o => o.ScreenType)
                .Include(o => o.WireHeadphones).ThenInclude(o => o.ConnectionType)
                .Include(o => o.WirelessHeadphones).ThenInclude(o => o.ChargingType).AsNoTracking().FirstOrDefaultAsync(o => o.ProductId == id);
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await DB.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(int skip, int count, string Category)
        {
            return await DB.Products.Include(o => o.Type).Where(o => o.Type.Category == Category).Skip(skip).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<Product>> Search(string Pattern, int skip, int count)
        {
             return await DB.Products
            .Include(o => o.Brand).Include(o => o.Color).Include(o => o.Accessory)
            .Include(o => o.Notebook).ThenInclude(o => o.OS).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.Processor)
            .Include(o => o.Smartphone).ThenInclude(o => o.OS).Include(o => o.Smartphone).ThenInclude(o => o.Processor)
            .Include(o => o.WireHeadphones).ThenInclude(o => o.ConnectionType)
            .Include(o => o.WirelessHeadphones).ThenInclude(o => o.ChargingType)
            .Where(o => o.Name.ToLower().Contains(Pattern.ToLower())).Skip(skip).Take(count).ToListAsync();
        }

        public async Task<int> SearchCount(string Pattern)
        {
            return await DB.Products.Where(o => o.Name.ToLower().Contains(Pattern.ToLower())).CountAsync();
        }

        public async Task<bool> Update(Product modified)
        {
            Product Original = DB.Products.Where(o => o.ProductId == modified.ProductId).First();
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