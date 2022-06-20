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
    public class MovementTypeController : Controller
    {
        ISimpleRepo<MovementType> MoveRepo;

        int ItemPerPage = 10;
        public MovementTypeController(ISimpleRepo<MovementType> MoveRepository)
        {
            MoveRepo = MoveRepository;
        }
        public async Task<IActionResult> List(int page = 1)
        {
            List<MovementType> output = await MoveRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<MovementType>;
            if (output==null)
            {
                output = new List<MovementType>();
            }
            int Count = await MoveRepo.GetCount();
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
            MovementType move = await MoveRepo.Get(id);
            if (move == null)
            {
                move = new MovementType();
            }
            return View(move);
        }
        public async Task<IActionResult> Update(MovementType move)
        {
            await MoveRepo.Update(move);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await MoveRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
