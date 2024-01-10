using Square.Api.Data.Interfaces;
using Square.Api.Entities;
using Square.Api.Services.Interfaces;

namespace Square.Api.Services
{

    public class PointService : IPointService
    {
        private readonly IPointRepository _repository;

        public PointService(IPointRepository repository) => _repository = repository ??
            throw new System.ArgumentNullException(nameof(repository));

        public async Task AddPointAsync(Point point) => await _repository.AddAsync(point);

        public async Task AddPointsAsync(IEnumerable<Point> points) => await _repository.AddRangeAsync(points);

        public async Task DeletePointAsync(Point point) => await _repository.DeleteAsync(point);

        public async Task<IEnumerable<Point>> GetPointsAsync() => await _repository.GetAllAsync();
    }
}
