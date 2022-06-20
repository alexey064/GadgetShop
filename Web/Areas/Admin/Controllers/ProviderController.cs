using Web.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Repository;
using Web.Repository.IProdMov;
using Web.Repository.IProductRepo;
using Web.Repository.ISimpleRepo;
using Web.Models.Linked;
using Web.Models.Simple;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProviderController : Controller
    {
        int itemPerPage = 15;
        private IProdMov<Provider> ProvRepo;
        private ISimpleRepo<Department> DepRepo;
        private ILinkedRepo<ProdMovement> ProdRepo;
        private IProductRepo<Product> ProductRepo;
        public ProviderController(IProdMov<Provider> ProvRepository, ISimpleRepo<Department> DepRepository, 
            ILinkedRepo<ProdMovement> ProdRepository, IProductRepo<Product> ProductRepository)
        {
            ProvRepo = ProvRepository;
            DepRepo = DepRepository;
            ProdRepo = ProdRepository;
            ProductRepo = ProductRepository;
        }
        public async Task<ActionResult> List(int page = 1)
        {
            int Count = await ProvRepo.GetCount();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            ViewBag.CurrentPage = page;
            var result = await ProvRepo.GetListFull((page - 1) * itemPerPage, itemPerPage);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            ProviderViewModel model = new ProviderViewModel();
            model.EditItem = await ProvRepo.GetFull(id);
            model.Departments = await DepRepo.GetAll() as List<Department>;
            if (model.EditItem==null)
            {
                model.EditItem = new Provider();
                model.EditItem.ProdMovement = new List<ProdMovement>();
            }
            return View(model);
        }
        public async Task<IActionResult> ProdMoveEdit(int? id)
        {
            ProdMViewModel model = new ProdMViewModel();
            model.EditItem = await ProvRepo.GetFull((int)id);
            model.product = await ProductRepo.GetAll() as List<Product>;
            return View(model);
        }
        public async Task<IActionResult> MoveSave(ProdMovement prod, int ProvId)
        {
            bool result = await ProvRepo.ProdMoveAdd(ProvId, prod);
            
            return RedirectToAction(nameof(ProdMoveEdit), new { Id = ProvId });
        }
        public async Task<IActionResult> Save(Provider provider)
        {
            await ProvRepo.Update(provider);
            return RedirectToAction(nameof(List));
        }
        [HttpPost]
        public async Task<IActionResult> ProdDel(int DelId) 
        {
            int ProvId = ProdRepo.GetFull(DelId).Result.Provider.Id;
            await ProvRepo.ProdMoveDelete(DelId);
            return RedirectToAction(nameof(Edit), new { id = ProvId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await ProvRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}