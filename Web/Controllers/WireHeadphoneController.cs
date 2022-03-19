using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository;
using Web.Repository.ISimpleRepo;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WireHeadphoneController : CommonCRUDController
    {
        int itemPerPage = 15;
        private ISimpleRepo<Brand> BrandRepo;
        private ISimpleRepo<Color> ColorRepo;
        private ISimpleRepo<ChargingType> ChargRepo;
        private ISimpleRepo<Department> DepRepo;
        private ISimpleRepo<Models.Model.simple.Type> TypeRepo;
        private ILinkedRepo<WireHeadphone> WireRepo;

        public WireHeadphoneController(ILinkedRepo<WireHeadphone> WireRepository, ISimpleRepo<Brand> BrandRepository,ISimpleRepo<Color> ColorRepository,
            ISimpleRepo<ChargingType> ChargRepository, ISimpleRepo<Models.Model.simple.Type> TypeRepository, ISimpleRepo<Department> DepRepository)
        {
            BrandRepo = BrandRepository;
            ColorRepo = ColorRepository;
            ChargRepo = ChargRepository;
            DepRepo = DepRepository;
            TypeRepo = TypeRepository;
            WireRepo = WireRepository;
        }
        public async Task<ActionResult> List(int page = 1)
        {
            int Count = await WireRepo.GetCount();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await WireRepo.GetListFull((page - 1) * itemPerPage, itemPerPage);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            WireHeadViewModel model = new WireHeadViewModel();
            model.Brands = await BrandRepo.GetAll() as List<Brand>;
            model.Departments = await DepRepo.GetAll() as List<Department>;
            model.Types = await TypeRepo.GetAll() as List<Models.Model.simple.Type>;
            model.Colors = await ColorRepo.GetAll() as List<Color>;
            model.ConnectionType = await ChargRepo.GetAll() as List<ChargingType>;
            model.EditItem = await WireRepo.GetShort(id);
            if (model.EditItem == null)
            {
                model.EditItem = new WireHeadphone();
                model.EditItem.Product = new Product();
            }
            return View(model);
        }
        public async Task<IActionResult> Save(WireHeadphone wire, IFormFile UploadFile)
        {
            if (UploadFile!=null)
            {
                wire.Product.Photo = base.LoadPhoto(UploadFile, wire.Product.Photo, "WireHeadphones");
            }
            if (await WireRepo.Update(wire))
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await WireRepo.Delete(id))
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }
    }
}