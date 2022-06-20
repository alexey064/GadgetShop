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
    public class VideocardController : Controller
    {
        ISimpleRepo<Videocard> VideocardRepo;

        int ItemPerPage = 10;
        public VideocardController(ISimpleRepo<Videocard> VideocardRepository)
        {
            VideocardRepo = VideocardRepository;
        }
        public async Task<IActionResult> List(int page = 1)
        {
            List<Videocard> output = await VideocardRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<Videocard>;
            if (output==null)
            {
                output = new List<Videocard>();
            }
            int Count = await VideocardRepo.GetCount();
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
            Videocard Videocard = await VideocardRepo.Get(id);
            if (Videocard == null)
            {
                Videocard = new Videocard();
            }
            return View(Videocard);
        }
        public async Task<IActionResult> Update(Videocard Videocard)
        {
            await VideocardRepo.Update(Videocard);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await VideocardRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
