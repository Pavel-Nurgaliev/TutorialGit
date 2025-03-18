using BethanysPieShop.Models.ShoppingCart;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCart shoppingCart;
        public ShoppingCartSummary(IShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var shoppingCartViewModel = new ShoppingCartViewModel(this.shoppingCart, this.shoppingCart.GetShoppingCartTotal());

            return View(shoppingCartViewModel);
        }
    }
}
