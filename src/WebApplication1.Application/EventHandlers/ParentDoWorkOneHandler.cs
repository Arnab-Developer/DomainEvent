using MediatR;
using WebApplication1.Domain.Events;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Application.EventHandlers;

public class ParentDoWorkOneHandler : INotificationHandler<ParentDoWorkOneEvent>
{
    private readonly IAnotherParentRepo _repo;

    public ParentDoWorkOneHandler(IAnotherParentRepo repo) => _repo = repo;

    async Task INotificationHandler<ParentDoWorkOneEvent>.Handle(
        ParentDoWorkOneEvent notification,
        CancellationToken cancellationToken)
    {
        var anotherParent = await _repo.GetAsync(1);
        anotherParent.DoWorkOne(notification.Msg);
    }
}