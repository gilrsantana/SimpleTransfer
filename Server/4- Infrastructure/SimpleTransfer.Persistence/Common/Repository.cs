using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleTransfer.Common.Interfaces;
using SimpleTransfer.Data.Context;

namespace SimpleTransfer.Persistence.Common;

public class Repository<T>(SimpleTransferContext context) : IRepository<T>
    where T : class
{
    protected readonly SimpleTransferContext Context = context;
    protected readonly DbSet<T> DbSet = context.Set<T>();


    public async Task<IEnumerable<T>> GetAllAsync(int skip = 0, int take = 25)
    {
        return await DbSet.Skip(skip).Take(take).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        return await DbSet.FindAsync(id);
    }

    public Task<T?[]> GetByFilterAsync(Expression<Func<T, bool>> filter)
    {
        IQueryable<T?> query = DbSet.Where(filter);
        return query.ToArrayAsync();
    }

    public void AddAsync(T entity)
    {
        DbSet.AddAsync(entity);
    }

    public void UpdateAsync(T entity)
    {
        DbSet.Update(entity);
    }
}