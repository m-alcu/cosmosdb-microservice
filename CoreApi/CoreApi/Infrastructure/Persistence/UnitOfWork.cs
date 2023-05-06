using CoreApi.Infrastructure.Database;

namespace CoreApi.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _dbContext;

    public UnitOfWork(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

}
