using MediatR;
using WebApplication1.Domain.Events;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Application.EventHandlers;

public class ParentDoWorkHandler : INotificationHandler<ParentDoWorkEvent>
{
    private readonly IAnotherParentRepo _repo;

    public ParentDoWorkHandler(IAnotherParentRepo repo) => _repo = repo;

    async Task INotificationHandler<ParentDoWorkEvent>.Handle(
        ParentDoWorkEvent notification,
        CancellationToken cancellationToken)
    {
        var anotherParent = await _repo.GetAsync(1);
        anotherParent.DoWork(notification.Msg);
    }
}