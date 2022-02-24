using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    public class AccessoryController : CommonCRUDController
    {
        int itemPerPage = 15;
        private ShopContext DB;
        public AccessoryController(ShopContext ctx) 
        {
            DB = ctx;
        }
        public ActionResult List(int page=1)
        {
            int Count = DB.Accessories.Count();
            int temp =(int) Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = DB.Accessories.Include(o => o.product).ThenInclude(o=>o.Department)
                .Include(o=>o.product).ThenInclude(o=>o.Type)
                .Include(o=>o.product).ThenInclude(o=>o.Brand)
                .Include(o=>o.product).ThenInclude(o=>o.Color)
                .Skip((page-1) * itemPerPage).Take(itemPerPage);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            AccessoryViewModel model = new AccessoryViewModel();
            model.Brands = await DB.Brands.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.department = await DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionaryAsync(o => o.DepartmentId, o => o.Adress);
            model.types = await DB.Types.Select(o => new { o.Id, o.Name, o.Category }).Where(o => o.Category == "Аксессуар").ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Colors = await DB.Colors.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.EditItem = await DB.Accessories.Include(o => o.product).Where(o => o.Id == id).FirstOrDefaultAsync();
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
            if (accessory.Id==0)
            {
                accessory.product.AddDate = DateTime.Now;
                DB.Accessories.Add(accessory);
            }
            else 
            {
              var prev = DB.Accessories.Include(o=>o.product).Where(o => o.Id == accessory.Id).First();
                prev.product.BrandId = accessory.product.BrandId;
                prev.product.ColorId = accessory.product.ColorId;
                prev.product.DepartmentId = accessory.product.DepartmentId;
                prev.product.Description = accessory.product.Description;
                prev.product.Discount = accessory.product.Discount;
                prev.product.DiscountDate = accessory.product.DiscountDate;
                prev.product.Name = accessory.product.Name;
                prev.product.Photo = accessory.product.Photo;
                prev.product.Price = accessory.product.Price;
                prev.product.TypeId = accessory.product.TypeId;
            }
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Accessory accessory = DB.Accessories.Where(o => o.Id == id).First();
            DB.Products.Remove(accessory.product);
            DB.Accessories.Remove(accessory);
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}