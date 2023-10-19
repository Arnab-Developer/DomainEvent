using Microsoft.EntityFrameworkCore.Storage;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Infra;

public class TranManagement : ITranManagement
{
    private readonly EfContext _context;

    public TranManagement(EfContext context) => _context = context;

    async Task<IDbContextTransaction> ITranManagement.StartTranAsync() =>
        await _context.Database.BeginTransactionAsync();

    async Task ITranManagement.CommitTranAsync(IDbContextTransaction tran) =>
        await tran.CommitAsync();
}