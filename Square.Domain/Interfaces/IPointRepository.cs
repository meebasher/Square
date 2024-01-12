using Square.Domain.Entities;

namespace Square.Api.Data.Interfaces
{
    public interface IPointRepository
    {
        Task<bool> PointExistsAsync(Point point);
        Task<IEnumerable<Point>> GetAllAsync();
        Task AddRangeAsync(IEnumerable<Point> points);
        Task AddAsync(Point point);
        Task DeleteAsync(Point point);
    }
}
