using E_commerceSystem.Models.Cart;

namespace E_commerceSystem.Services.CartService;

public interface ICartService
{
    Task<(bool Success, string Message)> AddToCart(HttpContext httpContext, int productId, int quantity);
    Task<(bool Success, string Message)> RemoveFromCart(HttpContext context, int productId);
    Task<(bool Success, string Message)> Checkout(HttpContext httpContext, string userEmail);
    List<CartItem> GetCart(HttpContext httpContext);
    void ClearCart(HttpContext httpContext);
}