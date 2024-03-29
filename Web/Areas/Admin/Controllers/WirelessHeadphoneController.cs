﻿using Web.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Repository;
using Web.Repository.ISimpleRepo;
using Web.Models.Simple;
using Web.Models.Linked;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class WirelessHeadphoneController : CommonCRUDController
    {
        int itemPerPage = 15;
        private ILinkedRepo<WirelessHeadphone> WirelessRepo;
        private ISimpleRepo<Brand> BrandRepo;
        private ISimpleRepo<Department> DepRepo;
        private ISimpleRepo<Color> ColorRepo;
        private ISimpleRepo<ChargingType> ChargRepo;
        private TypeRepository TypeRepo;
        public WirelessHeadphoneController(ILinkedRepo<WirelessHeadphone> wirelessRepository,ISimpleRepo<Brand> BrandRepository,
            ISimpleRepo<Department> DepRepository, ISimpleRepo<Color> ColorRepository, ISimpleRepo<ChargingType> ChargRepository,
            ISimpleRepo<Models.Simple.Type> TypeRepository)
        {
            WirelessRepo = wirelessRepository;
            BrandRepo = BrandRepository;
            DepRepo = DepRepository;
            ColorRepo = ColorRepository;
            ChargRepo = ChargRepository;
            TypeRepo = (TypeRepository)TypeRepository;
        }
        public async Task<ActionResult> List(int page = 1)
        {
            int Count = await WirelessRepo.GetCount();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await WirelessRepo.GetListFull((page - 1) * itemPerPage, itemPerPage);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            WirelessHeadViewModel model = new WirelessHeadViewModel();
            model.Brands = await BrandRepo.GetAll() as List<Brand>;
            model.Departments = await DepRepo.GetAll() as List<Department>;
            model.Types = await TypeRepo.GetByParam(nameof(WirelessHeadphone)) as List<Models.Simple.Type>;
            model.Colors = await ColorRepo.GetAll() as List<Color>;
            model.ConnectionType = await ChargRepo.GetAll() as List<ChargingType>;
            model.EditItem = await WirelessRepo.GetShort(id);
            if (model.EditItem == null)
            {
                model.EditItem = new WirelessHeadphone();
                model.EditItem.Product = new Product();
            }
            return View(model);
        }
        public async Task<IActionResult> Save(WirelessHeadphone wireless, IFormFile UploadFile)
        {
            if (UploadFile!=null)
            {
                wireless.Product.Photo = base.LoadPhoto(UploadFile, wireless.Product.Photo, nameof(WirelessHeadphone));
            }
            if (await WirelessRepo.Update(wireless))
            {
                //TODO
            } 
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await WirelessRepo.Delete(id))
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }
    }
}