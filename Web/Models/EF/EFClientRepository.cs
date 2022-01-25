using Diplom.Models.Model;

namespace Diplom.Models.EF
{
    public class EFClientRepository : IClientRepository
    {
        private ShopContext ctx;
        public EFClientRepository(ShopContext repository) { ctx = repository; }

        public void Add(Client client)
        {
            ctx.Clients.Add(client);
            ctx.SaveChanges();
        }
    }
}