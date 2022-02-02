using Diplom.Models.EF;
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
    public class NotebookController : Controller
    {
        int itemPerPage = 15;
        private ShopContext DB;
        public NotebookController(ShopContext ctx)
        {
            DB = ctx;
        }
        public async Task<ActionResult> List(int page = 1)
        {
            int Count = DB.Notebooks.Count();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await DB.Notebooks.Include(o => o.OS).Include(o => o.ScreenType).Include(o => o.Processor).Include(o => o.product)
                .ThenInclude(o => o.Type).Include(o => o.product).ThenInclude(o => o.Brand).Include(o => o.product)
                .ThenInclude(o => o.Department).Include(o => o.Videocard).Include(o => o.product).ThenInclude(o => o.Color)
                .Skip((page-1) * itemPerPage).Take(itemPerPage).ToListAsync();
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            NotebookViewModel model = new NotebookViewModel();
            model.Brands = await DB.Brands.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Departments = await DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionaryAsync(o => o.DepartmentId, o => o.Adress);
            model.Types = await DB.Types.Select(o => new { o.Id, o.Name, o.Category }).Where(o=>o.Category== "Ноутбук").ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Processors = await DB.Processors.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.ScreenTypes = await DB.ScreenTypes.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.OS = await DB.OS.Select(o => new { o.id, o.Name }).ToDictionaryAsync(o => o.id, o => o.Name);
            model.Colors = await DB.Colors.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.Videocards = await DB.Videocards.Select(o => new { o.Id, o.Name }).ToDictionaryAsync(o => o.Id, o => o.Name);
            model.EditItem = await DB.Notebooks.Include(o => o.product).Where(o => o.Id == id).FirstOrDefaultAsync();
            if (model.EditItem == null)
            {
                model.EditItem = new Notebook();
                model.EditItem.product = new Product();
            }
            return View(model);
        }
        public async Task<IActionResult> Save(Notebook notebook, IFormFile UploadFile)
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
                await DB.SaveChangesAsync();
                return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Notebook notebook = DB.Notebooks.Where(o => o.Id == id).First();
            DB.Products.Remove(notebook.product);
            DB.Notebooks.Remove(notebook);
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