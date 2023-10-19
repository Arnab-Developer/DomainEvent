using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApplication1.Infra;

public class EfContextFactory : IDesignTimeDbContextFactory<EfContext>
{
    EfContext IDesignTimeDbContextFactory<EfContext>.CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EfContext>();
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=webapp1;Integrated Security=True");

        return new EfContext(optionsBuilder.Options);
    }
}