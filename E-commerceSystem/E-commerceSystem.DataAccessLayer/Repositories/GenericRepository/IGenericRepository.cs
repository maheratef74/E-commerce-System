using System.Linq.Expressions;

namespace E_commerceSystem.DataAccessLayer.Repositories.GenericRepository;

public interface IGenericRepository<T> where T : class
{
    Task<int> GetTotalCountAsync(CancellationToken cancellationToken = default);
    Task<List<T>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync<TKey>(TKey id, params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}