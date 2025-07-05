using E_commerceSystem.DataAccessLayer.Entities;

namespace E_commerceSystem.Models.Products;

public class HomeModelVM
{
    public User User { get; set; } = null!;
    public List<Product> Products { get; set; } = new List<Product>();
}