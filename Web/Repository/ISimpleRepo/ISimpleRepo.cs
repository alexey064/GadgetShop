using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository.ISimpleRepo
{
    public interface ISimpleRepo<T>
    {
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<IEnumerable<T>> GetList(int skip, int count);
        public Task<IEnumerable<T>> GetByParam(string param);
        public Task<int> GetCount();
        public Task<bool> Update(T item);
        public Task<bool> Add(T item);
        public Task<bool> Delete(int id);
    }
}
