using Diplom.Models.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Repository.IProdMov;
using Web.Repository.IProductRepo;

namespace Web.Controllers
{
    public class BuyController : Controller
    {
        IProdMov<PurchaseHistory> HistRepo;
        IProductRepo<Product> ProductRepo;

        public BuyController(IProdMov<PurchaseHistory> HistRepository, IProductRepo<Product> ProductRepository) 
        {
            HistRepo = HistRepository;
            ProductRepo=ProductRepository;
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
        public async Task<IActionResult> CompleteOrder(int id)
        {
            if (User.Identity.Name != null)
            {
                PurchaseHistory purch = await HistRepo.GetFull(id);
                purch.StatusId = 13;
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
                hist.StatusId = 13;
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
            return RedirectToAction("main");
        }
    }
}
