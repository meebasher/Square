using MediatR;

namespace Square.Application.Commands
{
    public record PointDeleteCommand(int X, int Y) : IRequest;
}
