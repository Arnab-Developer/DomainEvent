using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Domain.Entities;

public class AnotherParent : Entity, IAggregateRoot
{
    public int Id { get; set; }

    public string Msg { get; set; }

    public string MsgOne { get; set; }

    public string MsgTwo { get; set; }

    public AnotherParent()
    {
        Msg = string.Empty;
        MsgOne = string.Empty;
        MsgTwo = string.Empty;
    }

    public void DoWork(string msg) => Msg = $"Another parent {msg}";

    public void DoWorkOne(string msg) => MsgOne = $"Another parent one {msg}";

    public void DoWorkTwo(string msg) => MsgTwo = $"Another parent two {msg}";
}