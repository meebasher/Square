using MediatR;
using Square.Api.Data.Interfaces;
using Square.Contracts.Point;
using Square.Contracts.Square;

namespace Square.Application.Queries.Handlers
{
    /// <summary>
    /// Implementaion of <see cref="ISquareQueryService"/>
    /// </summary>
    public class SquareQueryHandler : IRequestHandler<GetSquareQuery, SquareListResponse>
    {
        private readonly IPointRepository _pointRepository;

        public SquareQueryHandler(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository ??
            throw new ArgumentNullException(nameof(pointRepository));
        }

        public async Task<SquareListResponse> Handle(GetSquareQuery request, CancellationToken cancellationToken)
        {
            var points = await _pointRepository.GetAllAsync();
            var pointSet = new HashSet<(int, int)>(points.Select(p => (p.X, p.Y)));
            var xToPoints = points.GroupBy(p => p.X).ToDictionary(g => g.Key, g => g.ToList());

            var squares = new SquareListResponse();

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
                            squares.Squares.Add(new SquareResponse
                            {
                                Square = new List<PointResponse>
                                {
                                    new PointResponse(pair.Key, pair.Value[i].Y),
                                    new PointResponse(pair.Key, pair.Value[j].Y),
                                    new PointResponse(x2, pair.Value[i].Y),
                                    new PointResponse(x2, pair.Value[j].Y)
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
