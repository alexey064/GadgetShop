using Web.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
    public class ClientController : Controller
    {
        int itemPerPage = 15;
        private ILinkedRepo<Client> ClientRepo;
        private ISimpleRepo<Department> DepRepo;
        private ISimpleRepo<Models.Simple.Type> TypeRepo;
        public ClientController(ILinkedRepo<Client> clientRepository, ISimpleRepo<Department> depRepository,
            ISimpleRepo<Models.Simple.Type> TypeRepository)
        {
            ClientRepo = clientRepository;
            DepRepo = depRepository;
            TypeRepo = TypeRepository;
        }
        public async Task<ActionResult> List(int page = 1)
        {
            int Count = await ClientRepo.GetCount();
            int temp =(int) Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await ClientRepo.GetListFull((page - 1) * itemPerPage, itemPerPage);
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            ClientViewModel model = new ClientViewModel();
            model.Departments = await DepRepo.GetAll() as List<Department>;
            model.Posts = await TypeRepo.GetByParam("Post") as List<Models.Simple.Type>;
            model.EditItem = await ClientRepo.GetShort(id);
            if (model.EditItem == null)
            {
                model.EditItem = new Client();
                model.EditItem.Department = new Department();
                model.EditItem.Post = new Models.Simple.Type();
            }
            return View(model);
        }
        public async Task<IActionResult> Save(Client client)
        {
            if (await ClientRepo.Update(client)) 
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (await ClientRepo.Delete(id)) 
            {
                //TODO
            }
            return RedirectToAction(nameof(List));
        }
    }
}