using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;
using Web.Repository;
using Web.Repository.ILinkedRepo;
using Web.Repository.IProdMov;
using Web.Repository.IProductRepo;
using Web.Repository.ISimpleRepo;

namespace Web.UseCase
{
    public class AddToCartUseCase : IUseCase<AddToCartUseCase>
    {
        private IHttpContextAccessor _HttpContextAccessor;
        private IProdMov<PurchaseHistory> HistRepo;
        private ILinkedRepo<ProdMovement> ProdRepo;
        private ClientRepository ClientRepo;
        private IProductRepo<Product> ProductRepo;
        private ISimpleRepo<Web.Models.Simple.Type> TypeRepo;
        public AddToCartUseCase(IHttpContextAccessor accessor,ILinkedRepo<Client> clientRepo,
            IProdMov<PurchaseHistory> histRepo, ILinkedRepo<ProdMovement> prodRepo, IProductRepo<Product> producttRepo, 
            ISimpleRepo<Web.Models.Simple.Type> typeRepo)
        {
            HistRepo = histRepo;
            ProdRepo = prodRepo;
            ClientRepo = (ClientRepository)clientRepo;
            ProductRepo = producttRepo;
            _HttpContextAccessor = accessor;
            TypeRepo = typeRepo;
        }
        public async Task<bool> Execute(int id, int Count, string UserName)
        {
            if (_HttpContextAccessor.HttpContext.User.Identity.Name != null)
            {
                PurchaseHistory hist = await HistRepo.FindByUserName(_HttpContextAccessor.HttpContext.User.Identity.Name);
                if (hist == null)
                {//Если на пользователя не зарегистрирована корзина продуктов, то надо её создать
                    hist = new PurchaseHistory();
                    hist.Client = await ClientRepo.find(_HttpContextAccessor.HttpContext.User.Identity.Name);
                    hist.StatusId = TypeRepo.GetByParam("Status").Result.First(o => o.NormalizeName == "InCart").Id;
                    hist.ProdMovement = new List<ProdMovement>();
                    hist.DepartmentId = ProductRepo.Get(id).Result.DepartmentId;
                    await HistRepo.Update(hist);
                }
                ProdMovement ExistedProd = hist.ProdMovement.Where(o => o.ProductId == id).FirstOrDefault();
                if (ExistedProd != null)
                {//Если в корзину добавляем ранее добавленный товар
                    ExistedProd.Count = ExistedProd.Count + Count;
                    await ProdRepo.Update(ExistedProd);
                }
                else
                {
                    ProdMovement prod = new ProdMovement();
                    prod.Count = Count;
                    prod.ProductId = id;
                    prod.MovementTypeId = 2;
                    hist.ProdMovement.Add(prod);
                    prod.TotalPrice = prod.Count * ProductRepo.Get(id).Result.Price;
                    await HistRepo.Update(hist);
                }
                Product product = await ProductRepo.Get(id);
                product.Count = product.Count - Count;
                await ProductRepo.Update(product);
            }
            else
            {
                int counter = 0;
                //Проверяем наличие уже существующей записи.
                for (int i = 0; i < 30; i++)
                {
                    byte[] value;
                    _HttpContextAccessor.HttpContext.Session.TryGetValue("Prod" + i, out value);
                    if (value == null)
                    {
                        break;
                    }
                    if (int.Parse(System.Text.Encoding.UTF8.GetString(value)) == id)
                    {
                        _HttpContextAccessor.HttpContext.Session.TryGetValue("Count" + i, out value);
                        int prodcount = int.Parse(System.Text.Encoding.UTF8.GetString(value));
                        value = System.Text.Encoding.UTF8.GetBytes((prodcount + i).ToString());
                        _HttpContextAccessor.HttpContext.Session.Set("Count" + i, value);
                    }
                }
                //Проверяем незанятый ключ.
                for (int i = 0; i < 30; i++)
                {
                    byte[] value;
                    if (!_HttpContextAccessor.HttpContext.Session.TryGetValue("Prod" + i, out value))
                    {
                        break;
                    }
                    counter++;
                }
                int temp = id;
                byte[] intBytes = System.Text.Encoding.UTF8.GetBytes(temp.ToString());
                _HttpContextAccessor.HttpContext.Session.Set("Prod" + counter, intBytes);
                temp = Count;
                intBytes = System.Text.Encoding.UTF8.GetBytes(temp.ToString());
                _HttpContextAccessor.HttpContext.Session.Set("Count" + counter, intBytes);
            }
            return true;
        }
    }
}