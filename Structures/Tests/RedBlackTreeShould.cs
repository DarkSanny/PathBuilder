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


		[Test]
		public void Rotateleft_WhenTwoNodes()
		{
			var x = new RbtNode<int>(1, null, true);
			var y = new RbtNode<int>(2, x, false);
			x.Right = y;
			var result = RedBlackTree<int>.RotateToLeft(x);
			result.Left.Should().Be(x);
			result.Parent.Should().Be(RbtNode<int>.Nil);
			result.Left.Parent.Should().Be(y);
			result.Right.Should().Be(RbtNode<int>.Nil);
			result.Left.Right.Parent.Should().Be(null);
		}

		[Test]
		public void ReturnNode_WhenOneNode()
		{
			var x = new RbtNode<int>(1, null, true);
			var result = RedBlackTree<int>.RotateToLeft(x);
			result.Should().Be(x);
		}

		[Test]
		public void RotateLeft_WhenMoreThanTwoNodes()
		{
			var x = new RbtNode<int>(1, null, true);
			var y = new RbtNode<int>(3, x, false);
			var a = new RbtNode<int>(2, y, true);
			y.Left = a;
			x.Right = y;
			var result = RedBlackTree<int>.RotateToLeft(x);
			result.Left.Should().Be(x);
			result.Left.Right.Should().Be(a);
			result.Parent.Should().Be(RbtNode<int>.Nil);
			result.Left.Parent.Should().Be(y);
			result.Right.Should().Be(RbtNode<int>.Nil);
			result.Left.Right.Parent.Should().Be(x);
		}

		[Test]
		public void RotateRight_WhenTwoNodes()
		{
			var x = new RbtNode<int>(2, null, true);
			var y = new RbtNode<int>(1, x, false);
			x.Left = y;
			var result = RedBlackTree<int>.RotateToRight(x);
			result.Right.Should().Be(x);
			result.Parent.Should().Be(RbtNode<int>.Nil);
			result.Right.Parent.Should().Be(y);
			result.Left.Should().Be(RbtNode<int>.Nil);
			result.Right.Left.Parent.Should().Be(null);
		}

		[Test]
		public void ReturnNode_WhenOeNode()
		{
			var x = new RbtNode<int>(1, null, true);
			var result = RedBlackTree<int>.RotateToRight(x);
			result.Should().Be(x);
		}

		[Test]
		public void RotateRight_WhenMoreThanTwoNodes()
		{
			var x = new RbtNode<int>(3, null, true);
			var y = new RbtNode<int>(1, x, false);
			var a = new RbtNode<int>(2, y, true);
			y.Right = a;
			x.Left = y;
			var result = RedBlackTree<int>.RotateToRight(x);
			result.Right.Should().Be(x);
			result.Right.Left.Should().Be(a);
			result.Parent.Should().Be(RbtNode<int>.Nil);
			result.Right.Parent.Should().Be(y);
			result.Left.Should().Be(RbtNode<int>.Nil);
			result.Right.Left.Parent.Should().Be(x);
		}

		//Тесты, которые будут не доступны после релиза
		//Мне лень делать тесты через рефлексию, поэтому чтобы тесты заработали нужно сделать публичным поле _head

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
			_tree._head.Right.Left.Value.Should().Be(2);
			_tree._head.Right.Left.Parent.Should().Be(_tree._head.Right);
		}
	}
}