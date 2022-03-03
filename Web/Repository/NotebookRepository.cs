using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class NotebookRepository : IRepository<Notebook>
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
                notebook.product.AddDate = System.DateTime.Now;
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
                DB.Products.Remove(notebook.product);
                DB.Notebooks.Remove(notebook);
                await DB.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Notebook> GetFull(int id)
        {
            return await DB.Notebooks.Include(o => o.OS).Include(o => o.ScreenType).Include(o => o.Processor).Include(o => o.product)
            .ThenInclude(o => o.Type).Include(o => o.product).ThenInclude(o => o.Brand).Include(o => o.product)
            .ThenInclude(o => o.Department).Include(o => o.Videocard).Include(o => o.product).ThenInclude(o => o.Color)
            .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Notebook>> GetListFull(int skip, int count)
        {
            return await DB.Notebooks.Include(o => o.OS).Include(o => o.ScreenType).Include(o => o.Processor).Include(o => o.product)
            .ThenInclude(o => o.Type).Include(o => o.product).ThenInclude(o => o.Brand).Include(o => o.product)
            .ThenInclude(o => o.Department).Include(o => o.Videocard).Include(o => o.product).ThenInclude(o => o.Color)
            .Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Notebook>> GetListShort(int skip, int count)
        {
            return await DB.Notebooks.Include(o => o.product).Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<Notebook> GetShort(int id)
        {
            return await DB.Notebooks.Include(o => o.product).Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Notebook notebook)
        {
            if (notebook.Id == 0)
            {
                bool result = Add(notebook).Result;
            }
            else
            {
                var prev = DB.Notebooks.Include(o => o.product).Where(o => o.Id == notebook.Id).First();
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
                prev.product.BrandId = notebook.product.BrandId;
                prev.product.ColorId = notebook.product.ColorId;
                prev.product.DepartmentId = notebook.product.DepartmentId;
                prev.product.Description = notebook.product.Description;
                prev.product.Discount = notebook.product.Discount;
                prev.product.DiscountDate = notebook.product.DiscountDate;
                prev.product.Name = notebook.product.Name;
                prev.product.Photo = notebook.product.Photo;
                prev.product.Price = notebook.product.Price;
                prev.product.TypeId = notebook.product.TypeId;
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
