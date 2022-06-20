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
    public class DepartmentController : Controller
    {
        ISimpleRepo<Department> DepRepo;

        int ItemPerPage = 10;
        public DepartmentController(ISimpleRepo<Department> DepRepository)
        {
            DepRepo = DepRepository;
        }
        public async Task<IActionResult> List(int page = 1)
        {
            List<Department> output = await DepRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<Department>;
            if (output==null)
            {
                output = new List<Department>();
            }
            int Count = await DepRepo.GetCount();
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
            Department department = await DepRepo.Get(id);
            if (department == null)
            {
                department = new Department();
            }
            return View(department);
        }
        public async Task<IActionResult> Update(Department department)
        {
            await DepRepo.Update(department);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await DepRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}
