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
using System.Linq;
using System.Threading.Tasks;
using Web.Repository;
using Web.Repository.ISimpleRepo;

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClientController : Controller
    {
        int itemPerPage = 15;
        private ILinkedRepo<Client> ClientRepo;
        private ISimpleRepo<Department> DepRepo;
        private ISimpleRepo<Models.Model.simple.Type> TypeRepo;
        public ClientController(ILinkedRepo<Client> clientRepository, ISimpleRepo<Department> depRepository,
            ISimpleRepo<Models.Model.simple.Type> TypeRepository)
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
            model.Posts = await TypeRepo.GetByParam("Должность") as List<Models.Model.simple.Type>;
            model.EditItem = await ClientRepo.GetShort(id);
            if (model.EditItem == null)
            {
                model.EditItem = new Client();
                model.EditItem.Department = new Department();
                model.EditItem.Post = new Models.Model.simple.Type();
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