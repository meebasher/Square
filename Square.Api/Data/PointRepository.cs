using Microsoft.EntityFrameworkCore;
using Square.Api.Data.Interfaces;
using Square.Api.Entities;

namespace Square.Api.Data
{
    public class PointRepository : IPointRepository
    {
        private readonly SquareDbContext _context;

        public PointRepository(SquareDbContext context) => _context = context ??
            throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Add point to database.
        /// </summary>
        /// <param name="point"></param>
        public async Task AddAsync(Point point)
        {
            await _context.AddAsync(point);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds <see cref="IEnumerable{Point}"/> points to database.
        /// </summary>
        /// <param name="points"></param>
        public async Task AddRangeAsync(IEnumerable<Point> points)
        {
            await _context.AddRangeAsync(points);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes specified point.
        /// </summary>
        /// <param name="point"></param>
        public async Task DeleteAsync(Point point)
        {
            var pointToDelete = await _context.Points.
                FirstOrDefaultAsync(p => p.X == point.X && p.Y == point.Y);

            if (pointToDelete != null)
            {
                _context.Points.Remove(pointToDelete);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets all points from Database
        /// </summary>
        /// <returns>List of all point from database</returns>
        public async Task<IEnumerable<Point>> GetAllAsync() => await _context.Points.ToListAsync();
    }
}
