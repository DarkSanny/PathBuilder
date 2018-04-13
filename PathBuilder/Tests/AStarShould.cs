using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace PathBuilder.Tests
{
	[TestFixture]
	public class AStarShould
	{
		private AStar _astar;
		private IMap _field;

		[SetUp]
		public void SetUp()
		{
			_astar = new AStar();
			_field = A.Fake<IMap>();
		}

		[Test]
		public void ReturnStart_WhenEndIsStart()
		{
			var start = new Point(1, 1);
			var end = start;
			A.CallTo(() => _field.GetAreaOfPoint(A<Point>.Ignored)).Returns(new List<Point>());
			A.CallTo(() => _field.IsCouldVisited(A<Point>.Ignored)).Returns(true);
			_astar.FindPath(_field, start, end).Should().BeEquivalentTo(start);
		}

		[Test]
		public void ReturnPathToNext_WhenCouldVisit()
		{
			var start = new Point(1, 1);
			var end = new Point(1, 2);
			A.CallTo(() => _field.GetAreaOfPoint(start)).Returns(new[] { end });
			A.CallTo(() => _field.IsCouldVisited(A<Point>.Ignored)).Returns(true);
			_astar.FindPath(_field, start, end).Should().BeEquivalentTo(start, end);
		}

		[Test]
		public void NotReturnPathToStart_WhenNotCouldVisit()
		{
			var start = new Point(1, 1);
			var end = new Point(1, 2);
			A.CallTo(() => _field.GetAreaOfPoint(start)).Returns(new[] { end });
			A.CallTo(() => _field.IsCouldVisited(A<Point>.Ignored)).Returns(false);
			_astar.FindPath(_field, start, end).Should().BeEquivalentTo();
		}

		[Test]
		public void ReturnPathToNext_WhenOnePosiblePath()
		{
			var start = new Point(1, 1);
			var noHere = new Point(2, 1);
			var here = new Point(2, 2);
			var end = new Point(1, 2);
			A.CallTo(() => _field.GetAreaOfPoint(start)).Returns(new[] { here, noHere });
			A.CallTo(() => _field.GetAreaOfPoint(here)).Returns(new[] { end });
			A.CallTo(() => _field.IsCouldVisited(noHere)).Returns(false);
			A.CallTo(() => _field.IsCouldVisited(here)).Returns(true);
			A.CallTo(() => _field.IsCouldVisited(end)).Returns(true);
			_astar.FindPath(_field, start, end).Should().BeEquivalentTo(start, here, end);
		}

		[Test]
		public void ReturnNearestPath()
		{
			var start = new Point(1, 1);
			var noHere = new Point(5, 1);
			var here = new Point(2, 2);
			var end = new Point(1, 2);
			A.CallTo(() => _field.GetAreaOfPoint(start)).Returns(new[] { here, noHere });
			A.CallTo(() => _field.GetAreaOfPoint(here)).Returns(new[] { end });
			A.CallTo(() => _field.GetAreaOfPoint(noHere)).Returns(new[] { end });
			A.CallTo(() => _field.IsCouldVisited(noHere)).Returns(true);
			A.CallTo(() => _field.IsCouldVisited(here)).Returns(true);
			A.CallTo(() => _field.IsCouldVisited(end)).Returns(true);
			_astar.FindPath(_field, start, end).Should().BeEquivalentTo(start, here, end);
		}

	}
}
