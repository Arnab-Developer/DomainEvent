namespace WebApplication1.Domain.SeedWork;

public interface IUnitOfWork : IDisposable
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}