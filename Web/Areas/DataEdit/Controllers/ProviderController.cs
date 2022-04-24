using Diplom.Models.EF;
using Diplom.Models.Model;
using Diplom.Models.Model.simple;
using Diplom.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Repository;
using Web.Repository.IProdMov;
using Web.Repository.IProductRepo;
using Web.Repository.ISimpleRepo;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
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
            Provider prov = await ProvRepo.GetFull(ProvId);
            ProdMovement move = await ProdRepo.GetFull(prod.Id);
            if (move == null)
            {//If object is not in DB then add to db
                prov.ProdMovement.Add(prod);
                await ProvRepo.Update(prov);
                Product pr = await ProductRepo.Get(prod.ProductId);
                pr.Count += prod.Count;
                await ProductRepo.Update(pr);
            }
            else
            {
                int ChangedCount = prod.Count - move.Count;
                Product product = await ProductRepo.Get(move.ProductId);
                product.Count = product.Count + ChangedCount;
                await ProductRepo.Update(product);
                move.Count = prod.Count;
                move.ProductId = prod.ProductId;
            }
            await ProdRepo.Update(move);
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
            await ProvRepo.ProdMoveDelete(DelId);
            ProdMovement output = await ProdRepo.GetFull(DelId);
            return RedirectToAction(nameof(Edit), new { id = output.Provider.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await ProvRepo.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}