using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class ProviderRepository : IRepository<Provider>
    {
        private ShopContext DB;
        public ProviderRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<bool> Add(Provider provider)
        {
            DB.Providers.Add(provider);
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
                Provider provider = DB.Providers.Find(id);
                DB.Providers.Remove(provider);
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
            return await DB.Providers.CountAsync();
        }

        public async Task<Provider> GetFull(int id)
        {
            return await DB.Providers.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Provider>> GetListFull(int skip, int count)
        {
            return await DB.Providers.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Provider>> GetListShort(int skip, int count)
        {
            return await DB.Providers.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<Provider> GetShort(int id)
        {
            return await DB.Providers.FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Provider provider)
        {
            if (provider.Id == 0)
            {
                bool result = Add(provider).Result;
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
                    Provider newProvider = await DB.Providers.FirstOrDefaultAsync(o => o.Id == provider.Id);
                    newProvider.Date = provider.Date;
                    newProvider.DepartmentId = provider.DepartmentId;
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
