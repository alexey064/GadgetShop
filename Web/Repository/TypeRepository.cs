using Diplom.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;

namespace Web.Repository
{
    public class TypeRepository : IRepository<Diplom.Models.Model.simple.Type>
    {
        private ShopContext DB;
        public TypeRepository(ShopContext context)
        {
            DB = context;
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

        public async Task<Diplom.Models.Model.simple.Type> GetFull(int id)
        {
            return await DB.Types.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Diplom.Models.Model.simple.Type>> GetListFull(int skip, int count)
        {
            return await DB.Types.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Diplom.Models.Model.simple.Type>> GetListShort(int skip, int count)
        {
            return await DB.Types.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<Diplom.Models.Model.simple.Type> GetShort(int id)
        {
            return await DB.Types.FirstOrDefaultAsync();
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
