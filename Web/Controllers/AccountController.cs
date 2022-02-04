using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private ShopContext DB;
        public AccountController(UserManager<IdentityUser> usrmgr, SignInManager<IdentityUser> signmgr, ShopContext context) 
        {
            userManager = usrmgr;
            signInManager = signmgr;
            DB = context;
        }
        public IActionResult AccessDenied(string ReturnUrl) 
        {
            return Redirect("/");
        }
        [HttpPost]
        public async Task<IActionResult> Register(DEL_RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Client client = new Client();
                    client.NickName = user.UserName;
                    client.Email = user.Email;
                    client.PostId = DB.Types.Where(o => o.Name == "Покупатель").FirstOrDefault().Id;
                    client.DepartmentId = 1;
                    DB.Clients.Add(client);
                    DB.SaveChanges();
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View("/Shop/Main",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(DEL_LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return RedirectToAction("main", "shop");
                    }
                    else
                    {
                        return RedirectToAction("main", "shop");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return RedirectToAction("main", "shop");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("main", "shop");
        }
        public async Task<IActionResult> PasswordChanged(string oldPassword, string newPassword, string ConfirmPassword) 
        {
            IdentityUser user = new IdentityUser(User.Identity.Name);
            IdentityResult Result=await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (Result.Succeeded)
            {
                //роль успешно изменен
                return RedirectToAction("AccountSetting", "Shop");
            }
            else return RedirectToAction("AccountSetting", "Shop");
        }
    }
}