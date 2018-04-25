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
		//Мне лень делать тесты через рефлексию, поэтому чтобы тесты заработали нужно сделать публичным поле _head

		[Test]
		public void Rotateleft_WhenTwoNodes()
		{
			var x = new RbtNode<int>(1, null, true);
			var y = new RbtNode<int>(2, x, false);
			x.Right = y;
			_tree._head = x;
			_tree.RotateToLeft(x);
			_tree._head.Left.Should().Be(x);
			_tree._head.Parent.Should().Be(RbtNode<int>.Nil);
			_tree._head.Left.Parent.Should().Be(y);
			_tree._head.Right.Should().Be(RbtNode<int>.Nil);
			_tree._head.Left.Right.Parent.Should().Be(null);
		}

		[Test]
		public void ReturnNode_WhenRotateLeft_WithOneNode()
		{
			var x = new RbtNode<int>(1, null, true);
			_tree._head = x;
			_tree.RotateToLeft(x);
			_tree._head.Should().Be(x);
		}

		[Test]
		public void RotateLeft_WhenMoreThanTwoNodes()
		{
			var x = new RbtNode<int>(1, null, true);
			var y = new RbtNode<int>(3, x, false);
			var a = new RbtNode<int>(2, y, true);
			y.Left = a;
			x.Right = y;
			_tree._head = x;
			_tree.RotateToLeft(x);
			_tree._head.Left.Should().Be(x);
			_tree._head.Left.Right.Should().Be(a);
			_tree._head.Parent.Should().Be(RbtNode<int>.Nil);
			_tree._head.Left.Parent.Should().Be(y);
			_tree._head.Right.Should().Be(RbtNode<int>.Nil);
			_tree._head.Left.Right.Parent.Should().Be(x);
		}

		[Test]
		public void RotateRight_WhenTwoNodes()
		{
			var x = new RbtNode<int>(2, null, true);
			var y = new RbtNode<int>(1, x, false);
			x.Left = y;
			_tree._head = x;
			_tree.RotateToRight(x);
			_tree._head.Right.Should().Be(x);
			_tree._head.Parent.Should().Be(RbtNode<int>.Nil);
			_tree._head.Right.Parent.Should().Be(y);
			_tree._head.Left.Should().Be(RbtNode<int>.Nil);
			_tree._head.Right.Left.Parent.Should().Be(null);
		}

		[Test]
		public void ReturnNode_WhenRotateRight_WithOneNode()
		{
			var x = new RbtNode<int>(1, null, true);
			_tree._head = x;
			_tree.RotateToRight(x);
			_tree._head.Should().Be(x);
		}

		[Test]
		public void RotateRight_WhenMoreThanTwoNodes()
		{
			var x = new RbtNode<int>(3, null, true);
			var y = new RbtNode<int>(1, x, false);
			var a = new RbtNode<int>(2, y, true);
			y.Right = a;
			x.Left = y;
			_tree._head = x;
			_tree.RotateToRight(x);
			_tree._head.Right.Should().Be(x);
			_tree._head.Right.Left.Should().Be(a);
			_tree._head.Parent.Should().Be(RbtNode<int>.Nil);
			_tree._head.Right.Parent.Should().Be(y);
			_tree._head.Left.Should().Be(RbtNode<int>.Nil);
			_tree._head.Right.Left.Parent.Should().Be(x);
		}

		[Test]
		public void Insert_WhenEmptyTree()
		{
			_tree.Insert(1);
			_tree._head.Value.Should().Be(1);
			_tree._head.Right.Should().Be(RbtNode<int>.Nil);
			_tree._head.Left.Should().Be(RbtNode<int>.Nil);
		}

		[Test]
		public void InsertRight_WhenOnlyRoot()
		{
			_tree.Insert(2);
			_tree.Insert(2);
			_tree._head.Right.Value.Should().Be(2);
			_tree._head.Right.Parent.Should().Be(_tree._head);
		}

		[Test]
		public void InsertLeft_WhenOnlyRoot()
		{
			_tree.Insert(2);
			_tree.Insert(1);
			_tree._head.Left.Value.Should().Be(1);
			_tree._head.Left.Parent.Should().Be(_tree._head);
		}

		[Test]
		public void InsertRight_WhenSeveralNodes()
		{
			_tree.Insert(2);
			_tree.Insert(3);
			_tree.Insert(2);
			_tree._head.Right.Right.Value.Should().Be(3);
			_tree._head.Right.Right.Parent.Should().Be(_tree._head.Right);
		}

		[Test]
		public void InsertInLeft_WhenAllCases()
		{
			_tree._head = new RbtNode<int>(11, null, true);
			_tree._head.Left = new RbtNode<int>(2, _tree._head, false);
			_tree._head.Left.Left = new RbtNode<int>(1, _tree._head.Left, true);
			_tree._head.Left.Right = new RbtNode<int>(7, _tree._head.Left, true);
			_tree._head.Left.Right.Left = new RbtNode<int>(5, _tree._head.Left.Right, false);
			_tree._head.Left.Right.Right = new RbtNode<int>(8, _tree._head.Left.Right, false);
			_tree._head.Right = new RbtNode<int>(14, _tree._head, true);
			_tree._head.Right.Right = new RbtNode<int>(15, _tree._head.Right, false);
			_tree.Insert(4);
			_tree._head.Value.Should().Be(7);
		}

		[Test]
		public void InsertInRight_WhenAllCases()
		{
			_tree._head = new RbtNode<int>(11, null, true);
			_tree._head.Left = new RbtNode<int>(10, _tree._head, true);
			_tree._head.Left.Left = new RbtNode<int>(8, _tree._head.Left, false);
			_tree._head.Right = new RbtNode<int>(20, _tree._head, false);
			_tree._head.Right.Right = new RbtNode<int>(30, _tree._head.Right, true);
			_tree._head.Right.Left = new RbtNode<int>(15, _tree._head.Right, true);
			_tree._head.Right.Left.Left = new RbtNode<int>(13, _tree._head.Right.Left, false);
			_tree._head.Right.Left.Right = new RbtNode<int>(18, _tree._head.Right.Left, false);
			_tree.Insert(19);
			_tree._head.Value.Should().Be(15);
		}
	}
}