using Square.Contracts.Point;

namespace Square.Contracts.Square
{
    public record SquareResponse
    {
        public IEnumerable<PointResponse> Square { get; set; } = new List<PointResponse>();
    }
}
