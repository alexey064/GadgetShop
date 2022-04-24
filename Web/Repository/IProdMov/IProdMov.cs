using Diplom.Models.Model;
using System.Threading.Tasks;

namespace Web.Repository.IProdMov
{
    public interface IProdMov<T> : ILinkedRepo<T>
    {
        Task<bool> ProdMoveDelete(int MoveId);
        Task<bool> ProdMoveAdd(T Container, int ProductId, int Count);
        Task<PurchaseHistory> FindByUserName(string UserName);
    }
}