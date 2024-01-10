using Square.Api.Data.Interfaces;
using Square.Api.Entities;
using Square.Api.Services.Contracts;

namespace Square.Api.Services
{
    /// <summary>
    /// Implementaion of <see cref="ISquareService"/>
    /// </summary>
    public class SquareService : ISquareService
    {
        private readonly IPointRepository _pointRepository;

        public SquareService(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository ??
            throw new ArgumentNullException(nameof(pointRepository));
        }

        /// <summary>
        /// Determines all squares
        /// </summary>
        /// <returns>Returns every unique square</returns>
        public async Task<List<Models.Square>> GetSquaresAsync()
        {
            var points = await _pointRepository.GetAllAsync();
            var pointSet = new HashSet<(int, int)>(points.Select(p => (p.X, p.Y)));
            var xToPoints = points.GroupBy(p => p.X).ToDictionary(g => g.Key, g => g.ToList());

            var squares = new List<Models.Square>();

            foreach (var pair in xToPoints)
            {
                if (pair.Value.Count < 2) continue;

                for (int i = 0; i < pair.Value.Count; i++)
                {
                    for (int j = i + 1; j < pair.Value.Count; j++)
                    {
                        var dy = Math.Abs(pair.Value[i].Y - pair.Value[j].Y);
                        var x2 = pair.Key + dy;

                        if (xToPoints.ContainsKey(x2) && pointSet.Contains((x2, pair.Value[i].Y)) 
                            && pointSet.Contains((x2, pair.Value[j].Y)))
                        {
                            squares.Add(new Models.Square
                            {
                                Points = new List<Point>
                            {
                                new Point(pair.Key, pair.Value[i].Y),
                                new Point(pair.Key, pair.Value[j].Y),
                                new Point(x2, pair.Value[i].Y),
                                new Point(x2, pair.Value[j].Y)
                            }
                            });
                        }
                    }
                }
            }

            return squares;
        }
    }
}
