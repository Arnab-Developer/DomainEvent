using MediatR;
using Moq;
using WebApplication1.Application.Behaviors;
using WebApplication1.Application.Commands;
using Xunit;

namespace WebApplication1.ApplicationTest.Behaviors;

public class AdminNameValidationTest
{
    [Fact]
    public async Task Can_AdminNameValidation_WorkProperly()
    {
        // Arrange
        var nextMock = new Mock<RequestHandlerDelegate<string>>();

        var command = new HelloCommand() { Name = "Test User 4" };
        IPipelineBehavior<HelloCommand, string> behavior = new AdminNameValidation<HelloCommand, string>();

        nextMock
            .Setup(s => s())
            .ReturnsAsync("Response from next pipeline item");

        // Act
        var response = await behavior.Handle(command, nextMock.Object, CancellationToken.None);

        // Assert
        Assert.Equal("Response from next pipeline item", response);

        nextMock.Verify(v => v(), Times.Once());
        nextMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Can_AdminNameValidation_ThrowException_GivenCapitalAdminName()
    {
        // Arrange
        var nextMock = new Mock<RequestHandlerDelegate<string>>();

        var command = new HelloCommand() { Name = "Admin" };
        IPipelineBehavior<HelloCommand, string> behavior = new AdminNameValidation<HelloCommand, string>();

        nextMock
            .Setup(s => s())
            .ReturnsAsync("Response from next pipeline item");

        // Act
        var func = () => behavior.Handle(command, nextMock.Object, CancellationToken.None);

        // Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(func);
        Assert.Equal("request Name is Admin", ex.Message);

        nextMock.Verify(v => v(), Times.Never());
        nextMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Can_AdminNameValidation_ThrowException_GivenSmallAdminName()
    {
        // Arrange
        var nextMock = new Mock<RequestHandlerDelegate<string>>();

        var command = new HelloCommand() { Name = "admin" };
        IPipelineBehavior<HelloCommand, string> behavior = new AdminNameValidation<HelloCommand, string>();

        nextMock
            .Setup(s => s())
            .ReturnsAsync("Response from next pipeline item");

        // Act
        var func = () => behavior.Handle(command, nextMock.Object, CancellationToken.None);

        // Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(func);
        Assert.Equal("request Name is Admin", ex.Message);

        nextMock.Verify(v => v(), Times.Never());
        nextMock.VerifyNoOtherCalls();
    }
}