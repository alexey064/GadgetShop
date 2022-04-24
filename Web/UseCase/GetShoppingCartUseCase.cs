using Diplom.Models.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Repository.IProdMov;
using Web.Repository.IProductRepo;

namespace Web.UseCase
{
    public class GetShoppingCartUseCase : IUseCase<GetShoppingCartUseCase>
    {
        private IHttpContextAccessor _HttpContextAccessor;
        private IProdMov<PurchaseHistory> HistRepo;
        private IProductRepo<Product> ProductRepo;

        public GetShoppingCartUseCase(IHttpContextAccessor accessor, IProdMov<PurchaseHistory> HistRepository,
            IProductRepo<Product> ProductRepository) 
        {
            _HttpContextAccessor= accessor;
            HistRepo = HistRepository;
            ProductRepo = ProductRepository;
        }
        public async Task<IEnumerable<Product>> Execute() 
        {
            List<Product> products = new List<Product>();
            if (_HttpContextAccessor.HttpContext.User.Identity.Name != null)
            {
                PurchaseHistory hist = await HistRepo.FindByUserName(_HttpContextAccessor.HttpContext.User.Identity.Name);
                if (hist == null)
                {
                    return products;
                }
                foreach (ProdMovement item in hist.ProdMovement)
                {
                    item.Product.Count = item.Count;
                    products.Add(item.Product);
                }
            }
            else
            {
                for (int i = 0; i < 30; i++)
                {
                    byte[] result;
                    if (_HttpContextAccessor.HttpContext.Session.TryGetValue("Prod" + i, out result))
                    {
                        int ProductId = int.Parse(System.Text.Encoding.ASCII.GetString(result));
                        Product ProdAdd = await ProductRepo.Get(ProductId);
                        _HttpContextAccessor.HttpContext.Session.TryGetValue("Count" + i, out result);
                        ProdAdd.Count = int.Parse(System.Text.Encoding.ASCII.GetString(result));
                        products.Add(ProdAdd);
                    }
                    else break;
                }
            }
            return products;
        }
    }
}
