using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Repository
{
    public interface ILinkedRepo<T>
    {
        Task<int> GetCount();
        Task<T> GetShort(int id);
        Task<T> GetFull(int id);
        Task<IEnumerable<T>> GetListShort(int skip, int count);
        Task<IEnumerable<T>> GetListFull(int skip, int count);
        Task<bool> Update(T item);
        Task<bool> Add(T item);
        Task<bool> Delete(int id);
    }
}
