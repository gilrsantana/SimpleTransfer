using System.Linq.Expressions;

namespace SimpleTransfer.Common.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(int skip = 0, int take = 25);
    Task<T?> GetByIdAsync(string id);
    Task<T?[]> GetByFilterAsync(Expression<Func<T, bool>> filter);
    void AddAsync(T entity);
    void UpdateAsync(T entity);
}