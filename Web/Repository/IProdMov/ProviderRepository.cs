using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.IProdMov;
using Web.Repository.IProductRepo;
using Web.Models.Linked;

namespace Web.Repository
{
    public class ProviderRepository : IProdMov<Provider>
    {
        private ShopContext DB;
        private IProductRepo<Product> ProductRepo;
        public ProviderRepository(ShopContext context, IProductRepo<Product> ProductRepository)
        {
            DB = context;
            ProductRepo= ProductRepository;
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
                int s = 0;
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
            return await DB.Providers.Include(o => o.Department).Include(o=>o.ProdMovement).ThenInclude(o=>o.Product).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Provider>> GetListShort(int skip, int count)
        {
            return await DB.Providers.Skip(skip).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<Provider>> GetListFull(int skip, int count)
        {
            return await DB.Providers.Include(o => o.Department).Include(o=>o.ProdMovement).ThenInclude(o=>o.Product).Skip(skip).Take(count).ToListAsync();
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
        public async Task<bool> ProdMoveAdd(int ContainerId, ProdMovement prod)
        {
            
            Provider prov =  await GetFull(ContainerId);
            ProdMovement ExistedMove = prov.ProdMovement.Find(x => x.ProductId == prod.ProductId);
            if (ExistedMove == null)
            {//If object is not in DB then add to db
                //check if this product is added earlier
                prov.ProdMovement.Add(prod);
                await Update(prov);
                Product pr = await ProductRepo.Get(prod.ProductId);
                pr.Count += prod.Count;
                await ProductRepo.Update(pr);
            }
            else
            {
                int ChangedCount = prod.Count - ExistedMove.Count;
                Product product = await ProductRepo.Get(ExistedMove.ProductId);
                product.Count = product.Count + ChangedCount;
                await ProductRepo.Update(product);
                ExistedMove.Count = prod.Count;
                ExistedMove.ProductId = prod.ProductId;
                await Update(prov);
            }

            return true;
        }

        public Task<PurchaseHistory> FindByUserName(string UserName)
        {
            throw new NotImplementedException();
        }
    }
}
