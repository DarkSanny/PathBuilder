using System.Collections.Generic;
using System.Linq;
using Structures;

namespace PathBuilder
{
	public class BinaryTreeAStar : IPathBuilder
	{

		public IEnumerable<Point> FindPath(IMap map, Point from, Point to)
		{
			var points = new RandomBinaryTree<AStarPoint>();
			var path = new Dictionary<AStarPoint, AStarPoint>();
			var start = new AStarPoint(from, to, null);
			var finish = new AStarPoint(to, to, null);
			points.Add(start);
			path[start] = null;
			while (points.Count != 0)
			{
				var point = points.MinOrThrow();
				if (point.Point.Equals(to)) break;
				var nextPoints = map.GetAreaOfPoint(point.Point)
					.Where(map.IsCouldVisited)
					.Select(p => new AStarPoint(p, to, point))
					.Where(p => !path.ContainsKey(p));
				foreach (var aStarPoint in nextPoints)
				{
					points.Add(aStarPoint);
					path[aStarPoint] = point;
				}
				points.Remove(point);
			}
			foreach (var point in BuildPath(path, finish))
				yield return point;
		}

		private static IEnumerable<Point> BuildPath(
			IReadOnlyDictionary<AStarPoint, AStarPoint> path,
			AStarPoint finish)
		{
			if (!path.ContainsKey(finish)) yield break;
			var result = new Stack<Point>();
			result.Push(finish.Point);
			while (path[finish] != null)
				result.Push((finish = path[finish])?.Point);
			while (result.Count != 0)
				yield return result.Pop();
		}

	}
}
