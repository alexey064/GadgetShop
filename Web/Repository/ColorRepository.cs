using Diplom.Models.EF;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class ColorRepository : IRepository<Color>
    {
        private ShopContext DB;
        public ColorRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<bool> Add(Color color)
        {
            DB.Colors.Add(color);
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
                Color color = DB.Colors.Find(id);
                DB.Colors.Remove(color);
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
            return await DB.Colors.CountAsync();
        }

        public async Task<Color> GetFull(int id)
        {
            return await DB.Colors.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Color>> GetListFull(int skip, int count)
        {
            return await DB.Colors.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Color>> GetListShort(int skip, int count)
        {
            return await DB.Colors.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<Color> GetShort(int id)
        {
            return await DB.Colors.FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Color color)
        {
            if (color.Id == 0)
            {
                bool result = Add(color).Result;
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
                    Brand newcolor = await DB.Brands.FirstOrDefaultAsync(o => o.Id == color.Id);
                    newcolor.Name = color.Name;
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
