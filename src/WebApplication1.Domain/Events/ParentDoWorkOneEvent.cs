using MediatR;

namespace WebApplication1.Domain.Events;

public class ParentDoWorkOneEvent : INotification
{
    public string Msg { get; set; }

    public ParentDoWorkOneEvent() => Msg = string.Empty;
}