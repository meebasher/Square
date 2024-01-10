using Microsoft.EntityFrameworkCore;
using Square.Api.Data;
using Square.Api.Entities;
using Square.Api.Services;
using Square.Api.Services.Interfaces;

namespace Square.Api.Tests
{
    public class PointServiceTests
    {
        private readonly IPointService _pointService;

        public PointServiceTests()
        {
            var builder = new DbContextOptionsBuilder<SquareDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new SquareDbContext(builder.Options);
            var repo = new PointRepository(context);
            _pointService = new PointService(repo);
        }

        [Fact]
        public async Task AddPointTest()
        {
            await _pointService.AddPointAsync(new Point(0, 0));

            var count = (await _pointService.GetPointsAsync()).Count();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task AddPointsTest()
        {
            await _pointService.AddPointsAsync(new List<Point>
        {
            new Point(0, 0),
            new Point(0, 1),
        });

            var count = (await _pointService.GetPointsAsync()).Count();

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task DeletePointTest()
        {
            var point1 = new Point(0, 0);
            var point2 = new Point(0, 1);

            await _pointService.AddPointsAsync(new List<Point> { point1, point2 });

            var count = (await _pointService.GetPointsAsync()).Count();

            Assert.Equal(2, count);

            await _pointService.DeletePointAsync(point1);

            count = (await _pointService.GetPointsAsync()).Count();

            Assert.Equal(1, count);
        }
    }
}