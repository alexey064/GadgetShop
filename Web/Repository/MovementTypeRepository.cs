using Diplom.Models.EF;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class MovementTypeRepository : IRepository<MovementType>
    {
        private ShopContext DB;
        public MovementTypeRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<bool> Add(MovementType type)
        {
            DB.MovementTypes.Add(type);
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
                MovementType type = DB.MovementTypes.Find(id);
                DB.MovementTypes.Remove(type);
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
            return await DB.MovementTypes.CountAsync();
        }

        public async Task<MovementType> GetFull(int id)
        {
            return await DB.MovementTypes.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MovementType>> GetListFull(int skip, int count)
        {
            return await DB.MovementTypes.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<MovementType>> GetListShort(int skip, int count)
        {
            return await DB.MovementTypes.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<MovementType> GetShort(int id)
        {
            return await DB.MovementTypes.FirstOrDefaultAsync();
        }

        public async Task<bool> Update(MovementType type)
        {
            if (type.Id == 0)
            {
                bool result = Add(type).Result;
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
                    MovementType newtype = await DB.MovementTypes.FirstOrDefaultAsync(o => o.Id == type.Id);
                    newtype.Name = type.Name;
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
