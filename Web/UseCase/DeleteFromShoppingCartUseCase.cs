using Diplom.Models.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository.IProdMov;
using Web.Repository.IProductRepo;

namespace Web.UseCase
{
    public class DeleteFromShoppingCartUseCase
    {
        private IHttpContextAccessor contextAccessor;
        IProdMov<PurchaseHistory> HistRepo;
        IProductRepo<Product> ProductRepo;
        public DeleteFromShoppingCartUseCase(IHttpContextAccessor accessor, IProdMov<PurchaseHistory> HistRepo,
            IProductRepo<Product> ProductRepo) 
        {
            contextAccessor = accessor;
            this.HistRepo = HistRepo;
            this.ProductRepo = ProductRepo;
        }
        public async Task<bool> Execute(int id)
        {
            List<Product> products = new List<Product>();
            if (contextAccessor.HttpContext.User.Identity.Name != null)
            {
                PurchaseHistory hist = await HistRepo.FindByUserName(contextAccessor.HttpContext.User.Identity.Name);
                int count = hist.ProdMovement.Where(o => o.Product.ProductId == id).Select(o => o.Count).First();
                Product product = await ProductRepo.Get(id);
                hist.ProdMovement.Remove(hist.ProdMovement.Where(o => o.ProductId == id).First());
                product.Count = product.Count + count;
                if (!await ProductRepo.Update(product)) return false;
                if (!await HistRepo.Update(hist)) return false;
            }
            else
            {
                for (int i = 0; i < 30; i++)
                {
                    byte[] result;
                    if (contextAccessor.HttpContext.Session.TryGetValue("Prod" + i, out result))
                    {
                        int ProductId = int.Parse(System.Text.Encoding.ASCII.GetString(result));
                        if (ProductId == id)
                        {
                            contextAccessor.HttpContext.Session.Remove("Prod" + i);
                            contextAccessor.HttpContext.Session.Remove("Count" + i);
                        }
                    }
                    else break;
                }
            }
            return true;
        }
    }
}
