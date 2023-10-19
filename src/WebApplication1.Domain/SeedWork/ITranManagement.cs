using Microsoft.EntityFrameworkCore.Storage;

namespace WebApplication1.Domain.SeedWork;

public interface ITranManagement
{
    public Task<IDbContextTransaction> StartTranAsync();

    public Task CommitTranAsync(IDbContextTransaction tran);
}