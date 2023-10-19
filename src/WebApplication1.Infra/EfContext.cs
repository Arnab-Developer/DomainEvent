using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Infra;

public class EfContext : DbContext, IUnitOfWork
{
    private readonly IMediator? _mediator;

    public DbSet<Parent> Parents { get; set; }

    public DbSet<AnotherParent> AnotherParents { get; set; }

    public EfContext(DbContextOptions<EfContext> options) : base(options) { }

    public EfContext(DbContextOptions<EfContext> options, IMediator mediator) : base(options) =>
        _mediator = mediator;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Parent>()
            .ToTable("Parents")
            .Ignore(p => p.Notifications);

        modelBuilder
            .Entity<AnotherParent>()
            .ToTable("AnotherParents")
            .Ignore(p => p.Notifications);
    }

    async Task IUnitOfWork.SaveAsync(CancellationToken cancellationToken)
    {
        if (_mediator is null)
        {
            throw new InvalidOperationException(nameof(_mediator));
        }

        await _mediator.PublishAllEventsAsync(this);
        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}