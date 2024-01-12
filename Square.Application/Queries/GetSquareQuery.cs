using MediatR;

namespace Square.Application.Queries
{
    public record GetSquareQuery : IRequest<SquareListResponse>;
}
