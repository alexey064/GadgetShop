using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.Simple;
using Web.Repository.ISimpleRepo;

namespace Web.Areas.DataEdit.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ColorController : Controller
    {
        ISimpleRepo<Color> ColorRepo;

        int ItemPerPage = 10;
        public ColorController(ISimpleRepo<Color> ColorRepository)
        {
            ColorRepo = ColorRepository;
        }
        public async Task<IActionResult> List(int page = 1)
        {
            List<Color> output = await ColorRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<Color>;
            if (output==null)
            {
                output = new List<Color>();
            }
            int Count = await ColorRepo.GetCount();
            int temp = (int)Count / ItemPerPage;
            if (temp * ItemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            return View(output);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Color color = await ColorRepo.Get(id);
            if (color == null)
            {
                color = new Color();
            }
            return View(color);
        }
        public async Task<IActionResult> Update(Color color)
        {
            await ColorRepo.Update(color);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await ColorRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
