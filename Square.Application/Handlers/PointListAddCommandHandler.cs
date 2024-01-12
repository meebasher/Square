using MediatR;
using Square.Api.Data.Interfaces;
using Square.Application.Commands;
using Square.Contracts.Point;
using Square.Domain.Entities;

namespace Square.Application.Handlers
{
    public class PointListAddCommandHandler : IRequestHandler<PointListAddCommand, List<PointResponse>>
    {
        private readonly IPointRepository _pointRepository;

        public PointListAddCommandHandler(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public async Task<List<PointResponse>> Handle(PointListAddCommand command, CancellationToken cancellationToken)
        {
            var responses = new List<PointResponse>();

            foreach (var pointCommand in command.Points)
            {
                var point = new Point(pointCommand.X, pointCommand.Y);

                if (await _pointRepository.PointExistsAsync(point))
                {
                    await _pointRepository.AddAsync(point);
                    responses.Add(new PointResponse(point.X, point.Y));
                }
            }

            return responses;
        }
    }
}
