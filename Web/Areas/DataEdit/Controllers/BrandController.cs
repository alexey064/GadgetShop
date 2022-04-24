using Diplom.Models.Model.simple;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Repository.ISimpleRepo;

namespace Web.Areas.DataEdit.Controllers
{
    public class BrandController : Controller
    {
        ISimpleRepo<ChargingType> BrandRepo;

        int ItemPerPage = 10;
        public BrandController(ISimpleRepo<ChargingType> BrandRepository) 
        {
            BrandRepo = BrandRepository;
        }
        public async Task<IActionResult> List(int page=1)
        {
            List<ChargingType> output = await BrandRepo.GetList((page - 1) * ItemPerPage, ItemPerPage) as List<ChargingType>;
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
            ChargingType brand =await BrandRepo.Get(id);
            if (brand==null)
            {
                brand = new ChargingType();
            }
            return View(brand);
        }
        public async Task<IActionResult> Update(ChargingType brand) 
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