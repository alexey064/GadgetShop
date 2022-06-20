using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;

namespace Web.Repository
{
    public class NotebookRepository : ILinkedRepo<Notebook>
    {
        ShopContext DB;
        public NotebookRepository(ShopContext context) 
        {
            DB = context;
        }
        public async Task<bool> Add(Notebook notebook)
        {
            try
            {
                notebook.Product.AddDate = System.DateTime.Now;
                DB.Notebooks.Add(notebook);
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
                Notebook notebook = DB.Notebooks.Where(o => o.Id == id).First();
                DB.Products.Remove(notebook.Product);
                DB.Notebooks.Remove(notebook);
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
            return await DB.Notebooks.CountAsync();
        }

        public async Task<Notebook> GetFull(int id)
        {
            return await DB.Notebooks.Include(o => o.OS).Include(o => o.ScreenType).Include(o => o.Processor).Include(o => o.Product)
            .ThenInclude(o => o.Type).Include(o => o.Product).ThenInclude(o => o.Brand).Include(o => o.Product)
            .ThenInclude(o => o.Department).Include(o => o.Videocard).Include(o => o.Product).ThenInclude(o => o.Color)
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Notebook>> GetListFull(int skip, int count)
        {
            return await DB.Notebooks.Include(o => o.OS).Include(o => o.ScreenType).Include(o => o.Processor).Include(o => o.Product)
            .ThenInclude(o => o.Type).Include(o => o.Product).ThenInclude(o => o.Brand).Include(o => o.Product)
            .ThenInclude(o => o.Department).Include(o => o.Videocard).Include(o => o.Product).ThenInclude(o => o.Color)
            .Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Notebook>> GetListShort(int skip, int count)
        {
            return await DB.Notebooks.Include(o => o.Product).Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<Notebook> GetShort(int id)
        {
            return await DB.Notebooks.Include(o => o.Product).Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Notebook notebook)
        {
            if (notebook.Id == 0)
            {
                bool result = Add(notebook).Result;
            }
            else
            {
                var prev = DB.Notebooks.Include(o => o.Product).Where(o => o.Id == notebook.Id).First();
                prev.ProcessorId = notebook.ProcessorId;
                prev.VideocardID = notebook.VideocardID;
                prev.OSId = notebook.OSId;
                prev.RAMCount = notebook.RAMCount;
                prev.HDDSize = notebook.HDDSize;
                prev.SSDSize = notebook.SSDSize;
                prev.ScreenResolution = notebook.ScreenResolution;
                prev.ScreenSize = notebook.ScreenSize;
                prev.Outputs = notebook.Outputs;
                prev.Camera = notebook.Camera;
                prev.Optional = notebook.Optional;
                prev.WirelessCommunication = notebook.WirelessCommunication;
                prev.ScreenTypeId = notebook.ScreenTypeId;
                prev.BatteryCapacity = notebook.BatteryCapacity;
                prev.Weight = notebook.Weight;
                prev.Product.BrandId = notebook.Product.BrandId;
                prev.Product.ColorId = notebook.Product.ColorId;
                prev.Product.DepartmentId = notebook.Product.DepartmentId;
                prev.Product.Description = notebook.Product.Description;
                prev.Product.Discount = notebook.Product.Discount;
                prev.Product.DiscountDate = notebook.Product.DiscountDate;
                prev.Product.Name = notebook.Product.Name;
                prev.Product.Photo = notebook.Product.Photo;
                prev.Product.Price = notebook.Product.Price;
                prev.Product.TypeId = notebook.Product.TypeId;
            }
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
