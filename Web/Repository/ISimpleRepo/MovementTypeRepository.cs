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
    public class MovementTypeRepository : ISimpleRepo<MovementType>
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

        public async Task<MovementType> Get(int id)
        {
            return await DB.MovementTypes.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<MovementType>> GetAll()
        {
            return await DB.MovementTypes.OrderBy(o=>o.Name).ToListAsync();
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

        public async Task<IEnumerable<MovementType>> GetByParam(string param)
        {
            return await DB.MovementTypes.Where(o => o.Name == param).OrderBy(o=>o.Name).ToListAsync();
        }

        public async Task<IEnumerable<MovementType>> GetList(int skip, int count)
        {
            return await DB.MovementTypes.Skip(skip).Take(count).OrderBy(o=>o.Name).ToListAsync();
        }
    }
}
