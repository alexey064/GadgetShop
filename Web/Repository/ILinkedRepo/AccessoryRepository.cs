using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;

namespace Web.Repository
{
    public class AccessoryRepository : ILinkedRepo<Accessory>
    {
        ShopContext DB;
        public AccessoryRepository(ShopContext context) 
        {
            DB = context;
        }
        public async Task<Accessory> GetShort(int id)
        {
            return await DB.Accessories.Include(o => o.Product).ThenInclude(o => o.Brand)
            .Include(o => o.Product).ThenInclude(o => o.Color).Include(o => o.Product).ThenInclude(o => o.Department)
            .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Accessory> GetFull(int id)
        {
            return await DB.Accessories.Include(o => o.Product).ThenInclude(o => o.Brand)
            .Include(o => o.Product).ThenInclude(o => o.Color).Include(o => o.Product).ThenInclude(o => o.Department)
            .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<IEnumerable<Accessory>> GetListShort(int skip, int count)
        {
            return await DB.Accessories.Include(o => o.Product).Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Accessory>> GetListFull(int skip, int count)
        {
            return await DB.Accessories.Include(o => o.Product).ThenInclude(o => o.Brand)
            .Include(o => o.Product).ThenInclude(o => o.Color).Include(o => o.Product).ThenInclude(o => o.Department)
            .Skip(skip).Take(count).ToArrayAsync();

        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Accessory accessory = DB.Accessories.Include(o=>o.Product).Where(o => o.Id == id).First();
                DB.Products.Remove(accessory.Product);
                DB.Accessories.Remove(accessory);
                await DB.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Update(Accessory accessory)
        {
            if (accessory.Id == 0)
            {
               bool result=Add(accessory).Result;
                if (result)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                var prev = DB.Accessories.Include(o => o.Product).Where(o => o.Id == accessory.Id).First();
                prev.Product.BrandId = accessory.Product.BrandId;
                prev.Product.ColorId = accessory.Product.ColorId;
                prev.Product.DepartmentId = accessory.Product.DepartmentId;
                prev.Product.Description = accessory.Product.Description;
                prev.Product.Discount = accessory.Product.Discount;
                prev.Product.DiscountDate = accessory.Product.DiscountDate;
                prev.Product.Name = accessory.Product.Name;
                prev.Product.Photo = accessory.Product.Photo;
                prev.Product.Price = accessory.Product.Price;
                prev.Product.TypeId = accessory.Product.TypeId;
            }
            try
            {
                await DB.SaveChangesAsync();
                return true;
            }
            catch (Exception e) { return false; }
        }

        public async Task<bool> Add(Accessory accessory)
        {
            accessory.Product.AddDate = DateTime.Now;
            DB.Accessories.Add(accessory);
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

        public async Task<int> GetCount()
        {
            return await DB.Accessories.CountAsync();
        }
    }
}