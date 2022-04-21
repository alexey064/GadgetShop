using Diplom.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository.IProductRepo
{
    public interface IProductRepo<T>
    {
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<bool> Update(Product product);
    }
}
