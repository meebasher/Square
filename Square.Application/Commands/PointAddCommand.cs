using MediatR;
using Square.Contracts.Point;

namespace Square.Application.Commands
{
    public record PointAddCommand(int X, int Y) : IRequest<PointResponse>;
}
