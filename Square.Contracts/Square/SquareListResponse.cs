using Square.Contracts.Square;

namespace Square.Application.Queries
{
    public record SquareListResponse
    {
        public List<SquareResponse> Squares { get; set; } = new List<SquareResponse>();
    }
}
