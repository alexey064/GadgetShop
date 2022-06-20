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
    public class PurchaseHistoryController : Controller
    {
        int itemPerPage = 15;
        private IProdMov<PurchaseHistory> HistRepo;
        private ISimpleRepo<Department> DepRepo;
        private ILinkedRepo<Client> ClientRepo;
        private TypeRepository TypeRepo;
        private IProductRepo<Product> ProductRepo;
        private ILinkedRepo<ProdMovement> ProdRepo;

        public PurchaseHistoryController(IProdMov<PurchaseHistory> HistRepository, ISimpleRepo<Department> DepRepository,
            ILinkedRepo<Client> ClientRepository, ISimpleRepo<Web.Models.Simple.Type> TypeRepository,
            IProductRepo<Product> ProductRepository, ILinkedRepo<ProdMovement> ProdRepository)
        {
            HistRepo = HistRepository;
            DepRepo = DepRepository;
            ClientRepo = ClientRepository;
            TypeRepo = (TypeRepository)TypeRepository;
            ProductRepo = ProductRepository;
            ProdRepo = ProdRepository;
        }
        public async Task<ActionResult> List(int page = 1)
        {
            int Count = await HistRepo.GetCount();
            int temp = (int)Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await HistRepo.GetListFull((page - 1) * itemPerPage, itemPerPage);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            PurchHistoryViewModel model = new PurchHistoryViewModel();

            model.EditItem = await HistRepo.GetFull(id);
            model.Department = await DepRepo.GetAll() as List<Department>;
            model.People = await ClientRepo.GetListShort(0, await ClientRepo.GetCount()) as List<Client>;
            model.Status = await TypeRepo.GetByParam("Status") as List<Type>;
            if (model.EditItem == null)
            {
                model.EditItem = new PurchaseHistory();
                model.EditItem.ProdMovement = new List<ProdMovement>();
            }
            return View(model);
        }
        public async Task<IActionResult> ProdMoveEdit(int? id)
        {
            ProdLViewModel model = new ProdLViewModel();
            model.EditItem = await HistRepo.GetFull((int)id);
            model.product = await ProductRepo.GetAll() as List<Product>;
            return View(model);
        }

        public async Task<IActionResult> MoveSave(ProdMovement prod, int histId)
        {
            PurchaseHistory hist = await HistRepo.GetFull(histId);
            ProdMovement move = await ProdRepo.GetFull(prod.Id);
            if (move == null)
            {//if object is not in db then add to db
                hist.ProdMovement.Add(prod);
                await HistRepo.Update(hist);
                Product pr = await ProductRepo.Get(prod.ProductId);
                pr.Count -= prod.Count;
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
            return RedirectToAction(nameof(ProdMoveEdit), new { Id = histId });
        }
        public async Task<IActionResult> Save(PurchaseHistory purchHist)
        {
            if (await HistRepo.Update(purchHist))
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (await HistRepo.Delete(Id))
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }
    }
}