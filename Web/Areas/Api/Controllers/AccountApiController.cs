using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Areas.Api.Models;
using Web.Models.EF;
using Web.Models.ViewModel;
using Web.UseCase;

namespace Web.Areas.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AccountApiController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountApiController(UserManager<ApplicationUser> usrmgr, SignInManager<ApplicationUser> signmgr) 
        {
            userManager = usrmgr;
            signInManager = signmgr;
        }
        [Route("UpdateToken")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponse> UpdateToken(Dictionary<string, string> dict) 
        {
            var response = this.Response;
            RefreshToken refToken = new RefreshToken();

            TokenModel output = new TokenModel();
            ApplicationUser user = userManager.Users.FirstOrDefault(o => o.RefreshToken == dict["RefreshToken"]);
            if (user != null)
            {
                if (user.RefreshTokenExpiryTime < DateTime.UtcNow.AddDays(10) && user.RefreshTokenExpiryTime > DateTime.UtcNow)
                {
                    //get refresh Token
                    GenerateRefreshTokenUseCase GenerateRefreshToken = new GenerateRefreshTokenUseCase();
                    refToken = GenerateRefreshToken.Execute();

                    user.RefreshToken = refToken.Token;
                    user.RefreshTokenExpiryTime = refToken.ExpiredDate;
                    await userManager.UpdateAsync(user);
                }
                else { output.RefreshToken = user.RefreshToken; }
                //get access Token
                GenerateAccessTokenUseCase generateAccessToken = new GenerateAccessTokenUseCase();
                output.AccessToken=generateAccessToken.Execute(user);
                
            }
            else 
            {
                response.StatusCode = StatusCodes.Status401Unauthorized;
                await response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonCommon.ConvertToJson("wrong refresh token")));
                return response;
            }
            await response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonCommon.ConvertToJson(output)));
            return response;
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponse> Login(Dictionary<string, string> dict)
        {
            var response = this.Response;
            string token;
            var result =
                    await signInManager.PasswordSignInAsync(dict["Username"].ToString(), dict["Password"].ToString(), true, false);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(dict["Username"]);
                GenerateAccessTokenUseCase genAccessToken = new GenerateAccessTokenUseCase();
                token = genAccessToken.Execute(user);
                if (user.RefreshTokenExpiryTime<DateTime.Now.AddDays(10))
                {
                    GenerateRefreshTokenUseCase genRefreshToken = new GenerateRefreshTokenUseCase();
                    RefreshToken refreshToken = genRefreshToken.Execute();

                    user.RefreshToken = refreshToken.Token;
                    user.RefreshTokenExpiryTime = refreshToken.ExpiredDate;

                    await userManager.UpdateAsync(user);
                }

                TokenModel output = new TokenModel();
                output.AccessToken = token;
                output.RefreshToken = user.RefreshToken;
                await response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonCommon.ConvertToJson(output)));
                response.StatusCode = StatusCodes.Status200OK;
                return response;
            }
            response.StatusCode = StatusCodes.Status401Unauthorized;
            response.Body.Write(Encoding.UTF8.GetBytes("wrong UserName or Password"));
            return response;
        }

        private string GetUsername()
        {
            return HttpContext.User.Claims.ToArray()[0].Value;
        }
        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponse> Register(Dictionary<string, string> dict) 
        {
            var response = this.Response;
            RegisterUseCase register = (RegisterUseCase)HttpContext.RequestServices.GetService<IUseCase<RegisterUseCase>>();
            RegisterViewModel model = new RegisterViewModel();
            model.UserName = dict["UserName"];
            model.Password = dict["Password"];
            bool result = await register.Execute(model);
            if (result)
            {
                response.StatusCode = StatusCodes.Status200OK;
            }
            else { response.StatusCode = StatusCodes.Status401Unauthorized; }
            return response;
        }
    }
}