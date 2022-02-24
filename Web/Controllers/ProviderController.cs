using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult> List(int page = 0)
        {
            int Count = DB.Providers.Count();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            ViewBag.CurrentPage = page;
            var result = await DB.Providers.Include(o => o.Department).Include(o => o.ProdMovement).ThenInclude(o => o.Product).Skip(page * itemPerPage).Take(itemPerPage).ToArrayAsync();
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            ProviderViewModel model = new ProviderViewModel();
            model.EditItem = await DB.Providers.Include(o=>o.ProdMovement).ThenInclude(o=>o.Product).Where(o => o.Id == id).FirstOrDefaultAsync();
            model.Departments = await DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionaryAsync(o => o.DepartmentId, o => o.Adress);
            if (model.EditItem==null)
            {
                model.EditItem = new Provider();
                model.EditItem.ProdMovement = new List<ProdMovement>();
            }
            return View(model);
        }
        public async Task<IActionResult> ProdMoveEdit(int? id)
        {
            ProdMViewModel model = new ProdMViewModel();
            model.EditItem = await DB.Providers.Where(o => o.Id == id).FirstAsync();
            model.product = await DB.Products.Select(o => new { o.ProductId, o.Name }).ToDictionaryAsync(o => o.ProductId, o => o.Name);
            return View(model);
        }
        public async Task<IActionResult> MoveSave(ProdMovement prod, int ProvId)
        {
            Provider prov = await DB.Providers.Include(o=>o.ProdMovement).Where(o => o.Id == ProvId).FirstOrDefaultAsync();
            ProdMovement move = prov.ProdMovement.Where(o => o.ProductId == prod.ProductId).FirstOrDefault();
            if (move == null)
            {
                prov.ProdMovement.Add(prod);
                await DB.SaveChangesAsync();
                Product pr = await DB.Products.Where(o => o.ProductId == prod.ProductId).FirstOrDefaultAsync();
                pr.Count += prod.Count;
            }
            else
            {
                int ChangedCount = prod.Count - move.Count;
                Product product = await DB.Products.Where(o => o.ProductId == move.ProductId).FirstAsync();
                product.Count = product.Count + ChangedCount;
                await DB.SaveChangesAsync();
                move.Count = prod.Count;
                move.ProductId = prod.ProductId;
            }
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(ProdMoveEdit), new { Id = ProvId });
        }
        public async Task<IActionResult> Save(Provider provider)
        {
            if (provider.Id == 0)
            {
                DB.Providers.Add(provider);
            }
            else
            {
                var prev = await DB.Providers.Include(o => o.ProdMovement).Include(o => o.Department).Where(o => o.Id == provider.Id).FirstAsync();
                prev.Date = provider.Date;
                prev.DepartmentId = provider.DepartmentId;
                prev.ProdMovement = provider.ProdMovement;
                await DB.SaveChangesAsync();
            }
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public async Task<IActionResult> ProdDel(int DelId) 
        {
            ProdMovement prod = await DB.ProdMovements.Include(o=>o.Provider).Where(o => o.Id == DelId).FirstAsync();
            Product product = await DB.Products.Where(o => o.ProductId == prod.ProductId).FirstAsync();
            product.Count = product.Count - prod.Count;
            int RedId = prod.Provider.Id;
            DB.ProdMovements.Remove(prod);
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id = RedId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            Provider provider = await DB.Providers.Include(o => o.ProdMovement).FirstAsync(o => o.Id == id);
            foreach (ProdMovement item in provider.ProdMovement)
            {
                Product prod = DB.Products.Where(o => o.ProductId == item.ProductId).First();
                prod.Count = prod.Count - item.Count;
            }
            DB.ProdMovements.RemoveRange(provider.ProdMovement);
            DB.Providers.Remove(provider);
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}