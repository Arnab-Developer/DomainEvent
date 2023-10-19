using WebApplication1.Domain.Entities;

namespace WebApplication1.Domain.SeedWork;

public interface IParentRepo : IRepository<Parent>
{
    public Task<Parent> GetAsync(int id);
}