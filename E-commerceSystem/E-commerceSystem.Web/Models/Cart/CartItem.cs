namespace E_commerceSystem.Models.Cart;

public class CartItem
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public bool IsShippable { get; set; }
    public double? Weight { get; set; }
    public double Total => Price * Quantity;
}