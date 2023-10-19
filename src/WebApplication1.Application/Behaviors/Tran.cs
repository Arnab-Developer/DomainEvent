using MediatR;
using WebApplication1.Application.Commands;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Application.Behaviors;

public class Tran<TRequest, TResponse> : IPipelineBehavior<HelloCommand, string>
{
    private readonly ITranManagement _tranManagement;

    public Tran(ITranManagement tranManagement) => _tranManagement = tranManagement;

    async Task<string> IPipelineBehavior<HelloCommand, string>.Handle(
        HelloCommand request,
        RequestHandlerDelegate<string> next,
        CancellationToken cancellationToken)
    {
        var tran = await _tranManagement.StartTranAsync();
        var response = await next();
        await _tranManagement.CommitTranAsync(tran);
        return response;
    }
}