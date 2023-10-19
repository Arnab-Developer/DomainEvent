using WebApplication1.Domain.Events;
using WebApplication1.Domain.SeedWork;

namespace WebApplication1.Domain.Entities;

public class Parent : Entity, IAggregateRoot
{
    public int Id { get; set; }

    public string Msg { get; set; }

    public string MsgOne { get; set; }

    public string MsgTwo { get; set; }

    public Parent()
    {
        Msg = string.Empty;
        MsgOne = string.Empty;
        MsgTwo = string.Empty;
    }

    public void DoWork()
    {
        Msg = "Parent do work";

        Notifications.Add(new ParentDoWorkEvent()
        {
            Msg = "Parent do work"
        });
    }

    public void DoWorkOne()
    {
        MsgOne = "Parent do work one";

        Notifications.Add(new ParentDoWorkOneEvent()
        {
            Msg = "Parent do work one"
        });
    }

    public void DoWorkTwo()
    {
        MsgTwo = "Parent do work two";

        Notifications.Add(new ParentDoWorkTwoEvent()
        {
            Msg = "Parent do work two"
        });
    }
}