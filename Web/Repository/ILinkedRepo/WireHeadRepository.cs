using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class WireHeadRepository : ILinkedRepo<WireHeadphone>
    {
        ShopContext DB;
        public WireHeadRepository(ShopContext context) 
        {
            DB = context;
        }
        public async Task<bool> Add(WireHeadphone wire)
        {
            wire.Product.AddDate = DateTime.Now;
            DB.WireHeadphones.Add(wire);
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                WireHeadphone wire = DB.WireHeadphones.Where(o => o.Id == id).First();
                DB.Products.Remove(wire.Product);
                DB.WireHeadphones.Remove(wire);
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
            return await DB.WireHeadphones.CountAsync();
        }

        public async Task<WireHeadphone> GetFull(int id)
        {
            return await DB.WireHeadphones.Include(o => o.Product).ThenInclude(o => o.Brand).Include(o => o.Product).ThenInclude(o => o.Department)
     .Include(o => o.Product).ThenInclude(o => o.Type).Include(o => o.Product).ThenInclude(o => o.Color)
     .Include(o => o.ConnectionType)
     .FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<WireHeadphone>> GetListFull(int skip, int count)
        {
            return await DB.WireHeadphones.Include(o => o.Product).ThenInclude(o => o.Brand).Include(o => o.Product).ThenInclude(o => o.Department)
                .Include(o => o.Product).ThenInclude(o => o.Type).Include(o => o.Product).ThenInclude(o => o.Color)
                .Include(o => o.ConnectionType)
                .Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<WireHeadphone>> GetListShort(int skip, int count)
        {
            return await DB.WireHeadphones.Include(o => o.Product).Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<WireHeadphone> GetShort(int id)
        {
            return await DB.WireHeadphones.Include(o => o.Product).FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<bool> Update(WireHeadphone wire)
        {
            if (wire.Id == 0)
            {
                bool result = Add(wire).Result;
                if (result)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                var prev = DB.WireHeadphones.Include(o => o.Product).Where(o => o.Id == wire.Id).First();
                prev.WireLenght = wire.WireLenght;
                prev.ConnectionTypeId = wire.ConnectionTypeId;
                prev.Product.BrandId = wire.Product.BrandId;
                prev.Product.DepartmentId = wire.Product.DepartmentId;
                prev.Product.Description = wire.Product.Description;
                prev.Product.Discount = wire.Product.Discount;
                prev.Product.DiscountDate = wire.Product.DiscountDate;
                prev.Product.Name = wire.Product.Name;
                prev.Product.Photo = wire.Product.Photo;
                prev.Product.Price = wire.Product.Price;
                prev.Product.TypeId = wire.Product.TypeId;
                DB.SaveChanges();
            }
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
