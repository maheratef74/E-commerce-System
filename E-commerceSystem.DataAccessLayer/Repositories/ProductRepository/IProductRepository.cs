using E_commerceSystem.DataAccessLayer.Entities;

namespace E_commerceSystem.DataAccessLayer.Repositories.ProductRepository;

public interface IProductRepository
{
    public Task<List<Product>> GetAllProductsAsync();
    public Task<Product?> GetProductByIdAsync(int id);
    public Task ReduceTheQuntity(int productId, int quantity);}