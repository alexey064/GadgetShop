using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.ISimpleRepo;
using Web.Repository.Other;

namespace Web.Repository.ILinkedRepo
{
    public class ClientRepository : IClientRepo
    {
        private ShopContext DB;
        private ISimpleRepo<Department> DepRepo;
        private ISimpleRepo<Diplom.Models.Model.simple.Type> TypeRepo;

        public ClientRepository(ShopContext context, ISimpleRepo<Department> DepRepository, ISimpleRepo<Diplom.Models.Model.simple.Type> TypeRepository) 
        {
            DB = context;
            DepRepo = DepRepository;
            TypeRepo = TypeRepository;
        }
        public async Task<bool> Add(Client item)
        {
            try
            {
                await DB.Clients.AddAsync(item);
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
                Client client = DB.Clients.FirstOrDefault(o => o.Id == id);
                DB.Clients.Remove(client);
                await DB.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<Client> find(string name)
        {
            return DB.Clients.Where(o => o.NickName == name).FirstOrDefaultAsync();
        }

        public async Task<int> GetCount()
        {
            return await DB.Clients.CountAsync();
        }

        public async Task<Client> GetFull(int id)
        {
            return await DB.Clients.Include(o => o.Department).Include(o => o.Post).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Client>> GetListFull(int skip, int count)
        {
            return await DB.Clients.Include(o=>o.Department).Include(o=>o.Post).Skip(skip).Take(count).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetListShort(int skip, int count)
        {
            return await DB.Clients.Skip(skip).Take(count).ToListAsync();
        }

        public async Task<Client> GetShort(int id)
        {
            return await DB.Clients.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> Update(Client client)
        {
            if (client.Id == 0)
            {
                if (await Add(client))
                {
                    return true;
                }
                else return false;
            }
            else
            {
                var prev = await DB.Clients.Include(o => o.Department).Include(o => o.Post).Where(o => o.Id == client.Id).FirstAsync();
                prev.FullName = client.FullName;
                prev.Phone = client.Phone;
                prev.DepartmentId = client.DepartmentId;
                prev.PostId = client.PostId;
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
