using System;
using System.Collections.Generic;

namespace PathBuilder.v2
{
    public interface IPathBuilder
    {
        IEnumerable<Edge> FindPath(INode from, INode to);
        IEnumerable<Edge> FindPath(INode from, Predicate<INode> predicate);
    }
}