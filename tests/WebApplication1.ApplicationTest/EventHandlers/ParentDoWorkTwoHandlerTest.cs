using MediatR;
using Moq;
using WebApplication1.Application.EventHandlers;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Events;
using WebApplication1.Domain.SeedWork;
using Xunit;

namespace WebApplication1.ApplicationTest.EventHandlers;

public class ParentDoWorkTwoHandlerTest
{
    [Fact]
    public async Task Can_ParentDoWorkTwoHandler_WorkProperly()
    {
        // Arrange
        var repoMock = new Mock<IAnotherParentRepo>();
        var noti = new ParentDoWorkTwoEvent() { Msg = "Test msg" };
        var ap = new AnotherParent();
        var token = CancellationToken.None;

        repoMock
            .Setup(s => s.GetAsync(1))
            .ReturnsAsync(ap);

        repoMock
            .Setup(s => s.UnitOfWork.SaveAsync(token));

        INotificationHandler<ParentDoWorkTwoEvent> handler = new ParentDoWorkTwoHandler(repoMock.Object);

        // Act
        await handler.Handle(noti, CancellationToken.None);

        // Assert
        repoMock.Verify(v => v.GetAsync(1), Times.Once());
        repoMock.Verify(v => v.UnitOfWork.SaveAsync(token), Times.Once());
        repoMock.VerifyNoOtherCalls();
        Assert.True(IsEntityValid(ap));
    }

    private static bool IsEntityValid(Entity entity) =>
        entity is AnotherParent parent &&
        parent.Msg == string.Empty &&
        parent.MsgOne == string.Empty &&
        parent.MsgTwo == "Another parent two Test msg";
}