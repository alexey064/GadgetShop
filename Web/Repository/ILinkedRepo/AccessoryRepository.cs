using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await DB.Accessories.Include(o => o.product).ThenInclude(o => o.Brand)
            .Include(o => o.product).ThenInclude(o => o.Color).Include(o => o.product).ThenInclude(o => o.Department)
            .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Accessory> GetFull(int id)
        {
            return await DB.Accessories.Include(o => o.product).ThenInclude(o => o.Brand)
            .Include(o => o.product).ThenInclude(o => o.Color).Include(o => o.product).ThenInclude(o => o.Department)
            .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<IEnumerable<Accessory>> GetListShort(int skip, int count)
        {
            return await DB.Accessories.Include(o => o.product).Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Accessory>> GetListFull(int skip, int count)
        {
            return await DB.Accessories.Include(o => o.product).ThenInclude(o => o.Brand)
            .Include(o => o.product).ThenInclude(o => o.Color).Include(o => o.product).ThenInclude(o => o.Department)
            .Skip(skip).Take(count).ToArrayAsync();

        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Accessory accessory = DB.Accessories.Where(o => o.Id == id).First();
                DB.Products.Remove(accessory.product);
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
                var prev = DB.Accessories.Include(o => o.product).Where(o => o.Id == accessory.Id).First();
                prev.product.BrandId = accessory.product.BrandId;
                prev.product.ColorId = accessory.product.ColorId;
                prev.product.DepartmentId = accessory.product.DepartmentId;
                prev.product.Description = accessory.product.Description;
                prev.product.Discount = accessory.product.Discount;
                prev.product.DiscountDate = accessory.product.DiscountDate;
                prev.product.Name = accessory.product.Name;
                prev.product.Photo = accessory.product.Photo;
                prev.product.Price = accessory.product.Price;
                prev.product.TypeId = accessory.product.TypeId;
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
            accessory.product.AddDate = DateTime.Now;
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