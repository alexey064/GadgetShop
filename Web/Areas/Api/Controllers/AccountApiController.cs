using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models.ViewModel;
using Web.UseCase;

namespace Web.Areas.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]api")]
    public class AccountApiController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountApiController(UserManager<IdentityUser> usrmgr, SignInManager<IdentityUser> signmgr) 
        {
            userManager = usrmgr;
            signInManager = signmgr;
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Login(Dictionary<string, string> dict)
        {
            var result = await signInManager.PasswordSignInAsync(dict["UserName"], dict["Password"], true, true);
            if (!result.Succeeded)
            {
                return "wrong UserName or Password";
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, dict["UserName"]) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.MINUTES)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return JsonCommon.ConvertToJson(encodedJwt);
        }
        private string GetUsername()
        {
            return HttpContext.User.Claims.ToArray()[0].Value;
        }
        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Register(Dictionary<string, string> dict) 
        {
            RegisterUseCase register = (RegisterUseCase)HttpContext.RequestServices.GetService<IUseCase<RegisterUseCase>>();
            RegisterViewModel model = new RegisterViewModel();
            model.UserName = dict["UserName"];
            model.Password = dict["Password"];
            bool result = await register.Execute(model);
            if (result)
            {
                string Islogin= await Login(dict);
                if (Islogin.Length>40)
                {
                    return Islogin;
                }
            }
            return "false";
        }
    }
}