using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository;
using Web.Repository.ISimpleRepo;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NotebookController : CommonCRUDController
    {
        int itemPerPage = 15;
        private ILinkedRepo<Notebook> NotebookRepo;
        private ISimpleRepo<ChargingType> BrandRepo;
        private ISimpleRepo<Department> DepRepo;
        private ISimpleRepo<Type> TypeRepo;
        private ISimpleRepo<Processor> ProcRepo;
        private ISimpleRepo<ScreenType> ScreenRepo;
        private ISimpleRepo<OS> OSRepo;
        private ISimpleRepo<Color> ColorRepo;
        private ISimpleRepo<Videocard> CardRepo;
        public NotebookController(ILinkedRepo<Notebook> NotebookRepository, ISimpleRepo<ChargingType> BrandRepository, ISimpleRepo<Department> DepRepository,
            ISimpleRepo<Type> TypeRepository, ISimpleRepo<Processor> ProcRepository, ISimpleRepo<ScreenType> ScreenRepository,
            ISimpleRepo<OS> OSRepository, ISimpleRepo<Color> ColorRepository, ISimpleRepo<Videocard> CardRepository)
        {
            NotebookRepo = NotebookRepository;
            BrandRepo = BrandRepository;
            DepRepo = DepRepository;
            TypeRepo = TypeRepository;
            ProcRepo = ProcRepository;
            ScreenRepo = ScreenRepository;
            OSRepo = OSRepository;
            ColorRepo = ColorRepository;
            CardRepo = CardRepository;
        }
        public async Task<ActionResult> List(int page = 1)
        {
            int Count = await NotebookRepo.GetCount();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await NotebookRepo.GetListFull((page - 1) * itemPerPage, itemPerPage);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            NotebookViewModel model = new NotebookViewModel();
            model.Brands = await BrandRepo.GetAll() as List<Brand>;
            model.Departments = await DepRepo.GetAll() as List<Department>;
            model.Types = await TypeRepo.GetAll() as List<Type>;
            model.Processors = await ProcRepo.GetAll() as List<Processor>;
            model.ScreenTypes = await ScreenRepo.GetAll() as List<ScreenType>;
            model.OS = await OSRepo.GetAll() as List<OS>;
            model.Colors = await ColorRepo.GetAll() as List<Color>;
            model.Videocards = await CardRepo.GetAll() as List<Videocard>;
            model.EditItem = await NotebookRepo.GetFull(id);
            if (model.EditItem == null)
            {
                model.EditItem = new Notebook();
                model.EditItem.product = new Product();
            }
            return View(model);
        }
        public async Task<IActionResult> Save(Notebook notebook, IFormFile UploadFile)
        {
            if (UploadFile != null)
            {
                notebook.product.Photo = base.LoadPhoto(UploadFile, notebook.product.Photo, "Notebook");
            }
            await NotebookRepo.Update(notebook);
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await NotebookRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}