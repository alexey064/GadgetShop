using Diplom.Models.EF;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class OSRepository :IRepository<OS>
    {
        private ShopContext DB;
        public OSRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<bool> Add(OS os)
        {
            DB.OS.Add(os);
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
                OS os = DB.OS.Find(id);
                DB.OS.Remove(os);
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
            return await DB.OS.CountAsync();
        }

        public async Task<OS> GetFull(int id)
        {
            return await DB.OS.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OS>> GetListFull(int skip, int count)
        {
            return await DB.OS.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<OS>> GetListShort(int skip, int count)
        {
            return await DB.OS.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<OS> GetShort(int id)
        {
            return await DB.OS.FirstOrDefaultAsync();
        }

        public async Task<bool> Update(OS os)
        {
            if (os.id == 0)
            {
                bool result = Add(os).Result;
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
                    OS newOs = await DB.OS.FirstOrDefaultAsync(o => o.id == os.id);
                    newOs.Name = os.Name;
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
