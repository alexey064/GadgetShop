using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository;

namespace Diplom.Controllers
{
    public class AccessoryController : CommonCRUDController
    {
        int itemPerPage = 15;
        private ShopContext DB;
        private AccessoryRepository Repo;
        public AccessoryController(ShopContext ctx, AccessoryRepository repo) 
        {
            DB = ctx;
            Repo = repo;
        }
        public async Task<ActionResult> List(int page=1)
        {
            int Count = DB.Accessories.Count();
            int temp =(int) Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = Repo.getList(0, 5);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            AccessoryViewModel model = new AccessoryViewModel();
            model.Brands = await DB.Brands.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.department = await DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionaryAsync(o => o.DepartmentId, o => o.Adress);
            model.types = await DB.Types.Select(o => new { o.Id, o.Name, o.Category }).Where(o => o.Category == "Аксессуар").ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Colors = await DB.Colors.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.EditItem = Repo.Get(id);
            if (model.EditItem == null)
            {
                model.EditItem = new Accessory();
                model.EditItem.product = new Product();
            }
            return View("Edit",model);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Accessory accessory, IFormFile UploadFile) 
        {
            if (UploadFile!=null)
            {
                accessory.product.Photo = base.LoadPhoto(UploadFile, accessory.product.Photo, nameof(Accessory));
            }
            Repo.Change(accessory);
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Repo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}