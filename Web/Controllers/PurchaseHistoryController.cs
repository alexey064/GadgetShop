using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diplom.Controllers
{
    public class PurchaseHistoryController : Controller
    {
        int itemPerPage = 15;
        private ShopContext DB;
        public PurchaseHistoryController(ShopContext ctx)
        {
            DB = ctx;
        }
        public ActionResult List(int page = 0)
        {
            int Count = DB.Accessories.Count();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            List<PurchaseHistory> result = DB.PurchaseHistories.Include(o => o.department).Include(o => o.ProdMovement).ThenInclude(o => o.Product).Skip(page * itemPerPage).Take(itemPerPage).ToList();
            return View(result);
        }
        public IActionResult Edit(int id = 0)
        {
            PurchHistoryViewModel model = new PurchHistoryViewModel();

            model.EditItem = DB.PurchaseHistories.Include(o => o.department).Include(o => o.ProdMovement).ThenInclude(o=>o.Product).FirstOrDefault(o => o.Id == id);
            model.Department = DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionary(o => o.DepartmentId, o => o.Adress);
            model.People = DB.Clients.Select(o => new { o.DepartmentId, o.FullName }).ToDictionary(o => o.DepartmentId, o => o.FullName);
            model.Status = DB.Types.Select(o => new { o.Id, o.Name, o.Category }).Where(o => o.Category == "Должность").ToDictionary(o => o.Id, o => o.Name);
            if (model.EditItem == null)
            {
                model.EditItem = new PurchaseHistory();
                model.EditItem.ProdMovement = new List<ProdMovement>();
            }
            return View(model);
        }
        public IActionResult ProdMoveEdit(int? id)
        {
            ProdLViewModel model = new ProdLViewModel();
            model.EditItem = DB.PurchaseHistories.Where(o => o.Id == id).First();
            model.product = DB.Products.Select(o => new { o.ProductId, o.Name }).ToDictionary(o => o.ProductId, o => o.Name);
            return View(model);
        }

        public IActionResult MoveSave(ProdMovement prod)
        {
            return View();
        }
        public IActionResult Save(PurchaseHistory purchHist)
        {
            if (purchHist.Id == 0)
            {
                DB.PurchaseHistories.Add(purchHist);
            }
            else
            {
                var prev = DB.PurchaseHistories.Include(o => o.ProdMovement).Include(o => o.department).Include(o=>o.Seller).Include(o=>o.Client).Where(o => o.Id == purchHist.Id).First();
                prev.PurchaseDate = purchHist.PurchaseDate;
                prev.ClientId = purchHist.ClientId;
                prev.SellerId = purchHist.SellerId;
                prev.Status = purchHist.Status;
                prev.DepartmentId = purchHist.DepartmentId;
                prev.TotalCost = purchHist.TotalCost;
                prev.ProdMovement = purchHist.ProdMovement;
                DB.SaveChanges();
            }
            DB.SaveChanges();
            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(int id)
        {
            PurchaseHistory Hist = DB.PurchaseHistories.Include(o => o.ProdMovement).First(o => o.Id == id);
            foreach (ProdMovement item in Hist.ProdMovement)
            {
                Product prod = DB.Products.Where(o => o.ProductId == item.ProductId).First();
                prod.Count = prod.Count + item.Count;
            }
            DB.ProdMovements.RemoveRange(Hist.ProdMovement);
            DB.PurchaseHistories.Remove(Hist);
            DB.SaveChanges();
            return RedirectToAction(nameof(List));
        }
    }
}