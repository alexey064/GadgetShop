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
    public class ProcessorRepository : ISimpleRepo<Processor>
    {
        private ShopContext DB;
        public ProcessorRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<bool> Add(Processor processor)
        {
            DB.Processors.Add(processor);
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
                Processor processor = DB.Processors.Find(id);
                DB.Processors.Remove(processor);
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
            return await DB.Processors.CountAsync();
        }

        public async Task<Processor> Get(int id)
        {
            return await DB.Processors.FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<Processor>> GetAll()
        {
            return await DB.Processors.OrderBy(o=>o.Name).ToListAsync();
        }

        public async Task<bool> Update(Processor processor)
        {
            if (processor.Id == 0)
            {
                bool result = Add(processor).Result;
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
                    Processor newProcessor = await DB.Processors.FirstOrDefaultAsync(o => o.Id == processor.Id);
                    newProcessor.Name = processor.Name;
                    await DB.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Processor>> GetByParam(string param)
        {
            return await DB.Processors.Where(o => o.Name == param).OrderBy(o=>o.Name).ToListAsync();
        }

        public async Task<IEnumerable<Processor>> GetList(int skip, int count)
        {
            return await DB.Processors.Skip(skip).Take(count).OrderBy(o=>o.Name).ToListAsync();
        }
    }
}