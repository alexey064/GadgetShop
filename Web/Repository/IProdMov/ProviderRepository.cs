using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.IProdMov;
using Web.Repository.ISimpleRepo;

namespace Web.Repository
{
    public class ProviderRepository : IProdMov<Provider>
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
                Provider provider = await DB.Providers.Include(o => o.ProdMovement).FirstAsync(o => o.Id == id);
                foreach (ProdMovement item in provider.ProdMovement)
                {
                    Product prod = DB.Products.Where(o => o.ProductId == item.ProductId).First();
                    prod.Count = prod.Count - item.Count;
                }
                DB.ProdMovements.RemoveRange(provider.ProdMovement);
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

        public async Task<Provider> Get(int id)
        {
            return await DB.Providers.FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<Provider>> GetAll()
        {
            return await DB.Providers.ToListAsync();
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

        public async Task<IEnumerable<Provider>> GetByParam(string param)
        {
            return await DB.Providers.Where(o => o.DepartmentId == int.Parse(param)).ToListAsync();
        }

        public async Task<Provider> GetShort(int id)
        {
            return await DB.Providers.FindAsync(id);
        }

        public async Task<Provider> GetFull(int id)
        {
            return await DB.Providers.Include(o => o.Department).Include(o=>o.ProdMovement).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Provider>> GetListShort(int skip, int count)
        {
            return await DB.Providers.Skip(skip).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<Provider>> GetListFull(int skip, int count)
        {
            return await DB.Providers.Include(o => o.Department).Include(o=>o.ProdMovement).Skip(skip).Take(count).ToListAsync();
        }

        public async Task<bool> ProdMoveDelete(int MoveId)
        {
            ProdMovement prod = await DB.ProdMovements.Include(o => o.Provider).Where(o => o.Id == MoveId).FirstAsync();
            Product product = await DB.Products.Where(o => o.ProductId == prod.ProductId).FirstAsync();
            product.Count = product.Count - prod.Count;
            int RedId = prod.Provider.Id;
            DB.ProdMovements.Remove(prod);
            try
            {
                await DB.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        public Task<bool> ProdMoveAdd(Provider Container, int ProductId, int Count)
        {
            throw new NotImplementedException();
        }

        public Task<PurchaseHistory> FindByUserName(string UserName)
        {
            throw new NotImplementedException();
        }
    }
}
