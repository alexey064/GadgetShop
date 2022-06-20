using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;

namespace Web.Repository
{
    public class WirelessHeadRepository : ILinkedRepo<WirelessHeadphone>
    {
        ShopContext DB;
        public WirelessHeadRepository(ShopContext context) 
        {
            DB = context;
        }
        public async Task<bool> Add(WirelessHeadphone wireless)
        {
            try
            {
                wireless.Product.AddDate = DateTime.Now;
                DB.WirelessHeadphones.Add(wireless);
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
                WirelessHeadphone wireless = DB.WirelessHeadphones.Where(o => o.Id == id).First();
                DB.Products.Remove(wireless.Product);
                DB.WirelessHeadphones.Remove(wireless);
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
            return await DB.WirelessHeadphones.CountAsync();
        }

        public async Task<WirelessHeadphone> GetFull(int id)
        {
            return await DB.WirelessHeadphones.Include(o => o.Product).ThenInclude(o => o.Brand).Include(o => o.Product).ThenInclude(o => o.Department)
            .Include(o => o.Product).ThenInclude(o => o.Type).Include(o => o.Product).ThenInclude(o => o.Color)
            .Include(o => o.ChargingType)
            .FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<WirelessHeadphone>> GetListFull(int skip, int count)
        {
             return await DB.WirelessHeadphones.Include(o => o.Product).ThenInclude(o => o.Brand).Include(o => o.Product).ThenInclude(o => o.Department)
            .Include(o => o.Product).ThenInclude(o => o.Type).Include(o => o.Product).ThenInclude(o => o.Color)
            .Include(o => o.ChargingType)
            .Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<WirelessHeadphone>> GetListShort(int skip, int count)
        {
            return await DB.WirelessHeadphones.Include(o => o.Product).Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<WirelessHeadphone> GetShort(int id)
        {
            return await DB.WirelessHeadphones.Include(o => o.Product).FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<bool> Update(WirelessHeadphone wireless)
        {
            if (wireless.Id == 0)
            {
                bool result = Add(wireless).Result;
                if (result)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                var prev = DB.WirelessHeadphones.Include(o => o.Product).Where(o => o.Id == wireless.Id).First();
                prev.Battery = wireless.Battery;
                prev.BluetoothVersion = wireless.BluetoothVersion;
                prev.CaseBattery = wireless.CaseBattery;
                prev.Radius = wireless.Radius;
                prev.ChargingTypeId = wireless.ChargingTypeId;
                prev.Product.BrandId = wireless.Product.BrandId;
                prev.Product.DepartmentId = wireless.Product.DepartmentId;
                prev.Product.Description = wireless.Product.Description;
                prev.Product.Discount = wireless.Product.Discount;
                prev.Product.DiscountDate = wireless.Product.DiscountDate;
                prev.Product.Name = wireless.Product.Name;
                prev.Product.Photo = wireless.Product.Photo;
                prev.Product.Price = wireless.Product.Price;
                prev.Product.TypeId = wireless.Product.TypeId;
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
