using MediatR;
using Square.Contracts.Point;

namespace Square.Application.Commands
{
    public record PointListAddCommand : IRequest<List<PointResponse>>
    {
        public List<PointAddCommand> Points { get; set; }
    }
}
