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
    public class ScreenTypeController : Controller
    {
        ISimpleRepo<ScreenType> ScreenRepo;

        int ItemPerPage = 10;
        public ScreenTypeController(ISimpleRepo<ScreenType> ScreenRepository)
        {
            ScreenRepo = ScreenRepository;
        }
        public async Task<IActionResult> List(int page = 1)
        {
            List<ScreenType> output = await ScreenRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<ScreenType>;
            if (output==null)
            {
                output = new List<ScreenType>();
            }
            int Count = await ScreenRepo.GetCount();
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
            ScreenType Screen = await ScreenRepo.Get(id);
            if (Screen == null)
            {
                Screen = new ScreenType();
            }
            return View(Screen);
        }
        public async Task<IActionResult> Update(ScreenType Screen)
        {
            await ScreenRepo.Update(Screen);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await ScreenRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
