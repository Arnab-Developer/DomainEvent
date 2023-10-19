using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using WebApplication1.Application.Behaviors;
using WebApplication1.Application.Commands;
using WebApplication1.Domain.SeedWork;
using Xunit;

namespace WebApplication1.ApplicationTest.Behaviors;

public class TranTest
{
    [Fact]
    public async Task Can_Tran_WorkProperly()
    {
        // Arrange
        var nextMock = new Mock<RequestHandlerDelegate<string>>();
        var tranManagementMock = new Mock<ITranManagement>();
        var tranMock = new Mock<IDbContextTransaction>();
        var tran = tranMock.Object;

        nextMock
            .Setup(s => s())
            .ReturnsAsync("Response from next pipeline item");

        tranManagementMock
            .Setup(s => s.StartTranAsync())
            .ReturnsAsync(tran);

        tranManagementMock
            .Setup(s => s.CommitTranAsync(tran));

        var command = new HelloCommand() { Name = "Test User 4" };
        IPipelineBehavior<HelloCommand, string> behavior = new Tran<HelloCommand, string>(tranManagementMock.Object);

        // Act
        await behavior.Handle(command, nextMock.Object, CancellationToken.None);

        // Assert
        nextMock.Verify(v => v(), Times.Once());
        nextMock.VerifyNoOtherCalls();

        tranManagementMock.Verify(v => v.StartTranAsync(), Times.Once());
        tranManagementMock.Verify(v => v.CommitTranAsync(tran), Times.Once());
        tranManagementMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Can_Tran_DoNotCommitTran_InCaseOfException()
    {
        // Arrange
        var nextMock = new Mock<RequestHandlerDelegate<string>>();
        var tranManagementMock = new Mock<ITranManagement>();
        var tranMock = new Mock<IDbContextTransaction>();
        var tran = tranMock.Object;

        nextMock
            .Setup(s => s())
            .Throws<Exception>();

        tranManagementMock
            .Setup(s => s.StartTranAsync())
            .ReturnsAsync(tran);

        tranManagementMock
            .Setup(s => s.CommitTranAsync(tran));

        var command = new HelloCommand() { Name = "Test User 4" };
        IPipelineBehavior<HelloCommand, string> behavior = new Tran<HelloCommand, string>(tranManagementMock.Object);

        // Act
        var func = () => behavior.Handle(command, nextMock.Object, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<Exception>(func);

        nextMock.Verify(v => v(), Times.Once());
        nextMock.VerifyNoOtherCalls();

        tranManagementMock.Verify(v => v.StartTranAsync(), Times.Once());
        tranManagementMock.Verify(v => v.CommitTranAsync(tran), Times.Never());
        tranManagementMock.VerifyNoOtherCalls();
    }
}