using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NotebookController : Controller
    {
        int itemPerPage = 15;
        private ShopContext DB;
        public NotebookController(ShopContext ctx)
        {
            DB = ctx;
        }
        public ActionResult List(int page = 1)
        {
            int Count = DB.Notebooks.Count();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = DB.Notebooks.Include(o=>o.OS).Include(o => o.ScreenType).Include(o => o.Processor).Include(o => o.product)
                .ThenInclude(o => o.Type).Include(o => o.product).ThenInclude(o => o.Brand).Include(o => o.product)
                .ThenInclude(o => o.Department).Include(o=>o.Videocard).Include(o=>o.product).ThenInclude(o=>o.Color);
            
            return View(result);
        }
        public IActionResult Edit(int id = 0)
        {
            NotebookViewModel model = new NotebookViewModel();
            model.Brands = DB.Brands.Select(o => new { o.Id, o.Name }).ToDictionary(o => o.Id, o => o.Name);
            model.Departments = DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionary(o => o.DepartmentId, o => o.Adress);
            model.Types = DB.Types.Select(o => new { o.Id, o.Name, o.Category }).Where(o=>o.Category== "Ноутбук").ToDictionary(o => o.Id, o => o.Name);
            model.Processors = DB.Processors.Select(o => new { o.Id, o.Name }).ToDictionary(o => o.Id, o => o.Name);
            model.ScreenTypes = DB.ScreenTypes.Select(o => new { o.Id, o.Name }).ToDictionary(o => o.Id, o => o.Name);
            model.OS = DB.OS.Select(o => new { o.id, o.Name }).ToDictionary(o => o.id, o => o.Name);
            model.Colors = DB.Colors.Select(o => new { o.Id, o.Name }).ToDictionary(o => o.Id, o => o.Name);
            model.Videocards = DB.Videocards.Select(o => new { o.Id, o.Name }).ToDictionary(o => o.Id, o => o.Name);
            model.EditItem = DB.Notebooks.Include(o => o.product).Where(o => o.Id == id).FirstOrDefault();
            if (model.EditItem == null)
            {
                model.EditItem = new Notebook();
                model.EditItem.product = new Product();
            }
            return View(model);
        }
        public IActionResult Save(Notebook notebook, IFormFile UploadFile)
        {
            if (UploadFile != null)
            {
                notebook.product.Photo = LoadPhoto(UploadFile, notebook.product.Photo);
            }
            if (notebook.Id == 0)
                {
                notebook.product.AddDate = System.DateTime.Now;
                    DB.Notebooks.Add(notebook);
                }
                else
                {
                var prev = DB.Notebooks.Include(o => o.product).Where(o => o.Id == notebook.Id).First();
                prev.ProcessorId = notebook.ProcessorId;
                prev.VideocardID = notebook.VideocardID;
                prev.OSId = notebook.OSId;
                prev.RAMCount = notebook.RAMCount;
                prev.HDDSize = notebook.HDDSize;
                prev.SSDSize = notebook.SSDSize;
                prev.ScreenResolution = notebook.ScreenResolution;
                prev.ScreenSize = notebook.ScreenSize;
                prev.Outputs = notebook.Outputs;
                prev.Camera = notebook.Camera;
                prev.Optional = notebook.Optional;
                prev.WirelessCommunication = notebook.WirelessCommunication;
                prev.ScreenTypeId = notebook.ScreenTypeId;
                prev.BatteryCapacity = notebook.BatteryCapacity;
                prev.Weight = notebook.Weight;
                prev.product.BrandId = notebook.product.BrandId;
                prev.product.ColorId = notebook.product.ColorId;
                prev.product.DepartmentId = notebook.product.DepartmentId;
                prev.product.Description = notebook.product.Description;
                prev.product.Discount = notebook.product.Discount;
                prev.product.DiscountDate = notebook.product.DiscountDate;
                prev.product.Name = notebook.product.Name;
                prev.product.Photo = notebook.product.Photo;
                prev.product.Price = notebook.product.Price;
                prev.product.TypeId = notebook.product.TypeId;
            }
                DB.SaveChanges();
                return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(int id)
        {
            Notebook notebook = DB.Notebooks.Where(o => o.Id == id).First();
            DB.Products.Remove(notebook.product);
            DB.Notebooks.Remove(notebook);
            DB.SaveChanges();
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
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Files/Notebook/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/wwwroot/Files/Notebook/");
            }
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/wwwroot/Files/Notebook/");
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
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/wwwroot/Files/Notebook/notebook_" + (MaxNumb + 1) +
                ".png", FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return "/Files/Notebook/notebook_" + (MaxNumb + 1) + ".png";
        }
    }
}