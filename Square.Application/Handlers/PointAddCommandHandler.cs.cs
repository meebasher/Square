using MediatR;
using Square.Api.Data.Interfaces;
using Square.Application.Commands;
using Square.Contracts.Point;
using Square.Domain.Entities;

namespace Square.Application.Handlers
{
    public class PointAddCommandHandler : IRequestHandler<PointAddCommand, PointResponse>
    {
        private readonly IPointRepository _pointRepository;
        public PointAddCommandHandler(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository;
        }
        public async Task<PointResponse> Handle(PointAddCommand command, CancellationToken cancellationToken)
        {
            var point = new Point(command.X, command.Y);

            if (await _pointRepository.PointExistsAsync(point))
            {
                await _pointRepository.AddAsync(point);
            }

            return new PointResponse(point.X, point.Y);
        }
    }
}
