using MediatR;

namespace WebApplication1.Domain.SeedWork;

public abstract class Entity
{
    public IList<INotification> Notifications { get; set; }

    public Entity() => Notifications = new List<INotification>();
}