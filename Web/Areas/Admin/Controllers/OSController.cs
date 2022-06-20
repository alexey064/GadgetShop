using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Repository.ISimpleRepo;
using Web.Models.Simple;

namespace Web.Areas.DataEdit.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class OSController : Controller
    {
        ISimpleRepo<OS> OSRepo;

        int ItemPerPage = 10;
        public OSController(ISimpleRepo<OS> OSRepository)
        {
            OSRepo = OSRepository;
        }
        public async Task<IActionResult> List(int page = 1)
        {
            List<OS> output = await OSRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<OS>;
            if (output==null)
            {
                output = new List<OS>();
            }
            int Count = await OSRepo.GetCount();
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
            OS os = await OSRepo.Get(id);
            if (os == null)
            {
                os = new OS();
            }
            return View(os);
        }
        public async Task<IActionResult> Update(OS os)
        {
            await OSRepo.Update(os);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await OSRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}