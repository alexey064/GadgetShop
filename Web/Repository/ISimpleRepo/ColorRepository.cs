﻿using Web.Models.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.ISimpleRepo;
using Web.Models.Simple;

namespace Web.Repository
{
    public class ColorRepository : ISimpleRepo<Color>
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

        public async Task<Color> Get(int id)
        {
            return await DB.Colors.FirstOrDefaultAsync(o=>o.Id==id);
        }

        public async Task<IEnumerable<Color>> GetAll()
        {
            return await DB.Colors.OrderBy(o=>o.Name).ToListAsync();
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
                    Color newcolor = await DB.Colors.FirstOrDefaultAsync(o => o.Id == color.Id);
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

        public async Task<IEnumerable<Color>> GetByParam(string param)
        {
            return await DB.Colors.Where(o => o.Name == param).OrderBy(o=>o.Name).ToListAsync();
        }

        public async Task<IEnumerable<Color>> GetList(int skip, int count)
        {
            return await DB.Colors.Skip(skip).Take(count).OrderBy(o=>o.Name).ToListAsync();
        }
    }
}
