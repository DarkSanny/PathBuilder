using System;
using System.Collections.Generic;
using System.Linq;

namespace PathBuilder.v2
{
    public class BFS : IPathBuilder
    {
        public IEnumerable<Edge> FindPath(INode @from, INode to)
        {
            return FindPath(from, node => node.Equals(to));
        }

        public IEnumerable<Edge> FindPath(INode @from, Predicate<INode> predicate)
        {
            var path = new Dictionary<INode, (INode, double)>();
            var queue = new Queue<INode>();
            queue.Enqueue(from);
            var currentNode = from;
            while (queue.Count != 0)
            {
                currentNode = queue.Dequeue();
                if (predicate(currentNode))
                    break;
                var nextNodes = currentNode
                    .GetNeighbors()
                    .Where(n => !path.ContainsKey(n.node));
                foreach (var (node, weight) in nextNodes)
                {
                    path.Add(node, (currentNode, weight));
                    queue.Enqueue(node);
                }
            }

            return BuildReversedPath(path, from, currentNode).Reverse();
        }

        private static IEnumerable<Edge> BuildReversedPath(Dictionary<INode, (INode, double)> path, INode @from, INode to)
        {
            if (!path.ContainsKey(to))
                yield break;
            var currentNode = to;
            while (currentNode != from)
            {
                var nextNode = path[currentNode];
                yield return new Edge(nextNode.Item1, currentNode, nextNode.Item2);
                currentNode = nextNode.Item1;
            }
        }
    }
}