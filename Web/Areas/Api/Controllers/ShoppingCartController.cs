using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.Linked;
using Web.Repository.IProdMov;
using Web.UseCase;

namespace Web.Areas.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShoppingCartController : Controller
    {
        IProdMov<PurchaseHistory> HistRepo;
        public ShoppingCartController(IProdMov<PurchaseHistory> HistRepo) 
        {
            this.HistRepo = HistRepo;
        }
        [HttpGet]
        [Authorize]
        [Route("ShoppingCart")]
        public async Task<string> GetShoppingCart()
        {
            GetShoppingCartUseCase getShoppingCartUseCase = (GetShoppingCartUseCase)
                HttpContext.RequestServices.GetService(typeof(GetShoppingCartUseCase));
            List<Product> result = await getShoppingCartUseCase.Execute() as List<Product>;
            return JsonCommon.ConvertToJson(result);
        }
        [Authorize]
        [HttpPost]
        [Route("ShoppingCart")]
        public async Task<string> PostShoppingCart([FromBody] Dictionary<string, int> param)
        {
            AddToCartUseCase addToCartUseCase = (AddToCartUseCase)HttpContext.RequestServices.GetService(typeof(AddToCartUseCase));
            bool result = await addToCartUseCase.Execute(param["id"], 1, User.Identity.Name);
            return true.ToString();
        }
        [Authorize]
        [HttpDelete]
        [Route("ShoppingCart")]
        public async Task<string> DeleteShoppingCart(int id)
        {
            try
            {
                DeleteFromShoppingCartUseCase deleteFromShoppingCart = (DeleteFromShoppingCartUseCase)
                HttpContext.RequestServices.GetService(typeof(DeleteFromShoppingCartUseCase));
                bool result = await deleteFromShoppingCart.Execute(id);
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
                PurchaseHistory purch = await HistRepo.GetFull(id);
                purch.StatusId = 13;
                purch.PurchaseDate = DateTime.Now;
                await HistRepo.Update(purch);
                return "true";
            }
            catch (Exception e) { return "false"; }
        }
    }
}