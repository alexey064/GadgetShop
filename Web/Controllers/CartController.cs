using Diplom.Models.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.UseCase;

namespace Diplom.Controllers
{
    public class CartController : Controller
    {
        public CartController() 
        {
        }
        public async Task<IActionResult> ShoppingCart() 
        {
            GetShoppingCartUseCase getShoppingCartUseCase = (GetShoppingCartUseCase)
                HttpContext.RequestServices.GetService(typeof(GetShoppingCartUseCase));
            List<Product> result = await getShoppingCartUseCase.Execute() as List<Product>;
            return View(result);
        }
        public async Task<IActionResult> AddToCart(int id, int Count, string ReturnUrl) 
        {
            AddToCartUseCase addToCartUseCase= (AddToCartUseCase) HttpContext.RequestServices.GetService(typeof(AddToCartUseCase));
            bool result = await addToCartUseCase.Execute(id, Count, User.Identity.Name);
            return Redirect(ReturnUrl);
        }
        public async Task<IActionResult> RemoveCart(int id)
        {
            DeleteFromShoppingCartUseCase deleteFromShoppingCart = (DeleteFromShoppingCartUseCase)
                HttpContext.RequestServices.GetService(typeof(DeleteFromShoppingCartUseCase));
            bool result = await deleteFromShoppingCart.Execute(id);
            return RedirectToAction("ShoppingCart");
        }
    }
}