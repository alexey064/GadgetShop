using Diplom.Models.Model;
using System.Threading.Tasks;

namespace Web.Repository.Other
{
    public interface IClientRepo : ILinkedRepo<Client>
    {
        Task<Client> find(string name);
    }
}
