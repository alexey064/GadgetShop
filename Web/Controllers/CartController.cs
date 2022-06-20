using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.Linked;
using Web.UseCase;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private AddToCartUseCase addToCartCase;
        private GetShoppingCartUseCase GetShoppingCart;
        private DeleteFromShoppingCartUseCase DeleteShoppingCart;
        public CartController(IUseCase<AddToCartUseCase> addToCartUse, IUseCase<GetShoppingCartUseCase> getShoppingCart,
            IUseCase<DeleteFromShoppingCartUseCase> deleteShoppingCart)
        {
            addToCartCase = (AddToCartUseCase) addToCartUse;
            GetShoppingCart = (GetShoppingCartUseCase) getShoppingCart;
            DeleteShoppingCart = (DeleteFromShoppingCartUseCase) deleteShoppingCart;
        }
        public async Task<IActionResult> ShoppingCart() 
        {
            List<Product> result = await GetShoppingCart.Execute() as List<Product>;
            return View(result);
        }
        public async Task<IActionResult> AddToCart(int id, int Count, string ReturnUrl) 
        {
            bool result = await addToCartCase.Execute(id, Count, User.Identity.Name);
            return Redirect(ReturnUrl);
        }
        public async Task<IActionResult> RemoveCart(int id)
        {
;
            bool result = await DeleteShoppingCart.Execute(id);
            return RedirectToAction("ShoppingCart");
        }
    }
}