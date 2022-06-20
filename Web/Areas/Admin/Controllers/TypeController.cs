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
    public class TypeController : Controller
    {
        ISimpleRepo<Type> TypeRepo;

        int ItemPerPage = 10;
        public TypeController(ISimpleRepo<Type> TypeRepository)
        {
            TypeRepo = TypeRepository;
        }
        public async Task<IActionResult> List(int page = 1)
        {
            List<Type> output = await TypeRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<Type>;
            if (output==null)
            {
                output = new List<Type>();
            }
            int Count = await TypeRepo.GetCount();
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
            Type type = await TypeRepo.Get(id);
            if (type == null)
            {
                type = new Type();
            }
            return View(type);
        }
        public async Task<IActionResult> Update(Type type)
        {
            await TypeRepo.Update(type);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await TypeRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
