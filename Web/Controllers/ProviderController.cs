using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProviderController : Controller
    {
        int itemPerPage = 15;
        private ShopContext DB;
        public ProviderController(ShopContext ctx)
        {
            DB = ctx;
        }
        public ActionResult List(int page = 0)
        {
            int Count = DB.Providers.Count();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            ViewBag.CurrentPage = page;
            List<Provider> result = DB.Providers.Include(o => o.Department).Include(o => o.ProdMovement).ThenInclude(o => o.Product).Skip(page * itemPerPage).Take(itemPerPage).ToList();
            return View(result);
        }
        public IActionResult Edit(int id = 0)
        {
            ProviderViewModel model = new ProviderViewModel();
            model.EditItem = DB.Providers.Include(o=>o.ProdMovement).ThenInclude(o=>o.Product).Where(o => o.Id == id).FirstOrDefault();
            model.Departments = DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionary(o => o.DepartmentId, o => o.Adress);
            if (model.EditItem==null)
            {
                model.EditItem = new Provider();
                model.EditItem.ProdMovement = new List<ProdMovement>();
            }
            return View(model);
        }
        public IActionResult ProdMoveEdit(int? id)
        {
            ProdMViewModel model = new ProdMViewModel();
            model.EditItem = DB.Providers.Where(o => o.Id == id).First();
            model.product = DB.Products.Select(o => new { o.ProductId, o.Name }).ToDictionary(o => o.ProductId, o => o.Name);
            return View(model);
        }

        public IActionResult MoveSave(ProdMovement prod, int ProvId)
        {
            Provider prov = DB.Providers.Include(o=>o.ProdMovement).Where(o => o.Id == ProvId).FirstOrDefault();
            ProdMovement move = prov.ProdMovement.Where(o => o.ProductId == prod.ProductId).FirstOrDefault();
            if (move == null)
            {
                prov.ProdMovement.Add(prod);
                DB.SaveChanges();
                Product pr = DB.Products.Where(o => o.ProductId == prod.ProductId).FirstOrDefault();
                pr.Count += prod.Count;
            }
            else
            {
                int ChangedCount = prod.Count - move.Count;
                Product product = DB.Products.Where(o => o.ProductId == move.ProductId).First();
                product.Count = product.Count + ChangedCount;
                DB.SaveChanges();
                move.Count = prod.Count;
                move.ProductId = prod.ProductId;
            }
            DB.SaveChanges();
            return RedirectToAction(nameof(ProdMoveEdit), new { Id = ProvId });
        }
        public IActionResult Save(Provider provider)
        {
            if (provider.Id == 0)
            {
                DB.Providers.Add(provider);
            }
            else
            {
                var prev = DB.Providers.Include(o => o.ProdMovement).Include(o => o.Department).Where(o => o.Id == provider.Id).First();
                prev.Date = provider.Date;
                prev.DepartmentId = provider.DepartmentId;
                prev.ProdMovement = provider.ProdMovement;
                DB.SaveChanges();
            }
            DB.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public IActionResult ProdDel(int DelId) 
        {
            ProdMovement prod = DB.ProdMovements.Include(o=>o.Provider).Where(o => o.Id == DelId).First();
            Product product = DB.Products.Where(o => o.ProductId == prod.ProductId).First();
            product.Count = product.Count - prod.Count;
            int RedId = prod.Provider.Id;
            DB.ProdMovements.Remove(prod);
            DB.SaveChanges();
            return RedirectToAction(nameof(Edit), new { id = RedId });
        }

        public IActionResult Delete(int id)
        {
            Provider provider = DB.Providers.Include(o => o.ProdMovement).First(o => o.Id == id);
            foreach (ProdMovement item in provider.ProdMovement)
            {
                Product prod = DB.Products.Where(o => o.ProductId == item.ProductId).First();
                prod.Count = prod.Count - item.Count;
            }
            DB.ProdMovements.RemoveRange(provider.ProdMovement);
            DB.Providers.Remove(provider);
            DB.SaveChanges();
            return RedirectToAction(nameof(List));
        }
    }
}