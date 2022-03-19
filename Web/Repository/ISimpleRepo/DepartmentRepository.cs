using Diplom.Models.EF;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.ISimpleRepo;

namespace Web.Repository
{
    public class DepartmentRepository : ISimpleRepo<Department>
    {
        private ShopContext DB;
        public DepartmentRepository(ShopContext context)
        {
            DB = context;
        }
        public async Task<bool> Add(Department department)
        {
            DB.Departments.Add(department);
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
                Department department = DB.Departments.Find(id);
                DB.Departments.Remove(department);
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
            return await DB.Departments.CountAsync();
        }

        public async Task<Department> Get(int id)
        {
            return await DB.Departments.FirstOrDefaultAsync(o=>o.DepartmentId==id);
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await DB.Departments.ToListAsync();
        }

        public async Task<bool> Update(Department department)
        {
            if (department.DepartmentId == 0)
            {
                bool result = Add(department).Result;
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
                    Department newdepartment = await DB.Departments.FirstOrDefaultAsync(o => o.DepartmentId == department.DepartmentId);
                    newdepartment.Adress = department.Adress;
                    await DB.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Department>> GetByParam(string param)
        {
            return await DB.Departments.Where(o => o.Adress == param).ToListAsync();
        }
    }
}
