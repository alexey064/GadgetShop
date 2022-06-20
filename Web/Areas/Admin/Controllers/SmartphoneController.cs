using Web.Models.ViewModel;
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
    public class SmartphoneController : CommonCRUDController
    {
        int itemPerPage = 15;
        private ISimpleRepo<Brand> BrandRepo;
        private ISimpleRepo<Department> DepRepo;
        private ISimpleRepo<Processor> ProcRepo;
        private ISimpleRepo<ScreenType> ScreenRepo;
        private ISimpleRepo<OS> OSRepo;
        private ISimpleRepo<Color> ColorRepo;
        private ISimpleRepo<ChargingType> ChargRepo;
        private ILinkedRepo<Smartphone> SmartRepo;
        private TypeRepository TypeRepo;
        public SmartphoneController(ISimpleRepo<Brand> BrandRepository, ISimpleRepo<Department> DepRepository,
            ISimpleRepo<Processor> ProcRepository, ISimpleRepo<ScreenType> ScreenRepository, ISimpleRepo<OS> OSRepository,
            ISimpleRepo<Color> ColorRepository, ISimpleRepo<ChargingType> ChargRepository, ILinkedRepo<Smartphone> SmartRepository,
            ISimpleRepo<Type> TypeRepository)
        {
            BrandRepo = BrandRepository;
            DepRepo = DepRepository;
            ProcRepo = ProcRepository;
            ScreenRepo = ScreenRepository;
            OSRepo = OSRepository;
            ColorRepo = ColorRepository;
            OSRepo = OSRepository;
            ColorRepo = ColorRepository;
            ChargRepo = ChargRepository;
            SmartRepo = SmartRepository;
            TypeRepo = (TypeRepository)TypeRepository;
        }
        public async Task<ActionResult> List(int page = 1)
        {
            int Count = await SmartRepo.GetCount();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await SmartRepo.GetListFull((page - 1) * itemPerPage, itemPerPage);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            SmartphoneViewModel model = new SmartphoneViewModel();
            model.Brands = await BrandRepo.GetAll() as List<Brand>;
            model.Department = await DepRepo.GetAll() as List<Department>;
            model.Processors = await  ProcRepo.GetAll() as List<Processor>;
            model.ScreenTypes = await ScreenRepo.GetAll() as List<ScreenType>;
            model.Types = await TypeRepo.GetByParam(nameof(Smartphone)) as List<Type>;
            model.OS = await OSRepo.GetAll() as List<OS>;
            model.Colors = await ColorRepo.GetAll() as List<Color>;
            model.ChargingType = await ChargRepo.GetAll() as List<ChargingType>;
            model.EditItem =await  SmartRepo.GetShort(id);
            if (model.EditItem == null)
            {
                model.EditItem = new Smartphone();
                model.EditItem.Product = new Product();
            }
            return View(model);
        }
        public async Task<IActionResult> Save(Smartphone smartphone, IFormFile UploadFile)
        {
            if (UploadFile!=null)
            {
                smartphone.Product.Photo = base.LoadPhoto(UploadFile, smartphone.Product.Photo, nameof(Smartphone));
            }
            if (await SmartRepo.Update(smartphone)) 
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (await SmartRepo .Delete(id)) 
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }
    }
}