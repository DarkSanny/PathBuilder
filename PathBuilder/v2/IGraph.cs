using System.Collections.Generic;

namespace PathBuilder.v2
{
    public interface IGraph
    {
        IEnumerable<INode> GetNodes();
    }
}