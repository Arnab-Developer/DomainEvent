using MediatR;
using WebApplication1.Application.Commands;

namespace WebApplication1.Application.Behaviors;

public class EmptyHelloValidation<TRequest, TResponse> : IPipelineBehavior<HelloCommand, string>
{
    async Task<string> IPipelineBehavior<HelloCommand, string>.Handle(
        HelloCommand request,
        RequestHandlerDelegate<string> next,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentNullException(nameof(request), "Name is null");
        }

        var response = await next();
        return response;
    }
}