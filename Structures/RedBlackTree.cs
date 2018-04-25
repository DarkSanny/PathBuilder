using System;

namespace Structures
{
	internal class RbtNode<T> where T : IComparable<T>
	{
		public static RbtNode<T> Nil = new RbtNode<T>() { IsBlack = true };

		public T Value { get; private set; }
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

		public void CopyFrom(RbtNode<T> node)
		{
			Value = node.Value;
			IsBlack = node.IsBlack;
			Left = node.Left;
			Right = node.Right;
			Parent = node.Parent;
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
				node = node.Parent == node.Parent.Parent.Left ? InsertLeftFixup(node) : InsertRightFixUp(node);
			_head.IsBlack = true;
		}

		private RbtNode<T> InsertLeftFixup(RbtNode<T> node)
		{
			var tmp = node.Parent.Parent.Right;
			if (!tmp.IsBlack)
			{
				node.Parent.IsBlack = true;
				tmp.IsBlack = true;
				node.Parent.Parent.IsBlack = false;
				return node.Parent.Parent;
			}
			if (node == node.Parent.Right) RotateToLeft(node = node.Parent);
			node.Parent.IsBlack = true;
			node.Parent.Parent.IsBlack = false;
			RotateToRight(node.Parent.Parent);
			return node;
		}

		private RbtNode<T> InsertRightFixUp(RbtNode<T> node)
		{
			var tmp = node.Parent.Parent.Left;
			if (!tmp.IsBlack)
			{
				node.Parent.IsBlack = true;
				tmp.IsBlack = true;
				node.Parent.Parent.IsBlack = false;
				return node.Parent.Parent;
			}
			if (node == node.Parent.Left) RotateToRight(node = node.Parent);
			node.Parent.IsBlack = true;
			node.Parent.Parent.IsBlack = false;
			RotateToLeft(node.Parent.Parent);
			return node;
		}

		public void RotateToLeft(RbtNode<T> node)
		{
			var tmp = node.Right;
			if (tmp == RbtNode<T>.Nil || tmp.Value.CompareTo(node.Value) == 0) return;
			node.Right = tmp.Left;
			tmp.Left = node;
			if (node.Right != RbtNode<T>.Nil) node.Right.Parent = node;
			FixParent(node, tmp);
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
			FixParent(node, tmp);
			tmp.Parent = node.Parent;
			node.Parent = tmp;
		}

		private void FixParent(RbtNode<T> lastNode, RbtNode<T> newNode)
		{
			if (lastNode.Parent == RbtNode<T>.Nil) _head = newNode;
			else if (lastNode == lastNode.Parent.Left) lastNode.Parent.Left = newNode;
			else lastNode.Parent.Right = newNode;
		}

		private RbtNode<T> GetMinNodeOrThrow(RbtNode<T> node)
		{
			if (node == RbtNode<T>.Nil) throw new Exception("Tree is empty");
			var current = node;
			while (current.Left != RbtNode<T>.Nil)
				current = current.Left;
			return current;
		}

		private RbtNode<T> GetMaxNodeOrThrow(RbtNode<T> node)
		{
			if (node == RbtNode<T>.Nil) throw new Exception("Tree is empty");
			var current = node;
			while (current.Right != RbtNode<T>.Nil)
				current = current.Right;
			return current;
		}

		private RbtNode<T> TreeSuccesor(RbtNode<T> node)
		{
			if (node.Right != RbtNode<T>.Nil) return GetMinNodeOrThrow(node);
			var tmp = node.Parent;
			while (tmp != RbtNode<T>.Nil && node == tmp.Right)
			{
				node = tmp;
				tmp = tmp.Parent;
			}
			return tmp;
		}

		private RbtNode<T> FindNodeOrThrow(T item)
		{
			if (_head == RbtNode<T>.Nil) throw new Exception("Tree is empty");
			var current = _head;
			while (!current.Value.Equals(item) || current != RbtNode<T>.Nil)
				current = current.Value.CompareTo(item) > 0 ? current.Left : current.Right;
			if (current == RbtNode<T>.Nil) throw new ArgumentException("Tree is not contain value");
			return current;
		}

		public void Remove(T item)
		{
			var node = FindNodeOrThrow(item);
			var tmp = node.Left == RbtNode<T>.Nil || node.Right == RbtNode<T>.Nil ? node : TreeSuccesor(node);
			var tmp2 = tmp.Left != RbtNode<T>.Nil ? tmp.Left : tmp.Right;
			tmp2.Parent = tmp.Parent;
			if (tmp.Parent == RbtNode<T>.Nil) _head = tmp2;
			else if (tmp == tmp.Parent.Left) tmp.Parent.Left = tmp2;
			else tmp.Parent.Right = tmp2;
			if (tmp != node) node.CopyFrom(tmp);
			//WTF 
		}
	}
}
