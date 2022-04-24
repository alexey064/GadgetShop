using Diplom.Models.EF;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.ISimpleRepo;

namespace Web.Repository
{
    public class BrandRepository : ISimpleRepo<ChargingType>
    {
        private ShopContext DB;
        public BrandRepository(ShopContext context) 
        {
            DB = context;
        }
        public async Task<bool> Add(ChargingType brand)
        {
            DB.Brands.Add(brand);
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
                ChargingType brand = DB.Brands.Find(id);
                DB.Brands.Remove(brand);
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
            return await DB.Brands.CountAsync();
        }

        public async Task<ChargingType> Get(int id)
        {
            return await DB.Brands.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ChargingType>> GetAll() 
        {
            return await DB.Brands.ToListAsync();
        }

        public async Task<bool> Update(ChargingType brand)
        {
            if (brand.Id == 0)
            {
                bool result = Add(brand).Result;
                if (result)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                try
                {
                    ChargingType newbrand = await DB.Brands.FirstOrDefaultAsync(o => o.Id == brand.Id);
                    newbrand.Name = brand.Name;
                    await DB.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<ChargingType>> GetByParam(string param)
        {
            return await DB.Brands.Where(o => o.Name == param).ToListAsync();
        }

        public async Task<IEnumerable<ChargingType>> GetList(int skip, int count)
        {
            return await DB.Brands.Skip(skip).Take(count).ToListAsync();
        }
    }
}
