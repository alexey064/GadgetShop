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
    public class ProcessorController : Controller
    {
        ISimpleRepo<Processor> ProcRepo;

        int ItemPerPage = 10;
        public ProcessorController(ISimpleRepo<Processor> ProcRepository)
        {
            ProcRepo = ProcRepository;
        }
        public async Task<IActionResult> List(int page = 1)
        {
            List<Processor> output = await ProcRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<Processor>;
            if (output == null)
            {
                output = new List<Processor>();
            }
            int Count = await ProcRepo.GetCount();
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
            Processor proc = await ProcRepo.Get(id);
            if (proc == null)
            {
                proc = new Processor();
            }
            return View(proc);
        }
        public async Task<IActionResult> Update(Processor proc)
        {
            await ProcRepo.Update(proc);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await ProcRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
