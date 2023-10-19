using MediatR;

namespace WebApplication1.Domain.Events;

public class ParentDoWorkTwoEvent : INotification
{
    public string Msg { get; set; } = string.Empty;

    public ParentDoWorkTwoEvent() => Msg = string.Empty;
}