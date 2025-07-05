using E_commerceSystem.Models.Cart;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceSystem.Controllers;

public class CartViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();
        return View(cart);
    }
}