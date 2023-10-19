using MediatR;
using WebApplication1.Application.Commands;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Application.CommandHandlers;

public class HelloCommandHandler : IRequestHandler<HelloCommand, string>
{
    private readonly IParentRepo _repo;

    public HelloCommandHandler(IParentRepo repo) => _repo = repo;

    async Task<string> IRequestHandler<HelloCommand, string>.Handle(
        HelloCommand request,
        CancellationToken cancellationToken)
    {
        var parent = await _repo.GetAsync(1);

        parent.DoWork();
        parent.DoWorkOne();
        parent.DoWorkTwo();

        await _repo.UnitOfWork.SaveAsync(cancellationToken);
        return $"Hello {request.Name}";
    }
}