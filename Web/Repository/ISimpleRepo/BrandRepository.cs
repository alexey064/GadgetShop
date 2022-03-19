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
    public class BrandRepository : ISimpleRepo<Brand>
    {
        private ShopContext DB;
        public BrandRepository(ShopContext context) 
        {
            DB = context;
        }
        public async Task<bool> Add(Brand brand)
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
                Brand brand = DB.Brands.Find(id);
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

        public async Task<Brand> Get(int id)
        {
            return await DB.Brands.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Brand>> GetAll() 
        {
            return await DB.Brands.ToListAsync();
        }

        public async Task<bool> Update(Brand brand)
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
                    Brand newbrand = await DB.Brands.FirstOrDefaultAsync(o => o.Id == brand.Id);
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

        public async Task<IEnumerable<Brand>> GetByParam(string param)
        {
            return await DB.Brands.Where(o => o.Name == param).ToListAsync();
        }
    }
}
