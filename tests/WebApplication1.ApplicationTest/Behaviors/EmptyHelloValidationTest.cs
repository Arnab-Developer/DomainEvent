using MediatR;
using Moq;
using WebApplication1.Application.Behaviors;
using WebApplication1.Application.Commands;
using Xunit;

namespace WebApplication1.ApplicationTest.Behaviors;

public class EmptyHelloValidationTest
{
    [Fact]
    public async Task Can_EmptyHelloValidation_WorkProperly()
    {
        // Arrange
        var nextMock = new Mock<RequestHandlerDelegate<string>>();

        var command = new HelloCommand() { Name = "Test User 4" };
        IPipelineBehavior<HelloCommand, string> behavior = new EmptyHelloValidation<HelloCommand, string>();

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
    public async Task Can_EmptyHelloValidation_ThrowException_GivenEmptyName()
    {
        // Arrange
        var nextMock = new Mock<RequestHandlerDelegate<string>>();

        var command = new HelloCommand() { Name = "" };
        IPipelineBehavior<HelloCommand, string> behavior = new EmptyHelloValidation<HelloCommand, string>();

        nextMock
            .Setup(s => s())
            .ReturnsAsync("Response from next pipeline item");

        // Act
        var func = () => behavior.Handle(command, nextMock.Object, CancellationToken.None);

        // Assert
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(func);
        Assert.Equal("Name is null (Parameter 'request')", ex.Message);

        nextMock.Verify(v => v(), Times.Never());
        nextMock.VerifyNoOtherCalls();
    }
}