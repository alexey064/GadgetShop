using System.Threading.Tasks;
using Web.Models.Linked;

namespace Web.Repository.IProdMov
{
    public interface IProdMov<T> : ILinkedRepo<T>
    {
        Task<bool> ProdMoveDelete(int MoveId);
        Task<bool> ProdMoveAdd(int ContainerId, ProdMovement prod);
        Task<PurchaseHistory> FindByUserName(string UserName);
    }
}