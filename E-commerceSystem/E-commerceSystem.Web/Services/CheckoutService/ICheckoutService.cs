using E_commerceSystem.Models.Cart;

namespace E_commerceSystem.BusinessLogicLayer.Services.CheckoutService;

public interface ICheckoutService
{
    Task<double> CalculateSubtotalAsync(List<CartItem> items);
    Task<double> CalculateFeesAsync(List<CartItem> items);
}