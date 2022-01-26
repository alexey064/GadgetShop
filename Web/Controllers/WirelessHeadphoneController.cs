using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WirelessHeadphoneController : Controller
    {
        int itemPerPage = 15;
        private ShopContext DB;
        public WirelessHeadphoneController(ShopContext ctx)
        {
            DB = ctx;
        }
        public async Task<ActionResult> List(int page = 0)
        {
            int Count = DB.WirelessHeadphones.Count();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await DB.WirelessHeadphones.Include(o => o.Product).ThenInclude(o => o.Brand).Include(o => o.Product).ThenInclude(o => o.Department)
                .Include(o => o.Product).ThenInclude(o => o.Type).Include(o=>o.Product).ThenInclude(o=>o.Color)
                .Include(o=>o.ChargingType)
                .Skip(page * itemPerPage).Take(itemPerPage).ToArrayAsync();
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            WirelessHeadViewModel model = new WirelessHeadViewModel();
            model.Brands = await DB.Brands.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Departments = await DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionaryAsync(o => o.DepartmentId, o => o.Adress);
            model.Types = await DB.Types.Select(o => new { o.Id, o.Name, o.Category }).Where(o=>o.Category== "БеспрНаушники").ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Colors = await DB.Colors.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.ConnectionType = await DB.ChargingTypes.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.EditItem = await DB.WirelessHeadphones.Include(o => o.Product).Where(o => o.Id == id).FirstOrDefaultAsync();
            if (model.EditItem == null)
            {
                model.EditItem = new WirelessHeadphone();
                model.EditItem.Product = new Product();
            }
            return View(model);
        }
        public async Task<IActionResult> Save(WirelessHeadphone wireless, IFormFile UploadFile)
        {
            if (UploadFile!=null)
            {
                wireless.Product.Photo = LoadPhoto(UploadFile, wireless.Product.Photo);
            }
            if (wireless.Id == 0)
            {
                wireless.Product.AddDate = DateTime.Now;
                DB.WirelessHeadphones.Add(wireless);
            }
            else
            {
                var prev = DB.WirelessHeadphones.Include(o => o.Product).Where(o => o.Id == wireless.Id).First();
                prev.Battery = wireless.Battery;
                prev.BluetoothVersion = wireless.BluetoothVersion;
                prev.CaseBattery = wireless.CaseBattery;
                prev.Radius = wireless.Radius;
                prev.ChargingTypeId = wireless.ChargingTypeId;
                prev.Product.BrandId = wireless.Product.BrandId;
                prev.Product.DepartmentId = wireless.Product.DepartmentId;
                prev.Product.Description = wireless.Product.Description;
                prev.Product.Discount = wireless.Product.Discount;
                prev.Product.DiscountDate = wireless.Product.DiscountDate;
                prev.Product.Name = wireless.Product.Name;
                prev.Product.Photo = wireless.Product.Photo;
                prev.Product.Price = wireless.Product.Price;
                prev.Product.TypeId = wireless.Product.TypeId;
                DB.SaveChanges();
            }
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            WirelessHeadphone wireless = DB.WirelessHeadphones.Where(o => o.Id == id).First();
            DB.Products.Remove(wireless.Product);
            DB.WirelessHeadphones.Remove(wireless);
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        public string LoadPhoto(IFormFile file, string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                return filePath;
            }
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Files/WirelessHeadphones/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/wwwroot/Files/WirelessHeadphones/");
            }
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/wwwroot/Files/WirelessHeadphones/");
            int MaxNumb = 0;
            foreach (FileInfo FileName in dir.GetFiles())
            {
                string name = FileName.Name.Split('_')[1];
                name = name.Split('.')[0];
                int numb;
                int.TryParse(name, out numb);
                if (numb != 0)
                {
                    if (numb > MaxNumb)
                    {
                        MaxNumb = numb;
                    }
                }
            }
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/wwwroot/Files/WirelessHeadphones/WirelessHead_" + (MaxNumb + 1) + ".png", FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return "/Files/WirelessHeadphones/WirelessHead_" + (MaxNumb + 1) + ".png";
        }
    }
}