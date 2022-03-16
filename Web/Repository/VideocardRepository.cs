using Diplom.Models.EF;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class VideocardRepository : IRepository<Videocard>
    {
        private ShopContext DB;
        public VideocardRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<bool> Add(Videocard videocard)
        {
            DB.Videocards.Add(videocard);
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
                Videocard videocard = DB.Videocards.Find(id);
                DB.Videocards.Remove(videocard);
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
            return await DB.Videocards.CountAsync();
        }

        public async Task<Videocard> GetFull(int id)
        {
            return await DB.Videocards.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Videocard>> GetListFull(int skip, int count)
        {
            return await DB.Videocards.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Videocard>> GetListShort(int skip, int count)
        {
            return await DB.Videocards.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<Videocard> GetShort(int id)
        {
            return await DB.Videocards.FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Videocard videocard)
        {
            if (videocard.Id == 0)
            {
                bool result = Add(videocard).Result;
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
                    Videocard newVideocard = await DB.Videocards.FirstOrDefaultAsync(o => o.Id == videocard.Id);
                    newVideocard.Name = videocard.Name;
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
