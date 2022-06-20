using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;

namespace Web.Repository.IProdMov
{
    public class PurchHistoryRepository : IProdMov<PurchaseHistory>
    {
        private ShopContext DB;
        public PurchHistoryRepository(ShopContext context) 
        {
            DB = context;
        }

        public async Task<bool> Add(PurchaseHistory item)
        {
            DB.PurchaseHistories.Add(item);
            try
            {
                await DB.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                PurchaseHistory Hist = await DB.PurchaseHistories.Include(o => o.ProdMovement).FirstAsync(o => o.Id == id);
                foreach (ProdMovement item in Hist.ProdMovement)
                {
                    Product prod = await DB.Products.Where(o => o.ProductId == item.ProductId).FirstAsync();
                    prod.Count = prod.Count + item.Count;
                }
                DB.ProdMovements.RemoveRange(Hist.ProdMovement);
                DB.PurchaseHistories.Remove(Hist);
                await DB.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public async Task<PurchaseHistory> FindByUserName(string UserName)
        {
            return await DB.PurchaseHistories.Include(o => o.ProdMovement).ThenInclude(o => o.Product).Include(o => o.Client).Include(o=>o.Status)
                .Where(o => o.Client.NickName.Equals(UserName) && o.Status.NormalizeName.Equals("InCart")).FirstOrDefaultAsync();
            //TypeRepo.GetByParam("Status").Result.First(o => o.NormalizeName == "InProgress").Id;
        }

        public async Task<int> GetCount()
        {
            return await DB.PurchaseHistories.CountAsync();
        }

        public async Task<PurchaseHistory> GetFull(int id)
        {
            return await DB.PurchaseHistories.Include(o => o.Client).Include(o => o.department).Include(o => o.ProdMovement)
                .ThenInclude(o=>o.Product).Include(o => o.Seller).Include(o=>o.Status)
                .Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PurchaseHistory>> GetListFull(int skip, int count)
        {
            return await DB.PurchaseHistories.Include(o => o.Client).Include(o => o.department).Include(o => o.ProdMovement)
                .ThenInclude(o=>o.Product).Include(o => o.Seller)
        .Skip(skip).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<PurchaseHistory>> GetListShort(int skip, int count)
        {
            return await DB.PurchaseHistories.Include(o => o.ProdMovement)
            .Skip(skip).Take(count).ToListAsync();
        }

        public async Task<PurchaseHistory> GetShort(int id)
        {
            return await DB.PurchaseHistories.Include(o => o.ProdMovement)
            .Where(o=>o.Id==id).FirstOrDefaultAsync();
        }

        public Task<bool> ProdMoveAdd(int ContainerId, ProdMovement prod)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ProdMoveDelete(int MoveId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(PurchaseHistory item)
        {
            if (item.Id == 0)
            {
                if (await Add(item))
                {
                    return true;
                }
                else return false;
            }
            else
            {
                var prev = await GetFull(item.Id);
                prev.PurchaseDate = item.PurchaseDate;
                prev.ClientId = item.ClientId;
                prev.SellerId = item.SellerId;
                prev.Status = item.Status;
                prev.DepartmentId = item.DepartmentId;
                prev.TotalCost = item.TotalCost;
                prev.ProdMovement = item.ProdMovement;
                try
                {
                    await DB.SaveChangesAsync();
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }
    }
}