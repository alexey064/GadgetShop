using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web;
using Web.Areas.Api.Controllers;
using Web.Repository;
using Web.Repository.IProductRepo;
using Web.UseCase;

namespace Diplom.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiController : ControllerBase
    {
        ShopContext DB;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private IProductRepo<Product> ProductRepo;
        ILinkedRepo<ProdMovement> ProdRepo;
        public ApiController(ShopContext context, UserManager<IdentityUser> usrmgr, SignInManager<IdentityUser> signmgr, 
            IProductRepo<Product> ProductRepository, ILinkedRepo<ProdMovement> ProdRepository) 
        {
            DB = context;
            userManager = usrmgr;
            signInManager = signmgr;
            ProductRepo = ProductRepository;
            ProdRepo = ProdRepository;
        }
        [Route("NewlyAdded")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<string> NewlyAdded(int skip, int count)
        {
            GetNewlyAddedUseCase NewlyAdded = new GetNewlyAddedUseCase(ProductRepo);
            try
            {
                List<Product> output = await NewlyAdded.Execute(skip, count);
                return JsonCommon.ConvertToJson(output);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("MostBuyed")]
        public async Task<string> MostBuyed(int skip, int count) 
        {
            try
            {
                GetMostBuyedUseCase MostBuyed = new GetMostBuyedUseCase(ProductRepo,ProdRepo);
                List<Product> output = await MostBuyed.Execute(skip, count);
                return JsonCommon.ConvertToJson(output);
            }
            catch (Exception e) { return e.Message; }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("MostDiscounted")]
        public async Task<string> MostDiscounted(int skip, int count) 
        {
            try
            {
                MaxDiscountedUseCase MaxDiscounted = new MaxDiscountedUseCase(ProductRepo);
                List<Product> output = await MaxDiscounted.Execute(skip, count);
                return JsonCommon.ConvertToJson(output);
            }
            catch (Exception e) { return e.Message; }
        }
        [Route("Catalog")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<string> Catalog(string type, int skip, int count) 
        {
            return JsonCommon.ConvertToJson(await ProductRepo.GetByCategory(skip, count, type) as List<Product>);
        }
        [HttpGet]
        [Route("GetProduct")]
        [AllowAnonymous]
        public async Task<string> GetProduct(int id) 
        {
            try
            {
                Product model = await ProductRepo.Get(id);
                return JsonCommon.ConvertToJson(model);
            }
            catch (Exception e) { return e.Message; }
        }
        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> Login(Dictionary<string, string> dict) 
        {
            var result = await signInManager.PasswordSignInAsync(dict["Username"],dict["Password"],true, true);
            if (!result.Succeeded)
            {
                return "wrong UserName or Password";
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, dict["Username"]) };
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

    }
}