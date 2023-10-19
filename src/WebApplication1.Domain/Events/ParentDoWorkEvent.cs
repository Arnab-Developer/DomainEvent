using MediatR;

namespace WebApplication1.Domain.Events;

public class ParentDoWorkEvent : INotification
{
    public string Msg { get; set; }

    public ParentDoWorkEvent() => Msg = string.Empty;
}