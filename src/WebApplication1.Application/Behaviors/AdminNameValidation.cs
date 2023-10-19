using MediatR;
using WebApplication1.Application.Commands;

namespace WebApplication1.Application.Behaviors;

public class AdminNameValidation<TRequest, TResponse> : IPipelineBehavior<HelloCommand, string>
{
    async Task<string> IPipelineBehavior<HelloCommand, string>.Handle(
        HelloCommand request,
        RequestHandlerDelegate<string> next,
        CancellationToken cancellationToken)
    {
        if (request.Name.Equals("admin", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException($"{nameof(request)} Name is Admin");
        }

        var response = await next();
        return response;
    }
}