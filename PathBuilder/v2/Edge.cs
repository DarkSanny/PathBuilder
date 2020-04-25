namespace PathBuilder.v2
{
    public class Edge
    {
        public INode From { get; }
        public INode To { get; }
        public double Weight { get; }

        public Edge(INode from, INode to, double weight = 0)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}