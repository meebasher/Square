using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Square.Api.Middleware;
using System.Net;

namespace Square.Api.Tests
{
    public class ExceptionHandlingMiddlewareTests
    {
        private readonly Mock<RequestDelegate> _nextMock;
        private readonly Mock<ILogger<ExceptionHandlingMiddleware>> _loggerMock;
        private readonly ExceptionHandlingMiddleware _middleware;

        public ExceptionHandlingMiddlewareTests()
        {
            _nextMock = new Mock<RequestDelegate>();
            _loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
            _middleware = new ExceptionHandlingMiddleware(_nextMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task InvokeAsync_WhenNextThrowsException_ShouldHandleException()
        {
            // Arrange
            var context = new DefaultHttpContext();
            _nextMock.Setup(x => x(context)).ThrowsAsync(new Exception("Test exception"));

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
            _loggerMock.Verify(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }
    }
}
