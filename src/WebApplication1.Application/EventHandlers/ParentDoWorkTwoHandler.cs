using MediatR;
using WebApplication1.Domain.Events;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Application.EventHandlers;

public class ParentDoWorkTwoHandler : INotificationHandler<ParentDoWorkTwoEvent>
{
    private readonly IAnotherParentRepo _repo;

    public ParentDoWorkTwoHandler(IAnotherParentRepo repo) => _repo = repo;

    async Task INotificationHandler<ParentDoWorkTwoEvent>.Handle(
        ParentDoWorkTwoEvent notification,
        CancellationToken cancellationToken)
    {
        var anotherParent = await _repo.GetAsync(1);
        anotherParent.DoWorkTwo(notification.Msg);
        await _repo.UnitOfWork.SaveAsync(cancellationToken);
    }
}