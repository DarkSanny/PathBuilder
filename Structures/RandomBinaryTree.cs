using System;

namespace Structures
{
	internal class RbstNode<T> : Node<T>
	{
		public int Size { get; set; }

		public RbstNode(T value) : base(value)
		{
			Size = 1;
		}
	}

	public class RandomBinaryTree <T> : Tree<T>
	{	
		internal Random Random = new Random();

		internal static int GetSize(RbstNode<T> rbstNode) => rbstNode?.Size ?? 0;

		internal static void FixSize(RbstNode<T> rbstNode)
		{
			rbstNode.Size = GetSize((RbstNode<T>)rbstNode.Left) + GetSize((RbstNode<T>)rbstNode.Right) + 1;
		}

		public override void Insert(T item)
		{
			Head = RandomInsert((RbstNode<T>)Head, item);
			Count = GetSize((RbstNode<T>) Head);
		}

		public override void Remove(T item)
		{
			Head = Remove((RbstNode<T>)Head, item);
			Count = GetSize((RbstNode<T>)Head);
		}

		public override T GetMinOrThrow()
		{
			if (Head == null) throw new Exception("Tree is empty");
			var currentNode = Head;
			while (currentNode.Left != null)
				currentNode = currentNode.Left;
			return currentNode.Value;
		}

		public override T GetMaxOrThrow()
		{
			if (Head == null) throw new Exception("Tree is empty");
			var currentNode = Head;
			while (currentNode.Right != null)
				currentNode = currentNode.Right;
			return currentNode.Value;
		}

		internal RbstNode<T> RotateRight(RbstNode<T> rbstNode)
		{
			var tmp = (RbstNode<T>)rbstNode.Left;
			if (tmp == null || Compare(tmp.Value, rbstNode.Value) == 0) return rbstNode;
			rbstNode.Left = tmp.Right;
			tmp.Right = rbstNode;
			tmp.Size = rbstNode.Size;
			FixSize(rbstNode);
			FixSize(tmp);
			return tmp;
		}

		internal RbstNode<T> RotateLeft(RbstNode<T> rbstNode)
		{
			var tmp = (RbstNode<T>)rbstNode.Right;
			if (tmp == null || 	Compare(tmp.Value, rbstNode.Value) == 0) return rbstNode;
			rbstNode.Right = tmp.Left;
			tmp.Left = rbstNode;
			FixSize(rbstNode);
			FixSize(tmp);
			return tmp;
		}

		internal RbstNode<T> InsertRoot(RbstNode<T> rbstNode, T item) 
		{
			if (rbstNode == null) return new RbstNode<T>(item);
			if (Compare(rbstNode.Value, item) > 0)
			{
				rbstNode.Left = InsertRoot((RbstNode<T>)rbstNode.Left, item);
				return RotateRight(rbstNode);
			}
				rbstNode.Right = InsertRoot((RbstNode<T>)rbstNode.Right, item);
				return RotateLeft(rbstNode);
		}

		internal RbstNode<T> RandomInsert(RbstNode<T> rbstNode, T item) 
		{
			if (rbstNode == null) return new RbstNode<T>(item);
			if (Random.Next(rbstNode.Size + 1) == 0)
				return InsertRoot(rbstNode, item);
			if (Compare(rbstNode.Value, item) > 0)
				rbstNode.Left = RandomInsert((RbstNode<T>)rbstNode.Left, item);
			else
				rbstNode.Right = RandomInsert((RbstNode<T>)rbstNode.Right, item);
			FixSize(rbstNode);
			return rbstNode;
		}

		internal RbstNode<T> Join(RbstNode<T> first, RbstNode<T> second) 
		{
			if (first == null) return second;
			if (second == null) return first;
			if (Random.Next(first.Size + second.Size) < first.Size)
			{
				first.Right = Join((RbstNode<T>)first.Right, second);
				FixSize(first);
				return first;
			}
				second.Left = Join(first, (RbstNode<T>)second.Left);
				FixSize(second);
				return second;
		}

		internal RbstNode<T> Remove(RbstNode<T> rbstNode, T item)
		{
			if (rbstNode == null) return null;
			if (rbstNode.Value.Equals(item))
			{
				return Join((RbstNode<T>)rbstNode.Left, (RbstNode<T>)rbstNode.Right);
			}
			if (Compare(rbstNode.Value, item) > 0)
				rbstNode.Left = Remove((RbstNode<T>)rbstNode.Left, item);
			else
				rbstNode.Right = Remove((RbstNode<T>)rbstNode.Right, item);
			FixSize(rbstNode);
			return rbstNode;
		}

	}
}