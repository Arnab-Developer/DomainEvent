using WebApplication1.Domain.Entities;

namespace WebApplication1.Domain.SeedWork;

public interface IAnotherParentRepo : IRepository<AnotherParent>
{
    public Task<AnotherParent> GetAsync(int id);
}