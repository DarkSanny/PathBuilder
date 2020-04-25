using System.Collections.Generic;

namespace PathBuilder.v2
{
    public interface INode
    {
        IEnumerable<(INode node, double weight)> GetNeighbors();
    }
}
