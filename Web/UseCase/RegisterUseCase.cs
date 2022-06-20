using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Web.Models.Linked;
using Web.Models.ViewModel;
using Web.Repository;

namespace Web.UseCase
{
    public class RegisterUseCase : IUseCase<RegisterUseCase>
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private ILinkedRepo<Client> ClientRepo;
        public RegisterUseCase(UserManager<IdentityUser> usrmgr, SignInManager<IdentityUser> signmgr, ILinkedRepo<Client> ClientRepository) 
        {
            userManager = usrmgr;
            signInManager = signmgr;
            ClientRepo = ClientRepository;
        }
        public async Task<bool> Execute(RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) //Если удалось выполнить регистрацию, то создаем аккаунт клиента
            {
                Client client = new Client();
                client.NickName = user.UserName;
                client.Email = user.Email;
                client.PostId = 14;
                client.DepartmentId = 1;
                if (!await ClientRepo.Add(client))
                {
                    return false;
                }
                await signInManager.SignInAsync(user, isPersistent: false);
            }
            else return false;
            return true;
        }
    }
}