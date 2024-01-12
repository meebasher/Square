using MediatR;
using Square.Api.Data.Interfaces;
using Square.Application.Commands;
using Square.Domain.Entities;

namespace Square.Application.Handlers
{
    internal class PointDeleteCommandHandler : IRequestHandler<PointDeleteCommand>
    {
        private readonly IPointRepository _pointRepository;
        public PointDeleteCommandHandler(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public async Task Handle(PointDeleteCommand command, CancellationToken cancellationToken)
        {
            var point = new Point(command.X, command.Y);

            if (await _pointRepository.PointExistsAsync(point))
            {
                await _pointRepository.DeleteAsync(point);
            }
        }
    }
}
