﻿using System;
using System.Diagnostics;
using PathBuilder;

namespace Drawer
{
	public class Program
	{

		public static AStar Astar = new AStar();
		public static Bfs Bfs = new Bfs();

		public static void Main(string[] args)
		{
			//MazeTest();
			LongerTest();
		}

		public static void MazeTest()
		{
			var maze = new[]
			{
				"##################################",
				"#..#.............................#",
				"#..#.............................#",
				"#..#.###########################.#",
				"#..#.#...........................#",
				"#..#.#...........................#",
				"#..#.#############################",
				"#..#.............................#",
				"#..###########################...#",
				"#..#.............................#",
				"#................................#",
				"#................................#",
				"##################################",
			};
			var field = new SimpleField(maze);
			var start = new Point(1, 1);
			var end = new Point(10, 4);
			field.FindPath(Bfs, start, end);
			field.DrawField();
			var watch = new Stopwatch();
			watch.Start();
			for (var i = 0; i < 100000; i++)
				field.FindPath(Bfs, start, end);
			watch.Stop();
			Console.WriteLine(watch.ElapsedMilliseconds);
			field.FindPath(Astar, start, end);
			watch.Restart();
			for (var i = 0; i < 100000; i++)
				field.FindPath(Astar, start, end);
			watch.Stop();
			Console.WriteLine(watch.ElapsedMilliseconds);
		}

		public static void LongerTest()
		{
			var maze = new[]
			{
				"##################################################################################################################################",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"#................................................................................................................................#",
				"##################################################################################################################################",
			};
			var field = new SimpleField(maze);
			var start = new Point(2, 5);
			var end = new Point(127, 5);
			field.FindPath(Bfs, start, end);
			field.DrawField();
			var watch = new Stopwatch();
			watch.Start();
			for (var i = 0; i < 10000; i++)
				field.FindPath(Bfs, start, end);
			watch.Stop();
			Console.WriteLine(watch.ElapsedMilliseconds);
			field.FindPath(Astar, start, end);
			watch.Restart();
			for (var i = 0; i < 10000; i++)
				field.FindPath(Astar, start, end);
			watch.Stop();
			Console.WriteLine(watch.ElapsedMilliseconds);
		}
	}
}