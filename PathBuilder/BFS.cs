using System.Collections.Generic;
using System.Linq;

namespace PathBuilder
{
    public class Bfs : IPathBuilder
    {
	    public IEnumerable<Point> FindPath(IMap map, Point from, Point to)
	    {
			var path = new Dictionary<Point, Point>();
		    var queue = new Queue<Point>();
		    queue.Enqueue(to);
		    path[to] = null;
		    while (queue.Count != 0)
		    {
			    var point = queue.Dequeue();
			    var nextPoints = map
				    .GetAreaOfPoint(point)
				    .Where(map.IsCouldVisited)
				    .Where(p => !path.ContainsKey(p));
			    foreach (var nextPoint in nextPoints)
			    {
				    queue.Enqueue(nextPoint);
				    path[nextPoint] = point;
			    }
			    if (path.ContainsKey(from)) break;
		    }
			if (!path.ContainsKey(from)) yield break;
		    yield return from;
		    while (from != null && path[from] != null)
			    yield return from = path[from];
		}
    }
}
