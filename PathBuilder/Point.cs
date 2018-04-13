﻿namespace PathBuilder
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
			return X ^ 97 + Y;
		}
	}
}
