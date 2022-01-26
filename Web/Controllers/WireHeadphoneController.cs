﻿using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WireHeadphoneController : Controller
    {
        int itemPerPage = 15;
        private ShopContext DB;
        public WireHeadphoneController(ShopContext ctx)
        {
            DB = ctx;
        }
        public async Task<ActionResult> List(int page = 0)
        {
            int Count = DB.WireHeadphones.Count();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await DB.WireHeadphones.Include(o => o.Product).ThenInclude(o => o.Brand).Include(o => o.Product).ThenInclude(o => o.Department)
                .Include(o => o.Product).ThenInclude(o => o.Type).Include(o=>o.Product).ThenInclude(o=>o.Color)
                .Include(o=>o.ConnectionType)
                .Skip(page * itemPerPage).Take(itemPerPage).ToArrayAsync();
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            WireHeadViewModel model = new WireHeadViewModel();
            model.Brands = await DB.Brands.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Departments = await DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionaryAsync(o => o.DepartmentId, o => o.Adress);
            model.Types = await DB.Types.Select(o => new { o.Id, o.Name, o.Category }).Where(o=>o.Category== "ПровНаушники").ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Colors = await DB.Colors.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.ConnectionType = await DB.ChargingTypes.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.EditItem = await DB.WireHeadphones.Include(o => o.Product).Where(o => o.Id == id).FirstOrDefaultAsync();
            if (model.EditItem == null)
            {
                model.EditItem = new WireHeadphone();
                model.EditItem.Product = new Product();
            }
            return View(model);
        }
        public async Task<IActionResult> Save(WireHeadphone wire, IFormFile UploadFile)
        {
            if (UploadFile!=null)
            {
                wire.Product.Photo = LoadPhoto(UploadFile, wire.Product.Photo);
            }
            if (wire.Id == 0)
            {
                wire.Product.AddDate = DateTime.Now;
                DB.WireHeadphones.Add(wire);
            }
            else
            {
                var prev = DB.WireHeadphones.Include(o => o.Product).Where(o => o.Id == wire.Id).First();
                prev.WireLenght = wire.WireLenght;
                prev.ConnectionTypeId = wire.ConnectionTypeId;
                prev.Product.BrandId = wire.Product.BrandId;
                prev.Product.DepartmentId = wire.Product.DepartmentId;
                prev.Product.Description = wire.Product.Description;
                prev.Product.Discount = wire.Product.Discount;
                prev.Product.DiscountDate = wire.Product.DiscountDate;
                prev.Product.Name = wire.Product.Name;
                prev.Product.Photo = wire.Product.Photo;
                prev.Product.Price = wire.Product.Price;
                prev.Product.TypeId = wire.Product.TypeId;
                DB.SaveChanges();
            }
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            WireHeadphone wire = DB.WireHeadphones.Where(o => o.Id == id).First();
            DB.Products.Remove(wire.Product);
            DB.WireHeadphones.Remove(wire);
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
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Files/WireHeadphones/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/wwwroot/Files/WireHeadphones/");
            }
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/wwwroot/Files/WireHeadphones/");
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
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/wwwroot/Files/WireHeadphones/WireHead_" + (MaxNumb + 1) + ".png", FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return "/Files/WireHeadphones/WireHead_" + (MaxNumb + 1) + ".png";
        }
    }
}
