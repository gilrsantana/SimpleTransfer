namespace SimpleTransfer.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync();
}