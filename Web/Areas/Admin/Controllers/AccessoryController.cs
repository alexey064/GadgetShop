using Web.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Repository;
using Web.Repository.ISimpleRepo;
using Web.Models.Linked;
using Web.Models.Simple;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AccessoryController : CommonCRUDController
    {
        int itemPerPage = 15;
        private ILinkedRepo<Accessory> Repo;
        private ISimpleRepo<Brand> BrandRepo;
        private ISimpleRepo<Department> DepRepo;
        private ISimpleRepo<Color> ColorRepo;
        private ISimpleRepo<Type> TypeRepo;
        public AccessoryController(ILinkedRepo<Accessory> repo, ISimpleRepo<Brand> BrandRepository,
            ISimpleRepo<Department> DepRepository, ISimpleRepo<Color> ColorRepository, ISimpleRepo<Models.Simple.Type> TypeRepository) 
        {
            Repo = repo;
            BrandRepo = BrandRepository;
            DepRepo = DepRepository;
            ColorRepo = ColorRepository;
            TypeRepo = TypeRepository;
        }
        public async Task<ActionResult> List(int page=1)
        {
            int Count = await Repo.GetCount();
            int temp =(int) Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await Repo.GetListFull((page-1)*itemPerPage, itemPerPage);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            AccessoryViewModel model = new AccessoryViewModel();
            model.Brands = await BrandRepo.GetAll() as List<Brand>;
            model.department = await DepRepo.GetAll() as List<Department>;
            model.types = await TypeRepo.GetByParam(nameof(Accessory)) as List<Models.Simple.Type>;
            model.Colors = await ColorRepo.GetAll() as List<Color>;
            model.EditItem = await Repo.GetFull(id);
            if (model.EditItem == null)
            {
                model.EditItem = new Accessory();
                model.EditItem.Product = new Product();
            }
            return View("Edit",model);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Accessory accessory, IFormFile UploadFile) 
        {
            if (UploadFile!=null)
            {
                accessory.Product.Photo = base.LoadPhoto(UploadFile, accessory.Product.Photo, nameof(Accessory));
            }
            if (await Repo.Update(accessory)) 
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            
            if (await Repo.Delete(id))
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }
    }
}