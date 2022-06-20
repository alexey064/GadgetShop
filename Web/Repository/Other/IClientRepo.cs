using System.Threading.Tasks;
using Web.Models.Linked;

namespace Web.Repository.Other
{
    public interface IClientRepo : ILinkedRepo<Client>
    {
        Task<Client> find(string name);
    }
}
