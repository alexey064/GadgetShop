using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult> List(int page = 0)
        {
            int Count = await DB.Accessories.CountAsync();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            List<PurchaseHistory> result = await DB.PurchaseHistories.Include(o => o.department).Include(o => o.ProdMovement).ThenInclude(o => o.Product).Skip(page * itemPerPage).Take(itemPerPage).ToListAsync();
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            PurchHistoryViewModel model = new PurchHistoryViewModel();

            model.EditItem = await DB.PurchaseHistories.Include(o => o.department).Include(o => o.ProdMovement).ThenInclude(o=>o.Product).FirstOrDefaultAsync(o => o.Id == id);
            model.Department = await DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionaryAsync(o => o.DepartmentId, o => o.Adress);
            model.People = await DB.Clients.Select(o => new { o.DepartmentId, o.FullName }).ToDictionaryAsync(o => o.DepartmentId, o => o.FullName);
            model.Status = await DB.Types.Select(o => new { o.Id, o.Name, o.Category }).Where(o => o.Category == "Должность").ToDictionaryAsync(o => o.Id, o => o.Name);
            if (model.EditItem == null)
            {
                model.EditItem = new PurchaseHistory();
                model.EditItem.ProdMovement = new List<ProdMovement>();
            }
            return View(model);
        }
        public async Task<IActionResult> ProdMoveEdit(int? id)
        {
            ProdLViewModel model = new ProdLViewModel();
            model.EditItem = await DB.PurchaseHistories.Where(o => o.Id == id).FirstAsync();
            model.product = await DB.Products.Select(o => new { o.ProductId, o.Name }).ToDictionaryAsync(o => o.ProductId, o => o.Name);
            return View(model);
        }

        public IActionResult MoveSave(ProdMovement prod)
        {
            return View();
        }
        public async Task<IActionResult> Save(PurchaseHistory purchHist)
        {
            if (purchHist.Id == 0)
            {
                DB.PurchaseHistories.Add(purchHist);
            }
            else
            {
                var prev = await DB.PurchaseHistories.Include(o => o.ProdMovement).Include(o => o.department).Include(o=>o.Seller).Include(o=>o.Client).Where(o => o.Id == purchHist.Id).FirstAsync();
                prev.PurchaseDate = purchHist.PurchaseDate;
                prev.ClientId = purchHist.ClientId;
                prev.SellerId = purchHist.SellerId;
                prev.Status = purchHist.Status;
                prev.DepartmentId = purchHist.DepartmentId;
                prev.TotalCost = purchHist.TotalCost;
                prev.ProdMovement = purchHist.ProdMovement;
                DB.SaveChanges();
            }
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            PurchaseHistory Hist = await DB.PurchaseHistories.Include(o => o.ProdMovement).FirstAsync(o => o.Id == id);
            foreach (ProdMovement item in Hist.ProdMovement)
            {
                Product prod = await DB .Products.Where(o => o.ProductId == item.ProductId).FirstAsync();
                prod.Count = prod.Count + item.Count;
            }
            DB.ProdMovements.RemoveRange(Hist.ProdMovement);
            DB.PurchaseHistories.Remove(Hist);
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}