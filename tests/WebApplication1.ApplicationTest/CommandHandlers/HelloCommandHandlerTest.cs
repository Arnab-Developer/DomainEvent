using MediatR;
using Moq;
using WebApplication1.Application.CommandHandlers;
using WebApplication1.Application.Commands;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Events;
using WebApplication1.Domain.SeedWork;
using Xunit;

namespace WebApplication1.ApplicationTest.CommandHandlers;

public class HelloCommandHandlerTest
{
    [Fact]
    public async Task Can_HelloCommandHandler_WorkProperly()
    {
        // Arrange
        var repoMock = new Mock<IParentRepo>();
        var parent = new Parent();
        var token = CancellationToken.None;

        repoMock
            .Setup(s => s.GetAsync(1))
            .ReturnsAsync(parent);

        repoMock
            .Setup(s => s.UnitOfWork.SaveAsync(token));

        var command = new HelloCommand() { Name = "Jon3" };
        IRequestHandler<HelloCommand, string> handler = new HelloCommandHandler(repoMock.Object);

        // Act
        var response = await handler.Handle(command, token);

        // Assert
        Assert.Equal("Hello Jon3", response);
        Assert.True(IsEntityValid(parent));

        repoMock.Verify(v => v.GetAsync(1), Times.Once());
        repoMock.Verify(v => v.UnitOfWork.SaveAsync(token), Times.Once());
        repoMock.VerifyNoOtherCalls();
    }

    private static bool IsEntityValid(Entity entity) =>
        entity.Notifications.Count == 3 &&
        entity.Notifications[0] is ParentDoWorkEvent parentDoWorkEvent &&
        parentDoWorkEvent.Msg == "Parent do work" &&
        entity.Notifications[1] is ParentDoWorkOneEvent parentDoWorkOneEvent &&
        parentDoWorkOneEvent.Msg == "Parent do work one" &&
        entity.Notifications[2] is ParentDoWorkTwoEvent parentDoWorkTwoEvent &&
        parentDoWorkTwoEvent.Msg == "Parent do work two" &&
        entity is Parent parent &&
        parent.Msg == "Parent do work" &&
        parent.MsgOne == "Parent do work one" &&
        parent.MsgTwo == "Parent do work two";
}