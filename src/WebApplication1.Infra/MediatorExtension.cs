using MediatR;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Infra;

internal static class MediatorExtension
{
    public static async Task PublishAllEventsAsync(this IMediator mediator, EfContext context)
    {
        var entities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

        var events = entities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        entities
            .ToList()
            .ForEach(entity => entity.Entity.Notifications.Clear());

        foreach (var e in events)
        {
            await mediator.Publish(e);
        }
    }
}