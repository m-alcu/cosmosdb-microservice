namespace CoreApi.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
