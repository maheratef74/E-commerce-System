using E_commerceSystem.Models.Cart;

namespace E_commerceSystem.Services.CheckoutService;

public class CheckoutService : ICheckoutService
{
    public async Task<double> CalculateSubtotalAsync(List<CartItem> items)
    {
        return items.Sum(i => i.Total);
    }

    public async Task<double> CalculateFeesAsync(List<CartItem> items)
    {
        double shippingFee = items
            .Where(i => i.IsShippable)
            .Sum(i => (i.Weight ?? 0) * i.Quantity * 0.1);

        return shippingFee;
    }
}