using E_commerceSystem.DataAccessLayer.Enums;

namespace E_commerceSystem.DataAccessLayer.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    
    public ProductType ProductType { get; set; }
    
    public DateOnly? ExpiryDate { get; set; }
    public double? Weight { get; set; }
    public string? Photo { get; set; }
    
    public bool IsExpired => 
        ExpiryDate.HasValue && DateOnly.FromDateTime(DateTime.Now) > ExpiryDate.Value;
    public bool IsExpirable => ProductType == ProductType.Expirable || ProductType == ProductType.ExpirableAndShippable;
    public bool IsShippable => ProductType == ProductType.Shippable || ProductType == ProductType.ExpirableAndShippable;
}