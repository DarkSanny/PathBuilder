using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Structures.Tests
{
	[TestFixture]
	public class AvlTreeShould
	{
		private AvlTree<int> _avlTree;

		[SetUp]
		public void SetUp()
		{
			_avlTree = new AvlTree<int>();
		}

		[Test]
		public void AddFirstItem()
		{
			_avlTree.Insert(5);
			_avlTree.Head.Value.Should().Be(5);
		}

		[Test]
		public void AddGreaterItem()
		{
			_avlTree.Insert(5);
			_avlTree.Insert(6);
			_avlTree.Head.Right.Value.Should().Be(6);
		}

		[Test]
		public void AddLowerItem()
		{
			_avlTree.Insert(5);
			_avlTree.Insert(4);
			_avlTree.Head.Left.Value.Should().Be(4);
		}

		[Test]
		public void RemoveHead()
		{
			_avlTree.Insert(5);
			_avlTree.Remove(5);
			_avlTree.Head.Should().Be(null);
			_avlTree.Count.Should().Be(0);
		}

		[Test]
		public void DoNotRemoveNotExistItem()
		{
			_avlTree.Insert(5);
			_avlTree.Remove(4);
			_avlTree.Head.Value.Should().Be(5);
			_avlTree.Count.Should().Be(1);
		}

		[Test]
		public void DoNotRemove_WhenNoItem()
		{
			_avlTree.Remove(4);
			_avlTree.Head.Should().Be(null);
			_avlTree.Count.Should().Be(0);
		}

		[Test]
		public void Enumerate()
		{
			for (var i = 0; i < 5; i++)
				_avlTree.Insert(i);
			_avlTree.Should().BeEquivalentTo(new[] {0, 1, 2, 3, 4});
		}

		[Test]
		public void EnumerateRandomSequence()
		{
			var sequence = Enumerable.Range(0, 10).ToList();
			var random = new Random();
			foreach (var item in sequence.OrderBy(i => random.Next()))
				_avlTree.Insert(item);
			_avlTree.Should().BeEquivalentTo(sequence);
		}

		[Test]
		public void RemoveRandom()
		{
			var sequence = Enumerable.Range(0, 5);
			var random = new Random();
			foreach (var item in sequence.OrderBy(i => random.Next()))
				_avlTree.Insert(item);
			_avlTree.Remove(3);
			_avlTree.Should().BeEquivalentTo(new [] {0, 1, 2, 4});
		}

		[Test]
		public void FindMin()
		{
			for (var i = 0; i < 5; i++)
				_avlTree.Insert(i);
			_avlTree.GetMinOrThrow().Should().Be(0);
		}

		[Test]
		public void FindMax()
		{
			for (var i = 0; i < 5; i++)
				_avlTree.Insert(i);
			_avlTree.GetMaxOrThrow().Should().Be(4);
		}
	}
}
