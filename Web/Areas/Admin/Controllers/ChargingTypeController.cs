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
    public class ChargingTypeController : Controller
    {
        ISimpleRepo<ChargingType> ChargRepo;

        int ItemPerPage = 10;
        public ChargingTypeController(ISimpleRepo<ChargingType> ChargRepository)
        {
            ChargRepo = ChargRepository;
        }
        public async Task<IActionResult> List(int page = 1)
        {
            List<ChargingType> output = await ChargRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<ChargingType>;
            if (output==null)
            {
                output = new List<ChargingType>();
            }
            int Count = await ChargRepo.GetCount();
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
            ChargingType Charg = await ChargRepo.Get(id);
            if (Charg == null)
            {
                Charg = new ChargingType();
            }
            return View(Charg);
        }
        public async Task<IActionResult> Update(ChargingType Charg)
        {
            await ChargRepo.Update(Charg);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await ChargRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
