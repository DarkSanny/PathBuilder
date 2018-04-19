using System;

namespace Structures
{
	internal class RbtNode<T> where T : IComparable<T>
	{
		public static RbtNode<T> Nil = new RbtNode<T>() { IsBlack = true };

		public T Value { get; }
		public bool IsBlack { get; set; }
		public RbtNode<T> Left { get; set; }
		public RbtNode<T> Right { get; set; }
		public RbtNode<T> Parent { get; set; }

		private RbtNode()
		{
		}

		public RbtNode(T value, RbtNode<T> parent, bool isBlack)
		{
			Value = value;
			Parent = parent ?? Nil;
			Left = Right = Nil;
			IsBlack = isBlack;
		}
	}

	internal class RedBlackTree <T> where T: IComparable<T>
	{

		public RbtNode<T> _head = RbtNode<T>.Nil;

		public void Insert(T value)
		{
			var previous = RbtNode<T>.Nil;
			var current = _head;
			while (current != RbtNode<T>.Nil)
			{
				previous = current;
				current = current.Value.CompareTo(value) > 0 ? current.Left : current.Right;
			}
			var newNode = new RbtNode<T>(value, previous, false);
			if (previous == RbtNode<T>.Nil) _head = newNode;
			else if (previous.Value.CompareTo(value) > 0) previous.Left = newNode;
			else previous.Right = newNode;
			InsertFixup(newNode);
		}
	
		private void InsertFixup(RbtNode<T> node)
		{
			while (!node.Parent.IsBlack)
			{
				if (node.Parent == node.Parent.Parent.Left)
				{
					var tmp = node.Parent.Parent.Right;
					if (!tmp.IsBlack)
					{
						node.Parent.IsBlack = true;
						tmp.IsBlack = true;
						node.Parent.Parent.IsBlack = false;
						node = node.Parent.Parent;
					}
					else
					{
						if (node == node.Parent.Right) RotateToLeft(node = node.Parent);
						node.Parent.IsBlack = true;
						node.Parent.Parent.IsBlack = false;
						RotateToRight(node.Parent.Parent);
					}
				}
				else
				{
					var tmp = node.Parent.Parent.Left;
					if (!tmp.IsBlack)
					{
						node.Parent.IsBlack = true;
						tmp.IsBlack = true;
						node.Parent.Parent.IsBlack = false;
						node = node.Parent.Parent;
					}
					else
					{
						if (node == node.Parent.Left) RotateToRight(node = node.Parent);
						node.Parent.IsBlack = true;
						node.Parent.Parent.IsBlack = false;
						RotateToLeft(node.Parent.Parent);
					}
				}
			}
			_head.IsBlack = true;
		}

		public void RotateToLeft(RbtNode<T> node)
		{
			var tmp = node.Right;
			if (tmp == RbtNode<T>.Nil || tmp.Value.Equals(node.Value)) return;
			node.Right = tmp.Left;
			tmp.Left = node;
			if (node.Right != RbtNode<T>.Nil) node.Right.Parent = node;
			if (node.Parent == RbtNode<T>.Nil) _head = tmp;
			else if (node == node.Parent.Left) node.Parent.Left = tmp;
			else node.Parent.Right = tmp;
			tmp.Parent = node.Parent;
			node.Parent = tmp;
		}

		public void RotateToRight(RbtNode<T> node)
		{
			var tmp = node.Left;
			if (tmp == RbtNode<T>.Nil) return;
			node.Left = tmp.Right;
			tmp.Right = node;
			if (node.Left != RbtNode<T>.Nil) node.Left.Parent = node;
			if (node.Parent == RbtNode<T>.Nil) _head = tmp;
			else if (node == node.Parent.Left) node.Parent.Left = tmp;
			else node.Parent.Right = tmp;
			tmp.Parent = node.Parent;
			node.Parent = tmp;
		}	
	}
}
