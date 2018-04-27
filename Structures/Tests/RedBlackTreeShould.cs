using FluentAssertions;
using NUnit.Framework;

namespace Structures.Tests
{
	[TestFixture]
	public class RedBlackTreeShould
	{
		private RedBlackTree<int> _tree;

		[SetUp]
		public void SetUp()
		{
			_tree = new RedBlackTree<int>();
		}

		//Тесты, которые будут не доступны после релиза
		//Мне лень делать тесты через рефлексию, поэтому чтобы тесты заработали нужно сделать публичным поле Head

		[Test]
		public void Rotateleft_WhenTwoNodes()
		{
			var x = new RbtNode<int>(1, null, true);
			var y = new RbtNode<int>(2, x, false);
			x.Right = y;
			_tree.Head = x;
			_tree.RotateToLeft(x);
			_tree.Head.Left.Should().Be(x);
			_tree.Head.Parent.Should().Be(RbtNode<int>.Nil);
			_tree.Head.Left.Parent.Should().Be(y);
			_tree.Head.Right.Should().Be(RbtNode<int>.Nil);
			_tree.Head.Left.Right.Parent.Should().Be(null);
		}

		[Test]
		public void ReturnNode_WhenRotateLeft_WithOneNode()
		{
			var x = new RbtNode<int>(1, null, true);
			_tree.Head = x;
			_tree.RotateToLeft(x);
			_tree.Head.Should().Be(x);
		}

		[Test]
		public void RotateLeft_WhenMoreThanTwoNodes()
		{
			var x = new RbtNode<int>(1, null, true);
			var y = new RbtNode<int>(3, x, false);
			var a = new RbtNode<int>(2, y, true);
			y.Left = a;
			x.Right = y;
			_tree.Head = x;
			_tree.RotateToLeft(x);
			_tree.Head.Left.Should().Be(x);
			_tree.Head.Left.Right.Should().Be(a);
			_tree.Head.Parent.Should().Be(RbtNode<int>.Nil);
			_tree.Head.Left.Parent.Should().Be(y);
			_tree.Head.Right.Should().Be(RbtNode<int>.Nil);
			_tree.Head.Left.Right.Parent.Should().Be(x);
		}

		[Test]
		public void RotateRight_WhenTwoNodes()
		{
			var x = new RbtNode<int>(2, null, true);
			var y = new RbtNode<int>(1, x, false);
			x.Left = y;
			_tree.Head = x;
			_tree.RotateToRight(x);
			_tree.Head.Right.Should().Be(x);
			_tree.Head.Parent.Should().Be(RbtNode<int>.Nil);
			_tree.Head.Right.Parent.Should().Be(y);
			_tree.Head.Left.Should().Be(RbtNode<int>.Nil);
			_tree.Head.Right.Left.Parent.Should().Be(null);
		}

		[Test]
		public void ReturnNode_WhenRotateRight_WithOneNode()
		{
			var x = new RbtNode<int>(1, null, true);
			_tree.Head = x;
			_tree.RotateToRight(x);
			_tree.Head.Should().Be(x);
		}

		[Test]
		public void RotateRight_WhenMoreThanTwoNodes()
		{
			var x = new RbtNode<int>(3, null, true);
			var y = new RbtNode<int>(1, x, false);
			var a = new RbtNode<int>(2, y, true);
			y.Right = a;
			x.Left = y;
			_tree.Head = x;
			_tree.RotateToRight(x);
			_tree.Head.Right.Should().Be(x);
			_tree.Head.Right.Left.Should().Be(a);
			_tree.Head.Parent.Should().Be(RbtNode<int>.Nil);
			_tree.Head.Right.Parent.Should().Be(y);
			_tree.Head.Left.Should().Be(RbtNode<int>.Nil);
			_tree.Head.Right.Left.Parent.Should().Be(x);
		}

		[Test]
		public void Insert_WhenEmptyTree()
		{
			_tree.Insert(1);
			_tree.Head.Value.Should().Be(1);
			_tree.Head.Right.Should().Be(RbtNode<int>.Nil);
			_tree.Head.Left.Should().Be(RbtNode<int>.Nil);
		}

		[Test]
		public void InsertRight_WhenOnlyRoot()
		{
			_tree.Insert(2);
			_tree.Insert(2);
			_tree.Head.Right.Value.Should().Be(2);
			_tree.Head.Right.Parent.Should().Be(_tree.Head);
		}

		[Test]
		public void InsertLeft_WhenOnlyRoot()
		{
			_tree.Insert(2);
			_tree.Insert(1);
			_tree.Head.Left.Value.Should().Be(1);
			_tree.Head.Left.Parent.Should().Be(_tree.Head);
		}

		[Test]
		public void InsertRight_WhenSeveralNodes()
		{
			_tree.Insert(2);
			_tree.Insert(3);
			_tree.Insert(2);
			_tree.Head.Right.Right.Value.Should().Be(3);
			_tree.Head.Right.Right.Parent.Should().Be(_tree.Head.Right);
		}

		[Test]
		public void InsertInLeft_WhenAllCases()
		{
			_tree.Head = new RbtNode<int>(11, null, true);
			_tree.Head.Left = new RbtNode<int>(2, _tree.Head, false);
			_tree.Head.Left.Left = new RbtNode<int>(1, _tree.Head.Left, true);
			_tree.Head.Left.Right = new RbtNode<int>(7, _tree.Head.Left, true);
			_tree.Head.Left.Right.Left = new RbtNode<int>(5, _tree.Head.Left.Right, false);
			_tree.Head.Left.Right.Right = new RbtNode<int>(8, _tree.Head.Left.Right, false);
			_tree.Head.Right = new RbtNode<int>(14, _tree.Head, true);
			_tree.Head.Right.Right = new RbtNode<int>(15, _tree.Head.Right, false);
			_tree.Insert(4);
			_tree.Head.Value.Should().Be(7);
		}

		[Test]
		public void InsertInRight_WhenAllCases()
		{
			_tree.Head = new RbtNode<int>(11, null, true);
			_tree.Head.Left = new RbtNode<int>(10, _tree.Head, true);
			_tree.Head.Left.Left = new RbtNode<int>(8, _tree.Head.Left, false);
			_tree.Head.Right = new RbtNode<int>(20, _tree.Head, false);
			_tree.Head.Right.Right = new RbtNode<int>(30, _tree.Head.Right, true);
			_tree.Head.Right.Left = new RbtNode<int>(15, _tree.Head.Right, true);
			_tree.Head.Right.Left.Left = new RbtNode<int>(13, _tree.Head.Right.Left, false);
			_tree.Head.Right.Left.Right = new RbtNode<int>(18, _tree.Head.Right.Left, false);
			_tree.Insert(19);
			_tree.Head.Value.Should().Be(15);
		}

		[Test]
		public void RemoveRoot_WhenOneNode()
		{
			_tree.Insert(5);
			_tree.Remove(5);
			_tree.Contains(5).Should().Be(false);
		}

		[Test]
		public void RemoveRoot_WhenThreeNodes()
		{
			_tree.Insert(5);
			_tree.Insert(4);
			_tree.Insert(6);
			_tree.Remove(5);
			_tree.Contains(5).Should().Be(false);
		}

		[Test]
		public void MainRemoveTest()
		{
			for (var i = 0; i < 20; i++)
				_tree.Insert(i);
			_tree.Remove(15);
		}
	}
}