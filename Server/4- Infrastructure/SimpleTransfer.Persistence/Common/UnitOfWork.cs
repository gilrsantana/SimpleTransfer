using SimpleTransfer.Common.Interfaces;
using SimpleTransfer.Data.Context;

namespace SimpleTransfer.Persistence.Common;

public class UnitOfWork(SimpleTransferContext context) : IUnitOfWork
{
    private readonly SimpleTransferContext _context = context;
    private readonly Dictionary<Type, object> _repositories = new();

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        
        var repository = new Repository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}