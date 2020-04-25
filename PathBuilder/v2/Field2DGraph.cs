using System;
using System.Collections.Generic;
using System.Linq;

namespace PathBuilder.v2
{
    public class Field2DGraph : IGraph
    {
        private readonly INode[,] nodesField;

        public Field2DGraph(bool[,] field)
        {
            nodesField = BuildNodes(field);
        }

        public IEnumerable<INode> GetNodes()
        {
            return nodesField.Cast<INode>().Where(node => node != null);
        }

        private static INode[,] BuildNodes(bool[,] field)
        {
            var firstLength = field.GetLength(0);
            var secondLength = field.GetLength(1);
            var resultNodes = new Node[firstLength, secondLength];
            for (var i = 0; i < firstLength; i++)
            {
                for (var j = 0; j < secondLength; j++)
                {
                    FillNodes(resultNodes, field, i, j);
                }
            }

            // ReSharper disable once CoVariantArrayConversion
            return resultNodes;
        }

        private static void FillNodes(Node[,] nodes, bool[,] field, int i, int j)
        {
            if (field[i, j])
                return;
            var minI = Math.Max(i - 1, 0);
            var minJ = Math.Max(j - 1, 0);
            var maxI = Math.Min(i + 1, field.GetLength(0) - 1);
            var maxJ = Math.Min(j + 1, field.GetLength(1) - 1);
            if (nodes[i,j] == null)
                nodes[i, j] = new Node($"{i}:{j}");
            for (var ii = minI; ii <= maxI; ii++)
            {
                for (var jj = minJ; jj <= maxJ; jj++)
                {
                    if ((ii == i && jj == j) || field[ii, jj])
                        continue;
                    if (nodes[ii, jj] == null)
                        nodes[ii, jj] = new Node($"{ii}:{jj}");
                    nodes[i,j].CreateEdge(nodes[ii, jj], 1);
                }
            }
        }

        public INode this[int i, int j] => nodesField[i, j];
    }
}