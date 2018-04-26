using System;

namespace Structures
{
	internal class RbstNode<TNode> where TNode : IComparable<TNode>
	{
		public TNode Value { get; }
		public int Size { get; set; }
		public RbstNode<TNode> Left { get; set; }
		public RbstNode<TNode> Right { get; set; }

		public RbstNode(TNode item)
		{
			Value = item;
			Size = 1;
		}
	}

	public class RandomBinaryTree <T> : Tree<T> where T : IComparable<T> 
	{	

		private static Random _random = new Random();
		private RbstNode<T> _head;

		public int Count => GetSize(_head);

		private static int GetSize(RbstNode<T> rbstNode) => rbstNode?.Size ?? 0;

		private static void FixSize(RbstNode<T> rbstNode)
		{
			rbstNode.Size = GetSize(rbstNode.Left) + GetSize(rbstNode.Right) + 1;
		}

		public override void Insert(T item)
		{
			_head = RandomInsert(_head, item);
		}

		public override void Remove(T item)
		{
			_head = Remove(_head, item);
		}

		public override T GetMinOrThrow()
		{
			if (_head == null) throw new Exception("Tree is empty");
			var currentNode = _head;
			while (currentNode.Left != null)
				currentNode = currentNode.Left;
			return currentNode.Value;
		}

		private static RbstNode<T> RotateRight(RbstNode<T> rbstNode)
		{
			var tmp = rbstNode.Left;
			if (tmp == null || tmp.Value.CompareTo(rbstNode.Value) == 0) return rbstNode;
			rbstNode.Left = tmp.Right;
			tmp.Right = rbstNode;
			tmp.Size = rbstNode.Size;
			FixSize(rbstNode);
			FixSize(tmp);
			return tmp;
		}

		private static RbstNode<T> RotateLeft(RbstNode<T> rbstNode)
		{
			var tmp = rbstNode.Right;
			if (tmp == null || tmp.Value.CompareTo(rbstNode.Value) == 0) return rbstNode;
			rbstNode.Right = tmp.Left;
			tmp.Left = rbstNode;
			FixSize(rbstNode);
			FixSize(tmp);
			return tmp;
		}

		private static RbstNode<T> InsertRoot(RbstNode<T> rbstNode, T item) 
		{
			if (rbstNode == null) return new RbstNode<T>(item);
			if (rbstNode.Value.CompareTo(item) > 0)
			{
				rbstNode.Left = InsertRoot(rbstNode.Left, item);
				return RotateRight(rbstNode);
			}
				rbstNode.Right = InsertRoot(rbstNode.Right, item);
				return RotateLeft(rbstNode);
		}

		private static RbstNode<T> RandomInsert(RbstNode<T> rbstNode, T item) 
		{
			if (rbstNode == null) return new RbstNode<T>(item);
			if (_random.Next(rbstNode.Size + 1) == 0)
				return InsertRoot(rbstNode, item);
			if (rbstNode.Value.CompareTo(item) > 0)
				rbstNode.Left = RandomInsert(rbstNode.Left, item);
			else
				rbstNode.Right = RandomInsert(rbstNode.Right, item);
			FixSize(rbstNode);
			return rbstNode;
		}

		private static RbstNode<T> Join(RbstNode<T> first, RbstNode<T> second) 
		{
			if (first == null) return second;
			if (second == null) return first;
			if (_random.Next(first.Size + second.Size) < first.Size)
			{
				first.Right = Join(first.Right, second);
				FixSize(first);
				return first;
			}
				second.Left = Join(first, second.Left);
				FixSize(second);
				return second;
		}

		private static RbstNode<T> Remove(RbstNode<T> rbstNode, T item)
		{
			if (rbstNode == null) return null;
			if (rbstNode.Value.Equals(item))
			{
				return Join(rbstNode.Left, rbstNode.Right);
			}
			if (rbstNode.Value.CompareTo(item) > 0)
				rbstNode.Left = Remove(rbstNode.Left, item);
			else
				rbstNode.Right = Remove(rbstNode.Right, item);
			FixSize(rbstNode);
			return rbstNode;
		}

	}
}