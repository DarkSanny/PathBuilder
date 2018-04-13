using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace PathBuilder.Tests
{
	[TestFixture]
	public class BfsShould
	{
		private Bfs _bfs;
		private IMap _field;

		[SetUp]
		public void SetUp()
		{
			_bfs = new Bfs();
			_field = A.Fake<IMap>();
		}

		[Test]
		public void ReturnStart_WhenEndIsStart()
		{
			
			var start = new Point(1, 1);
			var end = start;
			A.CallTo(() => _field.GetAreaOfPoint(A<Point>.Ignored)).Returns(new List<Point>());
			A.CallTo(() => _field.IsCouldVisited(A<Point>.Ignored)).Returns(true);
			_bfs.FindPath(_field, start, end).Should().BeEquivalentTo(start);
		}

		[Test]
		public void ReturnPathToNext_WhenCouldVisit()
		{
			var start = new Point(1, 1);
			var end = new Point(1, 2);
			A.CallTo(() => _field.GetAreaOfPoint(end)).Returns(new [] {start});
			A.CallTo(() => _field.IsCouldVisited(A<Point>.Ignored)).Returns(true);
			_bfs.FindPath(_field, start, end).Should().BeEquivalentTo(start, end);
		}

		[Test]
		public void NotReturnPathToStart_WhenNotCouldVisit()
		{
			var start = new Point(1, 1);
			var end = new Point(1, 2);
			A.CallTo(() => _field.GetAreaOfPoint(end)).Returns(new[] { start });
			A.CallTo(() => _field.IsCouldVisited(A<Point>.Ignored)).Returns(false);
			_bfs.FindPath(_field, start, end).Should().BeEquivalentTo();
		}

		[Test]
		public void ReturnPathToNext_WhenOnePosiblePath()
		{
			var start = new Point(1, 1);
			var noHere = new Point(2, 1);
			var here = new Point(2, 2);
			var end = new Point(1, 2);
			A.CallTo(() => _field.GetAreaOfPoint(end)).Returns(new[] { here, noHere });
			A.CallTo(() => _field.GetAreaOfPoint(here)).Returns(new[] { start} );
			A.CallTo(() => _field.IsCouldVisited(noHere)).Returns(false);
			A.CallTo(() => _field.IsCouldVisited(here)).Returns(true);
			A.CallTo(() => _field.IsCouldVisited(start)).Returns(true);
			_bfs.FindPath(_field, start, end).Should().BeEquivalentTo(start, here, end);
		}

	}
}
