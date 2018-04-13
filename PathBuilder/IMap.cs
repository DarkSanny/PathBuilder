using System.Collections.Generic;

namespace PathBuilder
{
	public interface IMap
	{
		IEnumerable<Point> GetAreaOfPoint(Point point);
		bool IsCouldVisited(Point point);
	}
}
