using MediatR;

namespace WebApplication1.Application.Commands;

public class HelloCommand : IRequest<string>
{
    public string Name { get; set; }

    public HelloCommand() => Name = string.Empty;
}