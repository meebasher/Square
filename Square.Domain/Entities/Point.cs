namespace Square.Domain.Entities
{
    public class Point
    {
        public string Id { get; private set; }
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            Id = string.Concat(x, y);
            X = x;
            Y = y;
        }
    }
}
