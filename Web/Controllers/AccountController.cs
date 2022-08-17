using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Web.Models.EF;
using Web.Models.Linked;
using Web.Models.ViewModel;
using Web.Repository;
using Web.UseCase;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private ILinkedRepo<Client> ClientRepo;
        public AccountController(UserManager<ApplicationUser> usrmgr, SignInManager<ApplicationUser> signmgr, ILinkedRepo<Client> ClientRepository) 
        {
            userManager = usrmgr;
            signInManager = signmgr;
            ClientRepo = ClientRepository;
        }
        public IActionResult AccessDenied(string ReturnUrl) 
        {
            return Redirect("/shop/main");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            RegisterUseCase register = (RegisterUseCase) HttpContext.RequestServices.GetService<IUseCase<RegisterUseCase>>();
            bool result = await register.Execute(model);
            if (result) 
            {
                //TODO
            }
            return Redirect("/shop/main");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await signInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return Redirect("/shop/main");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/shop/main");
        }
        public async Task<IActionResult> PasswordChanged(string oldPassword, string newPassword, string ConfirmPassword) 
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = User.Identity.Name;
            IdentityResult Result=await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (Result.Succeeded)
            {
                //пароль успешно изменен
                return RedirectToAction("AccountSetting", "Shop");
            }
            else return RedirectToAction("AccountSetting", "Shop");
        }
        public IActionResult AccountSetting()
        {
            return View();
        }
    }
}