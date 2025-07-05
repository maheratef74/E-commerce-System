using E_commerceSystem.DataAccessLayer.Repositories.ProductRepository;
using E_commerceSystem.DataAccessLayer.Repositories.UserRepository;
using E_commerceSystem.Models.Cart;
using E_commerceSystem.Services.CartService;
using E_commerceSystem.Services.CheckoutService;
using Microsoft.AspNetCore.Mvc;

namespace E_commerceSystem.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity)
    {
        var (success, message) = await _cartService.AddToCart(HttpContext, productId, quantity);
        if (!success)
            return BadRequest(new { message });

        var cart = _cartService.GetCart(HttpContext);
        return Json(new
        {
            totalCount = cart.Sum(c => c.Quantity),
            totalPrice = cart.Sum(c => c.Total)
        });
    }
    
    [HttpGet("/CartComponent")]
    public IActionResult CartComponent()
    {
        return ViewComponent("Cart");
    }
    
    [HttpPost]
    [Route("Cart/Checkout")]
    public async Task<IActionResult> Checkout()
    {
        var userEmail = "maheratef600@gmail.com";
        var (success, message) = await _cartService.Checkout(HttpContext, userEmail);

        if (!success)
            return BadRequest(new { message });

        return Json(new { success = true, message });
    }
    
    [HttpPost]
    [Route("Cart/RemoveFromCart")]
    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        var (success, message) = await _cartService.RemoveFromCart(HttpContext, productId);

        if (!success)
            return BadRequest(new { message });

        var cart = _cartService.GetCart(HttpContext);
        return Json(new
        {
            totalCount = cart.Sum(c => c.Quantity),
            totalPrice = cart.Sum(c => c.Total),
            message
        });
    }

    [HttpGet]
    [Route("Cart/GetCartTotal")]
    public IActionResult GetCartTotal()
    {
        var cart = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();
        var total = cart.Sum(c => c.Total);
        return Json(new { total });
    }
}