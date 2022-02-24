﻿using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SmartphoneController : Controller
    {
        int itemPerPage = 15;
        private ShopContext DB;
        public SmartphoneController(ShopContext ctx)
        {
            DB = ctx;
        }
        public async Task<ActionResult> List(int page = 0)
        {
            int Count = DB.Smartphones.Count();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await DB.Smartphones.Include(o=>o.OS).Include(o => o.ScreenType).Include(o => o.Processor).Include(o => o.product).ThenInclude(o=>o.Brand)
                .Include(o=>o.product).ThenInclude(o=>o.Department).Include(o=>o.product).ThenInclude(o=>o.Type).Include(o=>o.product).ThenInclude(o=>o.Color)
                .Include(o=>o.ChargingType)
                .Skip(page * itemPerPage).Take(itemPerPage).ToArrayAsync();
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            SmartphoneViewModel model = new SmartphoneViewModel();
            model.Brands = await DB.Brands.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Department = await DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionaryAsync(o => o.DepartmentId, o => o.Adress);
            model.Processors = await DB.Processors.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.ScreenTypes = await DB.ScreenTypes.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Types = await DB.Types.Select(o => new { o.Id, o.Name, o.Category }).Where(o=>o.Category== "Смартфон").ToDictionaryAsync(o => o.Id, o => o.Name);
            model.OS = await DB.OS.Select(o => new { o.id, o.Name }).ToDictionaryAsync(o => o.id, o => o.Name);
            model.Colors = await DB.Colors.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.ChargingType = await DB.ChargingTypes.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.EditItem = await DB.Smartphones.Include(o => o.product).Where(o => o.Id == id).FirstOrDefaultAsync();
            if (model.EditItem == null)
            {
                model.EditItem = new Smartphone();
                model.EditItem.product = new Product();
            }
            return View(model);
        }
        public async Task<IActionResult> Save(Smartphone smartphone, IFormFile UploadFile)
        {
            if (UploadFile!=null)
            {
                smartphone.product.Photo = LoadPhoto(UploadFile, smartphone.product.Photo);
            }
            if (smartphone.Id == 0)
            {
                smartphone.product.AddDate = System.DateTime.Now;
                DB.Smartphones.Add(smartphone);
            }
            else
            {
                var prev = await DB.Smartphones.Include(o => o.product).Where(o => o.Id == smartphone.Id).FirstAsync();
                prev.BatteryCapacity = smartphone.BatteryCapacity;
                prev.Camera = smartphone.Camera;
                prev.ChargingTypeId = smartphone.ChargingTypeId;
                prev.Communication = smartphone.Communication;
                prev.MemoryCount = smartphone.MemoryCount;
                prev.NFC = smartphone.NFC;
                prev.Optional = smartphone.Optional;
                prev.OSId = smartphone.OSId;
                prev.PhoneSize = smartphone.PhoneSize;
                prev.ProcessorId = smartphone.ProcessorId;
                prev.RAMCount = smartphone.RAMCount;
                prev.ScreenResolution = smartphone.ScreenResolution;
                prev.ScreenSize = smartphone.ScreenSize;
                prev.ScreenTypeId = smartphone.ScreenTypeId;
                prev.SimCount = smartphone.SimCount;
                prev.Weight = smartphone.Weight;
                
                prev.product.AddDate = smartphone.product.AddDate;
                prev.product.BrandId = smartphone.product.BrandId;
                prev.product.ColorId = smartphone.product.ColorId;
                prev.product.DepartmentId = smartphone.product.DepartmentId;
                prev.product.Description = smartphone.product.Description;
                prev.product.Discount = smartphone.product.Discount;
                prev.product.DiscountDate = smartphone.product.DiscountDate;
                prev.product.Name = smartphone.product.Name;
                prev.product.Photo = smartphone.product.Photo;
                prev.product.Price = smartphone.product.Price;
                prev.product.TypeId = smartphone.product.TypeId;
                DB.SaveChanges();
            }
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Smartphone smartphone = await DB.Smartphones.Where(o => o.Id == id).FirstAsync();
            DB.Products.Remove(smartphone.product);
            DB.Smartphones.Remove(smartphone);
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
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Files/Smartphone/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/wwwroot/Files/Smartphone/");
            }
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/wwwroot/Files/smartphone/");
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
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/wwwroot/Files/Smartphone/Smartphone_" + (MaxNumb + 1) + ".png", FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return "/Files/Smartphone/Smartphone_" + (MaxNumb + 1) + ".png";
        }
    }
}