using System.Collections.Generic;
using NUnit.Framework;

namespace PathBuilder.v2
{
    public class Node : INode
    {
        private List<(INode, double)> neighbors = new List<(INode, double)>();
        private readonly string name;

        public Node(string name)
        {
            this.name = name;
        }

        public IEnumerable<(INode node, double weight)> GetNeighbors()
        {
            return neighbors;
        }

        public void CreateEdge(INode node, double weight)
        {
            neighbors.Add((node, weight));
        }

        public override string ToString()
        {
            return name;
        }
    }
}