using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Linked;
using Web.Repository;
using Web.Repository.IProdMov;
using Web.Repository.IProductRepo;
using Web.Repository.ISimpleRepo;

namespace Web.Controllers
{
    public class BuyController : Controller
    {
        private IProdMov<PurchaseHistory> HistRepo;
        private IProductRepo<Product> ProductRepo;
        private TypeRepository TypeRepo;

        public BuyController(IProdMov<PurchaseHistory> HistRepository, IProductRepo<Product> ProductRepository,
            ISimpleRepo<Web.Models.Simple.Type> typeRepo) 
        {
            HistRepo = HistRepository;
            ProductRepo=ProductRepository;
            TypeRepo =(TypeRepository) typeRepo;
        }

        public async Task<IActionResult> MakeOrder()
        {//Если в базе данных есть запись о списке покупок, то надо его вывести
            PurchaseHistory hist;
            if (User.Identity.Name != null)
            {
                hist = await HistRepo.FindByUserName(User.Identity.Name);
            }//если пользователь не авторизован, то надо создать новый список покупок
            else
            {
                hist = new PurchaseHistory();
                hist.ProdMovement = new List<ProdMovement>();
                byte[] result;
                HttpContext.Session.TryGetValue("Prod0", out result);
                int ProductId = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                hist.DepartmentId = ProductRepo.Get(ProductId).Result.DepartmentId;
                for (int i = 0; i < 30; i++)
                {
                    if (HttpContext.Session.TryGetValue("Prod" + i, out result))
                    {
                        ProdMovement movement = new ProdMovement();
                        ProductId = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                        movement.Product = await ProductRepo.Get(ProductId);
                        HttpContext.Session.TryGetValue("Count" + i, out result);
                        movement.Count = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                        hist.ProdMovement.Add(movement);
                    }
                    else break;
                }
            }
            return View(hist);
        }
        public async Task<IActionResult> CompleteOrder(int id,String adress, string Phone)
        {
            if (User.Identity.Name != null)
            {
                PurchaseHistory purch = await HistRepo.GetFull(id);
                purch.Adress = adress;
                purch.Phone = Phone;
                purch.StatusId = TypeRepo.GetByParam("Status").Result.First(o => o.NormalizeName == "InProgress").Id;
                purch.PurchaseDate = DateTime.Now;
                await HistRepo.Update(purch);
            }
            else
            {
                PurchaseHistory hist = new PurchaseHistory();
                hist.ProdMovement = new List<ProdMovement>();
                byte[] result;
                HttpContext.Session.TryGetValue("Prod0", out result);
                int ProductId = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                hist.DepartmentId = ProductRepo.Get(ProductId).Result.DepartmentId;
                hist.Adress = adress;
                hist.Phone = Phone;
                hist.StatusId = TypeRepo.GetByParam("Status").Result.First(o => o.NormalizeName == "InProgress").Id;
                hist.PurchaseDate = DateTime.Now;
                for (int i = 0; i < 30; i++)
                {
                    if (HttpContext.Session.TryGetValue("Prod" + i, out result))
                    {
                        ProdMovement movement = new ProdMovement();
                        ProductId = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                        movement.Product = await ProductRepo.Get(ProductId);
                        HttpContext.Session.TryGetValue("Count" + i, out result);
                        movement.Count = int.Parse(System.Text.Encoding.UTF8.GetString(result));
                        movement.MovementTypeId = 2;
                        hist.ProdMovement.Add(movement);
                    }
                    else break;
                }
                await HistRepo.Update(hist);
                foreach (ProdMovement item in hist.ProdMovement)
                {
                    Product prod = await ProductRepo.Get(item.ProductId);
                    prod.Count = prod.Count - item.Count;
                    await ProductRepo.Update(prod);
                }
            }
            return Redirect("/shop/main");
        }
    }
}