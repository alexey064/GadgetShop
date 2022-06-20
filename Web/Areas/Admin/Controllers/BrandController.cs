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
    public class BrandController : Controller
    {
        ISimpleRepo<Brand> BrandRepo;

        int ItemPerPage = 10;
        public BrandController(ISimpleRepo<Brand> BrandRepository) 
        {
            BrandRepo = BrandRepository;
        }
        public async Task<IActionResult> List(int page=1)
        {
            List<Brand> output = await BrandRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<Brand>;
            if (output==null)
            {
                output = new List<Brand>();
            }
            int Count = await BrandRepo.GetCount();
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
            Brand brand;
            if (id==0)
            {
                brand = new Brand();
            }
            else brand =await BrandRepo.Get(id);
            return View(brand);
        }
        public async Task<IActionResult> Update(Brand brand) 
        {
            await BrandRepo.Update(brand);
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id) 
        {
            await BrandRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}