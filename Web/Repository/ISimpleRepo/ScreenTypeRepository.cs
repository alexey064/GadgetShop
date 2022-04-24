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
    public class ScreenTypeRepository : ISimpleRepo<ScreenType>
    {
        private ShopContext DB;
        public ScreenTypeRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<bool> Add(ScreenType type)
        {
            DB.ScreenTypes.Add(type);
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
                ScreenType type = DB.ScreenTypes.Find(id);
                DB.ScreenTypes.Remove(type);
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
            return await DB.ScreenTypes.CountAsync();
        }

        public async Task<ScreenType> Get(int id)
        {
            return await DB.ScreenTypes.FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<ScreenType>> GetAll()
        {
            return await DB.ScreenTypes.ToListAsync();
        }

        public async Task<bool> Update(ScreenType type)
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
                    ScreenType newtype = await DB.ScreenTypes.FirstOrDefaultAsync(o => o.Id == type.Id);
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

        public async Task<IEnumerable<ScreenType>> GetByParam(string param)
        {
            return await DB.ScreenTypes.Where(o => o.Name == param).ToListAsync();
        }

        public async Task<IEnumerable<ScreenType>> GetList(int skip, int count)
        {
            return await DB.ScreenTypes.Skip(skip).Take(count).ToListAsync();
        }
    }
}
