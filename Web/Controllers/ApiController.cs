using Diplom.Models.EF;
using Diplom.Models.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web;

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
        public ApiController(ShopContext context, UserManager<IdentityUser> usrmgr, SignInManager<IdentityUser> signmgr) 
        {
            DB = context;
            userManager = usrmgr;
            signInManager = signmgr;
        }
        [Route("NewlyAdded")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<string> NewlyAdded()
        {
            try
            {
                List<Product> output = await DB.Products.OrderByDescending(o => o.AddDate).Take(5).ToListAsync();
                return ConvertToJson(output);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("MostBuyed")]
        public async Task<string> MostBuyed() 
        {
            try
            {
                Dictionary<int, int> temp = await DB.ProdMovements.Where(o => o.MovementTypeId == 2).GroupBy(o => o.ProductId)
                .Select(g => new { ProductId = g.Key, Count = g.Sum(o => o.Count) }).OrderByDescending(o => o.Count)
                .Take(5).ToDictionaryAsync(o => o.ProductId, o => o.Count);
                List<Product> output = new List<Product>();
                foreach (KeyValuePair<int, int> item in temp)
                {
                    output.Add(DB.Products.Where(o => o.ProductId == item.Key).FirstOrDefault());
                }
                return ConvertToJson(output);
            }
            catch (Exception e) { return e.Message; }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("MostDiscounted")]
        public async Task<string> MostDiscounted() 
        {
            try
            {
                List<Product> output = await DB.Products.Where(o => o.DiscountDate > System.DateTime.Now).OrderByDescending(o => o.Discount).Take(5).ToListAsync();
                return ConvertToJson(output);
            }
            catch (Exception e) { return e.Message; }
        }
        [Route("Catalog")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<string> Catalog(string type) 
        {
            List<Product> catalog = new List<Product>();
            switch (type)
            {
                case nameof(Notebook):
                    catalog = await DB.Products.Where(o => o.Notebook != null).ToListAsync();
                    break;
                case nameof(Smartphone):
                    catalog = await DB.Products.Where(o => o.Smartphone != null).ToListAsync();
                    break;
                case nameof(Accessory):
                    catalog = await DB.Products.Where(o => o.Accessory != null).ToListAsync();
                    break;
                case nameof(WireHeadphone):
                    catalog = await DB.Products.Where(o => o.WireHeadphones != null).ToListAsync();
                    break;
                case nameof(WirelessHeadphone):
                    catalog = await DB.Products.Where(o => o.WirelessHeadphones != null).ToListAsync();
                    break;
            }
            return ConvertToJson(catalog);
        }
        [HttpGet]
        [Route("GetProduct")]
        [AllowAnonymous]
        public async Task<string> GetProduct(int id) 
        {
            try
            {
                Product model = await DB.Products.Include(o => o.Brand).Include(o => o.Color).Include(o => o.Accessory).Include(o => o.Type)
               .Include(o => o.Notebook).ThenInclude(o => o.OS).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook)
               .ThenInclude(o => o.Processor).Include(o => o.Notebook).ThenInclude(o => o.Videocard).Include(o => o.Notebook).ThenInclude(o => o.ScreenType)
               .Include(o => o.Smartphone).ThenInclude(o => o.OS).Include(o => o.Smartphone).ThenInclude(o => o.Processor)
               .Include(o => o.Smartphone).ThenInclude(o => o.ChargingType).Include(o => o.Smartphone).ThenInclude(o => o.ScreenType)
               .Include(o => o.WireHeadphones).ThenInclude(o => o.ConnectionType)
               .Include(o => o.WirelessHeadphones).ThenInclude(o => o.ChargingType)
               .Where(o => o.ProductId == id).FirstOrDefaultAsync();
                string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return json;
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
            return ConvertToJson(encodedJwt);
        }
        [HttpGet]
        [Authorize]
        [Route("ShoppingCart")]
        public async Task<string> GetShoppingCart() 
        {
            List<Product> products = new List<Product>();
                PurchaseHistory hist = await DB.PurchaseHistories.Include(o => o.ProdMovement).ThenInclude(o => o.Product).Include(o => o.Client)
                    .Where(o => o.Client.NickName == GetUsername() && o.StatusId == 11).FirstOrDefaultAsync();
                if (hist == null)
                {
                    hist = new PurchaseHistory();
                    hist.Client = await DB.Clients.Where(o => o.NickName == GetUsername()).FirstAsync();
                    hist.StatusId = 11;
                    hist.DepartmentId = 1;
                    hist.ProdMovement = new List<ProdMovement>();
                    hist.PurchaseDate = System.DateTime.Now;
                    DB.PurchaseHistories.Add(hist);
                    await DB.SaveChangesAsync();
                }
                foreach (ProdMovement item in hist.ProdMovement)
                {
                    item.Product.Count = item.Count;
                    products.Add(item.Product);
                }
            return ConvertToJson(products);
        }
        [Authorize]
        [HttpPost]
        [Route("ShoppingCart")]
        public async Task<string> PostShoppingCart([FromBody] Dictionary<string, int> param) 
        {
            string result = "false";
            Product test = await DB.Products.FindAsync(param["id"]);
            if (test.Count > 0)
            {
                int Count = 1;
                PurchaseHistory hist = await DB.PurchaseHistories.Include(o => o.ProdMovement).Include(o => o.Client).Where(o => o.Client.NickName == GetUsername() && o.StatusId == 11).FirstOrDefaultAsync();
                if (hist == null)
                {//Если на пользователя не зарегистрирована корзина продуктов, то надо её создать
                    hist = new PurchaseHistory();
                    hist.Client = await DB.Clients.Where(o => o.NickName == GetUsername()).FirstOrDefaultAsync();
                    hist.StatusId = 11;
                    hist.ProdMovement = new List<ProdMovement>();
                    DB.PurchaseHistories.Add(hist);
                    hist.DepartmentId = DB.Products.Where(o => o.ProductId == param["id"]).First().DepartmentId;
                    await DB.SaveChangesAsync();
                }
                ProdMovement ExistedProd = hist.ProdMovement.Where(o => o.ProductId == param["id"]).FirstOrDefault();
                if (ExistedProd != null)
                {//Если в корзину добавляем ранее добавленный товар
                    ExistedProd.Count = ExistedProd.Count + Count;
                    await DB.SaveChangesAsync();
                }
                else
                {
                    ProdMovement prod = new ProdMovement();
                    prod.Count = Count;
                    prod.ProductId = param["id"];
                    prod.MovementTypeId = 2;
                    hist.ProdMovement.Add(prod);
                    DB.SaveChanges();
                }
                Product product = DB.Products.Where(o => o.ProductId == param["id"]).FirstOrDefault();
                product.Count = product.Count - Count;
                await DB.SaveChangesAsync();
                result = "true";
            }
            return result;
        }
        [Authorize]
        [HttpDelete]
        [Route("ShoppingCart")]
        public async Task<string> DeleteShoppingCart(int id) 
        {
            try
            {
                PurchaseHistory hist = await DB.PurchaseHistories.Include(o => o.ProdMovement).ThenInclude(o => o.Product)
                  .Include(o => o.Client).Where(o => o.Client.NickName == GetUsername() && o.StatusId == 11).FirstOrDefaultAsync();
                int count = hist.ProdMovement.Where(o => o.Product.ProductId == id).Select(o => o.Count).First();
                Product product = DB.Products.Where(o => o.ProductId == id).First();
                hist.ProdMovement.Remove(hist.ProdMovement.Where(o => o.ProductId == id).First());
                product.Count = product.Count + count;
                await DB.SaveChangesAsync();
                return "true";
            }
            catch (Exception e) { return "false"; }
        }
        [Authorize]
        [HttpPatch]
        [Route("CompleteOrder")]
        public async Task<string> CompleteOrder(int id) 
        {
            try
            {
                PurchaseHistory purch = await DB.PurchaseHistories.Where(o => o.Id == id).FirstAsync();
                purch.StatusId = 13;
                purch.PurchaseDate = DateTime.Now;
                await DB.SaveChangesAsync();
                return "true";
            }
            catch (Exception e) { return "false"; }
        }
        private string GetUsername() 
        {
            return HttpContext.User.Claims.ToArray()[0].Value;
        }
        private string ConvertToJson(object item) 
        {
            return JsonConvert.SerializeObject(item, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}