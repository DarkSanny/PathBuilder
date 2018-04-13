namespace PathBuilder
{
	public class Point
	{
		public int X { get; }
		public int Y { get; }

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return X + ":" + Y;
		}

		public override bool Equals(object obj)
		{
			return obj is Point point && (X == point.X && Y == point.Y);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return X ^ 97 + Y;
			}
		}

		public static Point operator +(Point point1, Point point2)
		{
			return new Point(point1.X + point2.X, point1.Y + point2.Y);
		}

		public static Point operator -(Point point1, Point point2)
		{
			return new Point(point1.X - point2.X, point1.Y - point2.Y);
		}
	}
}
