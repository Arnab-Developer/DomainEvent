using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Infra;

public class AnotherParentRepo : IAnotherParentRepo
{
    private readonly EfContext _context;

    public AnotherParentRepo(EfContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    async Task<AnotherParent> IAnotherParentRepo.GetAsync(int id) =>
        await _context.AnotherParents.SingleAsync(p => p.Id == id);
}