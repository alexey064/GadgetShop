using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class SmartphoneRepository : ILinkedRepo<Smartphone>
    {
        ShopContext DB;
        public SmartphoneRepository(ShopContext context) 
        {
            DB = context;
        }
        public async Task<bool> Add(Smartphone smartphone)
        {
            try
            {
                smartphone.product.AddDate = System.DateTime.Now;
                DB.Smartphones.Add(smartphone);
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
                Smartphone smartphone = await DB.Smartphones.Where(o => o.Id == id).FirstAsync();
                DB.Products.Remove(smartphone.product);
                DB.Smartphones.Remove(smartphone);
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
            return await DB.Smartphones.CountAsync();
        }

        public async Task<Smartphone> GetFull(int id)
        {
            return await DB.Smartphones.Include(o => o.OS).Include(o => o.ScreenType).Include(o => o.Processor).Include(o => o.product).ThenInclude(o => o.Brand)
            .Include(o => o.product).ThenInclude(o => o.Department).Include(o => o.product).ThenInclude(o => o.Type).Include(o => o.product).ThenInclude(o => o.Color)
            .Include(o => o.ChargingType)
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Smartphone>> GetListFull(int skip, int count)
        {
            return await DB.Smartphones.Include(o => o.OS).Include(o => o.ScreenType).Include(o => o.Processor).Include(o => o.product).ThenInclude(o => o.Brand)
            .Include(o => o.product).ThenInclude(o => o.Department).Include(o => o.product).ThenInclude(o => o.Type).Include(o => o.product).ThenInclude(o => o.Color)
            .Include(o => o.ChargingType)
            .Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Smartphone>> GetListShort(int skip, int count)
        {
             return await DB.Smartphones.Include(o=>o.product).Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<Smartphone> GetShort(int id)
        {
            return await DB.Smartphones.Include(o => o.product).FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<bool> Update(Smartphone smartphone)
        {
            if (smartphone.Id == 0)
            {
                bool result = Add(smartphone).Result;
                if (result)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                var prev = await DB.Smartphones.Include(o => o.product).Where(o => o.Id == smartphone.Id).FirstAsync();
                prev.BatteryCapacity = smartphone.BatteryCapacity;
                prev.Camera = smartphone.Camera;
                prev.ChargingTypeId = smartphone.ChargingTypeId;
                prev.Communication = smartphone.Communication;
                prev.MemoryCount = smartphone.MemoryCount;
                prev.NFC = smartphone.NFC;
                prev.Optional = smartphone.Optional;
                prev.OSId = smartphone.OSId;
                prev.PhoneSize = smartphone.PhoneSize;
                prev.ProcessorId = smartphone.ProcessorId;
                prev.RAMCount = smartphone.RAMCount;
                prev.ScreenResolution = smartphone.ScreenResolution;
                prev.ScreenSize = smartphone.ScreenSize;
                prev.ScreenTypeId = smartphone.ScreenTypeId;
                prev.SimCount = smartphone.SimCount;
                prev.Weight = smartphone.Weight;

                prev.product.AddDate = smartphone.product.AddDate;
                prev.product.BrandId = smartphone.product.BrandId;
                prev.product.ColorId = smartphone.product.ColorId;
                prev.product.DepartmentId = smartphone.product.DepartmentId;
                prev.product.Description = smartphone.product.Description;
                prev.product.Discount = smartphone.product.Discount;
                prev.product.DiscountDate = smartphone.product.DiscountDate;
                prev.product.Name = smartphone.product.Name;
                prev.product.Photo = smartphone.product.Photo;
                prev.product.Price = smartphone.product.Price;
                prev.product.TypeId = smartphone.product.TypeId;
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
