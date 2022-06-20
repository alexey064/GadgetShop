using Web.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Repository.ISimpleRepo;

namespace Web.Repository
{
    public class TypeRepository : ISimpleRepo<Models.Simple.Type>
    {
        private ShopContext DB;
        public TypeRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<IEnumerable<Models.Simple.Type>> GetByParam(string Category) 
        {
            return await DB.Types.Where(o => o.Category == Category).OrderBy(o=>o.Name).ToListAsync();
        }
        public async Task<bool> Add(Models.Simple.Type type)
        {
            DB.Types.Add(type);
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
                Models.Simple.Type type = DB.Types.Find(id);
                DB.Types.Remove(type);
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
            return await DB.Types.CountAsync();
        }

        public async Task<Models.Simple.Type> Get(int id)
        {
            return await DB.Types.FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<Web.Models.Simple.Type>> GetAll()
        {
            return await DB.Types.OrderBy(o=>o.Name).ToListAsync();
        }

        public async Task<bool> Update(Web.Models.Simple.Type type)
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
                    Models.Simple.Type newtype = await DB.Types.FirstOrDefaultAsync(o => o.Id == type.Id);
                    newtype.Id= type.Id;
                    newtype.Name = type.Name;
                    newtype.NormalizeName = type.NormalizeName;
                    newtype.Category=type.Category;
                    await DB.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Web.Models.Simple.Type>> GetList(int skip, int count)
        {
            return await DB.Types.Skip(skip).Take(count).OrderBy(o=>o.Name).ToListAsync();
        }
    }
}
