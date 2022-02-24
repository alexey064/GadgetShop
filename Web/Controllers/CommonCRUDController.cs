using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Diplom.Controllers
{
    public abstract class CommonCRUDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public string LoadPhoto(IFormFile file, string filePath, string type)
        {
            if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }
                return filePath;
            }
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/wwwroot/Files/"+type+"/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/wwwroot/Files/"+type+"/");
            }
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/wwwroot/Files/"+type+"/");
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
            using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/wwwroot/Files/Accessory/"+type+"_" + (MaxNumb + 1) + ".png", FileMode.Create))
            {
                file.CopyTo(fs);
            }
            return "/Files/Accessory/"+type+"_" + (MaxNumb + 1) + ".png";
        }
    }
}
