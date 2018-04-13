using System;
using System.Collections.Generic;
using System.Linq;
using PathBuilder;

namespace Drawer
{
	public class SimpleField : IMap
	{

		private readonly bool[][] _field;
		private List<Point> _path;

		public SimpleField(IEnumerable<string> maze)
		{
			  _field = maze
				  .Select(i => i
					  .Select(j => j != '#')
					  .ToArray())
				  .ToArray();
		}

		public IEnumerable<Point> GetAreaOfPoint(Point point)
		{
			return new[]
			{
				new Point(point.X, point.Y - 1),
				new Point(point.X - 1, point.Y),
				new Point(point.X + 1, point.Y),
				new Point(point.X, point.Y + 1)
			};
		}

		public bool IsCouldVisited(Point point)
		{
			return IsInsideMap(point) && _field[point.Y][point.X];
		}

		public void FindPath(IPathBuilder builder, Point from, Point to)
		{
			_path = builder.FindPath(this, from, to).ToList();
		}

		private bool IsInsideMap(Point point)
		{
			return point.Y >= 0 && point.Y < _field.Length
			       && point.X >= 0 && point.X < _field[point.Y].Length;
		}

		public void DrawField()
		{
			for (var i = 0; i < _field.Length; i++)
			{
				for (var j = 0; j < _field[i].Length; j++)
				{
					Console.ForegroundColor = (_path != null && _path.Contains(new Point(j, i))) ? ConsoleColor.Red : ConsoleColor.White;
					var symbol = (_field[i][j] ? '.' : '#');
					Console.Write(symbol);
				}
				Console.WriteLine();
			}
		}
	}
}
