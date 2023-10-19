using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Infra;

public class ParentRepo : IParentRepo
{
    private readonly EfContext _context;

    public ParentRepo(EfContext context) => _context = context;

    public IUnitOfWork UnitOfWork => _context;

    async Task<Parent> IParentRepo.GetAsync(int id) =>
        await _context.Parents.SingleAsync(p => p.Id == id);
}