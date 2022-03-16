using Diplom.Models.EF;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class ScreenTypeRepository : IRepository<ScreenType>
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

        public async Task<ScreenType> GetFull(int id)
        {
            return await DB.ScreenTypes.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ScreenType>> GetListFull(int skip, int count)
        {
            return await DB.ScreenTypes.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<ScreenType>> GetListShort(int skip, int count)
        {
            return await DB.ScreenTypes.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<ScreenType> GetShort(int id)
        {
            return await DB.ScreenTypes.FirstOrDefaultAsync();
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
    }
}
