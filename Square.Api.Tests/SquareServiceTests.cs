using Moq;
using Square.Api.Data.Interfaces;
using Square.Application.Queries.Handlers;
using Square.Domain.Entities;

namespace Square.Api.Tests
{
    public class SquareServiceTests
    {
        private readonly Mock<IPointRepository> _mockRepo;
        private readonly SquareQueryHandler _squareService;

        public SquareServiceTests()
        {
            _mockRepo = new Mock<IPointRepository>();
            _squareService = new SquareQueryHandler(_mockRepo.Object);
        }

        [Fact]
        public async Task GetSquaresAsyncTest()
        {
            // Arrange
            var points = new List<Point>
        {
            new Point(0, 0),
            new Point(0, 1),
            new Point(1, 0),
            new Point(1, 1)
        };
            _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(points);

            // Act
            var result = await _squareService.GetSquaresAsync();

            // Assert
            Assert.Single(result); 
            var square = result.First();
            Assert.Contains(square.Points, p => p.X == 0 && p.Y == 0);
            Assert.Contains(square.Points, p => p.X == 0 && p.Y == 1);
            Assert.Contains(square.Points, p => p.X == 1 && p.Y == 0);
            Assert.Contains(square.Points, p => p.X == 1 && p.Y == 1);
        }
    }
}
