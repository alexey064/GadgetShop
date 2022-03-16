using Diplom.Models.EF;
using Diplom.Models.Model.simple;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class DepartmentRepository : IRepository<Department>
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

        public async Task<Department> GetFull(int id)
        {
            return await DB.Departments.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Department>> GetListFull(int skip, int count)
        {
            return await DB.Departments.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<IEnumerable<Department>> GetListShort(int skip, int count)
        {
            return await DB.Departments.Skip(skip).Take(count).ToArrayAsync();
        }

        public async Task<Department> GetShort(int id)
        {
            return await DB.Departments.FirstOrDefaultAsync();
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
    }
}
