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
    public class OSRepository :ISimpleRepo<OS>
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

        public async Task<OS> Get(int id)
        {
            return await DB.OS.FirstOrDefaultAsync(o => o.id == id);
        }

        public async Task<IEnumerable<OS>> GetAll()
        {
            return await DB.OS.ToListAsync();
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

        public async Task<IEnumerable<OS>> GetByParam(string param)
        {
            return await DB.OS.Where(o => o.Name == param).ToListAsync();
        }
    }
}
