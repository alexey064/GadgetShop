using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.ISimpleRepo;
using Web.Models.Simple;

namespace Web.Repository
{
    public class ChargingTypeRepository : ISimpleRepo<ChargingType>
    {
        private ShopContext DB;
        public ChargingTypeRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<bool> Add(ChargingType type)
        {
            DB.ChargingTypes.Add(type);
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
                ChargingType type = DB.ChargingTypes.Find(id);
                DB.ChargingTypes.Remove(type);
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
            return await DB.ChargingTypes.CountAsync();
        }

        public async Task<ChargingType> Get(int id)
        {
            return await DB.ChargingTypes.FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<ChargingType>> GetAll()
        {
            return await DB.ChargingTypes.OrderBy(o=>o.Name).ToListAsync();
        }

        public async Task<bool> Update(ChargingType type)
        {
            if (type.Id == 0)
            {
                bool result = Add(type).Result;
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
                    ChargingType newtype = await DB.ChargingTypes.FirstOrDefaultAsync(o => o.Id == type.Id);
                    newtype.Name = type.Name;
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
            return await DB.ChargingTypes.Where(o => o.Name == param).OrderBy(o=>o.Name).ToListAsync();
        }

        public async Task<IEnumerable<ChargingType>> GetList(int skip, int count)
        {
            return await DB.ChargingTypes.Skip(skip).Take(count).OrderBy(o=>o.Name).ToListAsync();
        }
    }
}