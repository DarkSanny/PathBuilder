using System.Collections.Generic;

namespace PathBuilder
{
	public interface IPathBuilder
	{
		IEnumerable<Point> FindPath(IMap map, Point from, Point to);
	}
}
