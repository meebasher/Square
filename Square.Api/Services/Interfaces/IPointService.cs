using Square.Api.Entities;

namespace Square.Api.Services.Interfaces
{
    public interface IPointService
    {
        Task<IEnumerable<Point>> GetPointsAsync();
        Task AddPointsAsync(IEnumerable<Point> points);
        Task AddPointAsync(Point point);
        Task DeletePointAsync(Point point);
    }

}
