using E_commerceSystem.DataAccessLayer.DbContext;
using E_commerceSystem.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_commerceSystem.DataAccessLayer.Repositories.ProductRepository;

public class ProductRepository : IProductRepository
{
    public readonly AppDbContext _appContext;

    public ProductRepository(AppDbContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _appContext.Products
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _appContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task ReduceTheQuntity(int productId, int quantity)
    {
        var product = await _appContext.Products
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product.Quantity >= quantity)
        {
            product.Quantity -= quantity;
            _appContext.Products.Update(product);
            await _appContext.SaveChangesAsync();
        }
    }
}