using System;

namespace PathBuilder
{
	internal class AStarPoint : IComparable<AStarPoint>
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

		public int CompareTo(AStarPoint other)
		{
			return CostOfWay.CompareTo(other.CostOfWay);
		}
	}
}