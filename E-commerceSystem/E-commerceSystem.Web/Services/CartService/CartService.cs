using E_commerceSystem.Controllers;
using E_commerceSystem.DataAccessLayer.Repositories.ProductRepository;
using E_commerceSystem.DataAccessLayer.Repositories.UserRepository;
using E_commerceSystem.Models.Cart;
using E_commerceSystem.Services.CheckoutService;

namespace E_commerceSystem.Services.CartService;

public class CartService : ICartService
{
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICheckoutService _checkoutService;

    public CartService(IProductRepository productRepository, IUserRepository userRepository, ICheckoutService checkoutService)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
        _checkoutService = checkoutService;
    }

    public async Task<(bool Success, string Message)> AddToCart(HttpContext httpContext, int productId, int quantity)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null)
            return (false, "Product not found.");

        if (product.IsExpired)
            return (false, "This product is expired and cannot be added to the cart.");

        if (quantity > product.Quantity)
            return (false, $"Only {product.Quantity} units available in stock.");

        var cart = GetCart(httpContext);
        var item = cart.FirstOrDefault(i => i.ProductId == productId);

        if (item != null)
        {
            item.Quantity += quantity;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity,
                Weight = product.Weight,
                IsShippable = product.IsShippable
            });
        }

        httpContext.Session.SetObject("cart", cart);
        return (true, "Item added to cart.");    }

    public async Task<(bool Success, string Message)> RemoveFromCart(HttpContext context, int productId)
    {
        var cart = GetCart(context);
        var item = cart.FirstOrDefault(i => i.ProductId == productId);
    
        if (item == null)
            return (false, "Product not found in cart.");

        cart.Remove(item);
        context.Session.SetObject("cart", cart);

        return (true, "Product removed from cart.");
    }

    public async Task<(bool Success, string Message)> Checkout(HttpContext httpContext, string userEmail)
    {
        var cart = GetCart(httpContext);
        if (!cart.Any())
            return (false, "Your cart is empty.");

        var user = await _userRepository.GetUserByEmailAsync(userEmail);
        var subtotal = await _checkoutService.CalculateSubtotalAsync(cart);
        var shipping = await _checkoutService.CalculateFeesAsync(cart);
        var total = subtotal + shipping;

        if (user.Balance < total)
            return (false, "Insufficient balance.");

        await _userRepository.ReduceBalance(total, userEmail);

        foreach (var item in cart)
        {
            await _productRepository.ReduceTheQuntity(item.ProductId, item.Quantity);
        }

        ClearCart(httpContext);
        return (true, "Checkout completed successfully!");
    }

    public List<CartItem> GetCart(HttpContext httpContext)
    {
        return httpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();
    }

    public void ClearCart(HttpContext httpContext)
    {
        httpContext.Session.SetObject("cart", new List<CartItem>());
    }
}