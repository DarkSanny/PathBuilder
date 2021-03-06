﻿using FluentAssertions;
using NUnit.Framework;

namespace Structures.Tests
{
	[TestFixture]
	public class RandomBinaryTreeShould
	{
		private RandomBinaryTree<int> _rbt;

		[SetUp]
		public void SetUp()
		{
			_rbt = new RandomBinaryTree<int>();
		}

		[Test]
		public void AddFirstItem()
		{
			_rbt.Insert(5);
			_rbt.Count.Should().Be(1);
		}

		[Test]
		public void AddSomeItems()
		{
			for (var i = 0; i < 100; i++)
				_rbt.Insert(5 + i);
			_rbt.Insert(20);
			_rbt.Count.Should().Be(101);
		}
		   
		[Test]
		public void RemoveItem()
		{
			for (var i = 0; i < 100; i++)
				_rbt.Insert(5 + i);
			_rbt.Remove(5);
			_rbt.Count.Should().Be(99);
		}

		[Test]
		public void HaveSameSize_WhenRemoteNonContainedItem()
		{
			for (var i = 0; i < 100; i++)
				_rbt.Insert(5 + i);
			_rbt.Remove(1);
			_rbt.Count.Should().Be(100);
		}

		[Test]
		public void HaveZeroSize_WhenAllItemsDeleted()
		{
			_rbt.Insert(5);
			_rbt.Remove(5);
			_rbt.Count.Should().Be(0);
		}

		[Test]
		public void DeleteLief()
		{
			_rbt.Insert(5);
			_rbt.Insert(6);
			_rbt.Remove(5);
			_rbt.Count.Should().Be(1);
		}

		[Test]
		public void Enumerate()
		{
			for (var i = 0; i < 5; i++)
				_rbt.Insert(i);
			_rbt.Should().BeEquivalentTo(new[] { 0, 1, 2, 3, 4 });
		}
	}
}