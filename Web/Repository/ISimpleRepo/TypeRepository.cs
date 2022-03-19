using Diplom.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using Web.Repository.ISimpleRepo;

namespace Web.Repository
{
    public class TypeRepository : ISimpleRepo<Diplom.Models.Model.simple.Type>
    {
        private ShopContext DB;
        public TypeRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<IEnumerable<Diplom.Models.Model.simple.Type>> GetByParam(string Category) 
        {
            return await DB.Types.Where(o => o.Category == Category).ToListAsync();
        }
        public async Task<bool> Add(Diplom.Models.Model.simple.Type type)
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
                Diplom.Models.Model.simple.Type type = DB.Types.Find(id);
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

        public async Task<Diplom.Models.Model.simple.Type> Get(int id)
        {
            return await DB.Types.FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<Diplom.Models.Model.simple.Type>> GetAll()
        {
            return await DB.Types.ToArrayAsync();
        }

        public async Task<bool> Update(Diplom.Models.Model.simple.Type type)
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
                    Diplom.Models.Model.simple.Type newtype = await DB.Types.FirstOrDefaultAsync(o => o.Id == type.Id);
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
