using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.ISimpleRepo;
using Web.Models.Simple;

namespace Web.Repository
{
    public class VideocardRepository : ISimpleRepo<Videocard>
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

        public async Task<Videocard> Get(int id)
        {
            return await DB.Videocards.FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<Videocard>> GetAll()
        {
            return await DB.Videocards.OrderBy(o=>o.Name).ToListAsync();
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

        public async Task<IEnumerable<Videocard>> GetByParam(string param)
        {
            return await DB.Videocards.Where(o => o.Name == param).OrderBy(o=>o.Name).ToListAsync();
        }

        public async Task<IEnumerable<Videocard>> GetList(int skip, int count)
        {
            return await DB.Videocards.Skip(skip).Take(count).OrderBy(o=>o.Name).ToListAsync();
        }
    }
}
