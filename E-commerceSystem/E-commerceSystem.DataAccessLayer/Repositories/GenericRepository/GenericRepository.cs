using System.Linq.Expressions;
using E_commerceSystem.DataAccessLayer.DbContext;
using Microsoft.EntityFrameworkCore;

namespace E_commerceSystem.DataAccessLayer.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private protected AppDbContext _appDbContext;
    public GenericRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Set<T>().CountAsync(cancellationToken);
    }
    public async Task<List<T>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Set<T>()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _appDbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync<TKey>(TKey id, params Expression<Func<T, object>>[] includes)
    {
        var query = _appDbContext.Set<T>().AsQueryable();
        
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        var entityType = _appDbContext.Model.FindEntityType(typeof(T));
        var primaryKey = entityType.FindPrimaryKey();
        var primaryKeyProperty = primaryKey.Properties.FirstOrDefault();

        if (primaryKeyProperty == null)
        {
            throw new InvalidOperationException($"Entity {typeof(T).Name} has no primary key defined.");
        }

        // Build expression: x => x.Id == id
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, primaryKeyProperty.Name);
        var equals = Expression.Equal(property, Expression.Constant(id));
        var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

        return await query.FirstOrDefaultAsync(lambda);
    
    }

    public async Task AddAsync(T entity)
    {
        await _appDbContext.Set<T>().AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _appDbContext.Set<T>().Update(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _appDbContext.Set<T>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }

}