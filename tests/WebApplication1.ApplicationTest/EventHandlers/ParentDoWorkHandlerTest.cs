using MediatR;
using Moq;
using WebApplication1.Application.EventHandlers;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Events;
using WebApplication1.Domain.SeedWork;
using Xunit;

namespace WebApplication1.ApplicationTest.EventHandlers;

public class ParentDoWorkHandlerTest
{
    [Fact]
    public async Task Can_ParentDoWorkHandler_WorkProperly()
    {
        // Arrange
        var repoMock = new Mock<IAnotherParentRepo>();
        var noti = new ParentDoWorkEvent() { Msg = "Test msg" };
        var ap = new AnotherParent();

        repoMock
            .Setup(s => s.GetAsync(1))
            .ReturnsAsync(ap);

        INotificationHandler<ParentDoWorkEvent> handler = new ParentDoWorkHandler(repoMock.Object);

        // Act
        await handler.Handle(noti, CancellationToken.None);

        // Assert
        repoMock.Verify(v => v.GetAsync(1), Times.Once());
        repoMock.VerifyNoOtherCalls();
        Assert.True(IsEntityValid(ap));
    }

    private static bool IsEntityValid(Entity entity) =>
        entity is AnotherParent parent &&
        parent.Msg == "Another parent Test msg" &&
        parent.MsgOne == string.Empty &&
        parent.MsgTwo == string.Empty;
}