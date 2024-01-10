using System.Text.Json.Serialization;

namespace Square.Api.Entities
{
    public class Point 
    {
        [JsonIgnore]
        public Guid Id { get; private set; }
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            Id = Guid.NewGuid();
            X = x;
            Y = y;
        }
    }
}
