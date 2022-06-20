using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;

namespace Web.Repository.ILinkedRepo
{
    public class ProdMovementRepository : ILinkedRepo<ProdMovement>
    {
        private ShopContext DB;
        public ProdMovementRepository(ShopContext context) 
        {
            DB = context;
        }
        public async Task<bool> Add(ProdMovement item)
        {
            try
            {
                DB.ProdMovements.Add(item);
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
                ProdMovement move = DB.ProdMovements.Find(id);
                DB.ProdMovements.Remove(move);
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
            return await DB.ProdMovements.CountAsync();
        }

        public async Task<ProdMovement> GetFull(int id)
        {
            return await DB.ProdMovements.Include(o => o.Product.Accessory).Include(o => o.Product.Brand).Include(o => o.Product.Color)
                .Include(o => o.Product.Department).Include(o => o.Product.Notebook).Include(o => o.Product.Smartphone).Include(o => o.Product.WireHeadphones)
                .Include(o => o.Product.WirelessHeadphones).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<ProdMovement>> GetListFull(int skip, int count)
        {
            return await DB.ProdMovements.Include(o => o.Product.Accessory).Include(o => o.Product.Brand).Include(o => o.Product.Color)
    .Include(o => o.Product.Department).Include(o => o.Product.Notebook).Include(o => o.Product.Smartphone).Include(o => o.Product.WireHeadphones)
    .Include(o => o.Product.WirelessHeadphones).Skip(skip).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<ProdMovement>> GetListShort(int skip, int count)
        {
            return await DB.ProdMovements.Include(o => o.Product).Skip(skip).Take(count).ToListAsync();
        }

        public async Task<ProdMovement> GetShort(int id)
        {
            return await DB.ProdMovements.Include(o => o.Product).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> Update(ProdMovement item)
        {
            if (item.Id == 0)
            {
                return await Add(item);
            }
            else 
            {
                ProdMovement move = await DB.ProdMovements.FirstOrDefaultAsync(o => o.Id == item.Id);
                move.Count = item.Count;
                move.ProductId = item.ProductId;
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
        }
    }
}
