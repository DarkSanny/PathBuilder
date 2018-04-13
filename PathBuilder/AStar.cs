using System;
using System.Collections.Generic;
using System.Linq;

namespace PathBuilder
{

	public class AStar : IPathBuilder 
	{
		private class AStarPoint
		{
			public Point Point { get; }
			private double PassedWay { get; }
			private double RemainedWay { get; }
			public double CostOfWay { get; }

			public AStarPoint(Point point, Point finish, AStarPoint lastPoint)
			{
				Point = point;
				PassedWay = lastPoint == null ? 0 : lastPoint.PassedWay + DistanceOf(Point, lastPoint.Point);
				RemainedWay = DistanceOf(finish, Point);
				CostOfWay = PassedWay + RemainedWay;
			}

			private static double DistanceOf(Point point1, Point point2)
			{
				var delta = point2 - point1;
				return Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);
			}

			public override bool Equals(object obj)
			{
				if (!(obj is AStarPoint)) return false;
				var point = (AStarPoint) obj;
				return point.Point.Equals(Point);
			}

			public override int GetHashCode()
			{
				return Point.GetHashCode();
			}
		}

		public IEnumerable<Point> FindPath(IMap map, Point from, Point to)
		{
			var points = new List<AStarPoint>();
			var path = new Dictionary<AStarPoint, AStarPoint>();
			var start = new AStarPoint(from, to, null);
			var finish = new AStarPoint(to, to, null);
			points.Add(start);
			path[start] = null;
			while (points.Count != 0)
			{
				var point = FindMinOrThrow(points);
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
			foreach (var point in BuildPath(path, start, finish))
				yield return point;
		}

		private static IEnumerable<Point> BuildPath(
			IReadOnlyDictionary<AStarPoint, AStarPoint> path, 
			AStarPoint start, 
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

		private static AStarPoint FindMinOrThrow(IEnumerable<AStarPoint> list)
		{
			var min = list
				.Aggregate<AStarPoint, AStarPoint>(null, 
					(current, aStarPoint) => current == null ? aStarPoint : current.CostOfWay > aStarPoint.CostOfWay ? aStarPoint : current);
			if (min == null) throw new ArgumentException("List is empty");
			return min;
		}
	}
}
