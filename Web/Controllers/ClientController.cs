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

namespace Diplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClientController : Controller
    {
        int itemPerPage = 15;
        private ShopContext DB;
        public ClientController(ShopContext ctx)
        {
            DB = ctx;
        }
        public async Task<ActionResult> List(int page = 0)
        {
            int Count = DB.Clients.Count();
            int temp =(int) Count / itemPerPage;
            if (temp * itemPerPage == Count)
            {
                ViewBag.MaxPage = temp;
            }
            else ViewBag.MaxPage = temp + 1;
            var result = await DB.Clients.Include(o => o.Department).Include(o => o.Post).Skip(page * itemPerPage).Take(itemPerPage).ToArrayAsync();
            return View(result);
        }
        public async Task<IActionResult> Edit(int id = 0)
        {
            ClientViewModel model = new ClientViewModel();
            model.Departments = await DB.Departments.Select(o => new { o.DepartmentId, o.Adress }).ToDictionaryAsync(o => o.DepartmentId, o => o.Adress);
            model.Posts = await DB.Types.Select(o => new { o.Id, o.Name, o.Category}).Where(o=>o.Category== "Должность").ToDictionaryAsync(o => o.Id, o => o.Name);
            model.EditItem = await DB.Clients.Include(o=>o.Department).Include(o=>o.Post).Where(o => o.Id == id).FirstOrDefaultAsync();
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
            if (client.Id == 0)
            {
                DB.Clients.Add(client);
            }
            else
            {
                var prev = await DB.Clients.Include(o => o.Department).Include(o=>o.Post).Where(o => o.Id ==client.Id).FirstAsync();
                prev.FullName = client.FullName;
                prev.Phone = client.Phone;
                prev.DepartmentId = client.DepartmentId;
                prev.PostId = client.PostId;
            }
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        public async Task<ActionResult> Delete(int id)
        {
            Client client = DB.Clients.Where(o => o.Id == id).First();
            DB.Clients.Remove(client);
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}