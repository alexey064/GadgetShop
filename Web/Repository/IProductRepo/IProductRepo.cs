using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;

namespace Web.Repository.IProductRepo
{
    public interface IProductRepo<T>
    {
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<bool> Update(Product product);
        public Task<IEnumerable<Product>> GetByCategory(int skip, int count, string Category);
        public Task<IEnumerable<Product>> Search(string Pattern,int skip, int count);
        public Task<int> SearchCount(string Pattern);
    }
}
