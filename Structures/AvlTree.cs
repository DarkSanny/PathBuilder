using System;

namespace Structures
{
	internal class AvlNode<T>
	{
		public T Value { get; set; }
		public int Height { get; set; }
		public AvlNode<T> Left { get; set; }
		public AvlNode<T> Right { get; set; }

		public AvlNode(T value)
		{
			Value = value;
			Height = 1;
		}
	}

	public class AvlTree<T> : Tree<T> 
	{
		internal AvlNode<T> Head;
		public int Count { get; private set; }

		public override void Insert(T value)
		{
			Head = Insert(Head, value);
			Count++;
		}

		public override void Remove(T item)
		{
			var count = Count;
			Head = Remove(Head, item, ref count);
			Count = count;
		}

		public override T GetMinOrThrow()
		{
			var min = FindMin(Head);
			if (min == null) throw new Exception("Tree is empty");
			return min.Value;
		}

		public override T GetMaxOrThrow()
		{
			var max = FindMax(Head);
			if (max == null) throw new Exception("Tree is empty");
			return max.Value;
		}

		internal static int GetHeight(AvlNode<T> node)
		{
			return node?.Height ?? 0;
		}

		internal static int GetBalanceFactor(AvlNode<T> node)
		{
			return GetHeight(node?.Right) - GetHeight(node?.Left);
		}

		internal static void FixHeight(AvlNode<T> node)
		{
			var leftHeight = GetHeight(node.Left);
			var rightHeight = GetHeight(node.Right);
			node.Height = (leftHeight > rightHeight ? leftHeight : rightHeight) + 1;
		}

		internal AvlNode<T> RotateToRight(AvlNode<T> node)
		{
			var tmp = node.Left;
			if (tmp == null || Compare(tmp.Value, node.Value) == 0) return node;
			node.Left = tmp.Right;
			tmp.Right = node;
			FixHeight(node);
			FixHeight(tmp);
			return tmp;
		}

		internal AvlNode<T> RotateToLeft(AvlNode<T> node)
		{
			var tmp = node.Right;
			if (tmp == null || Compare(tmp.Value, node.Value) == 0) return node;
			node.Right = tmp.Left;
			tmp.Left = node;
			FixHeight(node);
			FixHeight(tmp);
			return tmp;
		}

		internal AvlNode<T> Balance(AvlNode<T> node)
		{
			FixHeight(node);
			var balanceFactor = GetBalanceFactor(node);
			switch (balanceFactor)
			{
				case 2:
					if (GetBalanceFactor(node.Right) < 0) node.Right = RotateToRight(node.Right);
					return RotateToLeft(node);
				case -2:
					if (GetBalanceFactor(node.Left) > 0) node.Left = RotateToLeft(node.Left);
					return RotateToRight(node);
			}
			return node;
		}

		internal AvlNode<T> Insert(AvlNode<T> node, T value)
		{
			if (node == null) return new AvlNode<T>(value);
			if (Compare(node.Value, value) > 0) node.Left = Insert(node.Left, value);
			else node.Right = Insert(node.Right, value);
			return Balance(node);
		}

		internal AvlNode<T> FindMin(AvlNode<T> node)
		{
			if (node == null) return null;
			while (true)
			{
				if (node?.Left == null) return node;
				node = node.Left;
			}
		}

		internal AvlNode<T> FindMax(AvlNode<T> node)
		{
			if (node == null) return null;
			while (true)
			{
				if (node?.Right == null) return node;
				node = node.Right;
			}
		}

		internal AvlNode<T> RemoveMin(AvlNode<T> node)
		{
			if (node.Left == null) return node.Right;
			node.Left = RemoveMin(node.Left);
			return Balance(node);
		}

		internal AvlNode<T> Remove(AvlNode<T> node, T item, ref int count)
		{
			if (node == null) return null;
			if (Compare(node.Value, item) > 0) node.Left = Remove(node.Left, item, ref count);
			else if (!node.Value.Equals(item)) node.Right = Remove(node.Right, item, ref count);
			else 
			{
				count--;
				var left = node.Left;
				var right = node.Right;
				if (right == null) return left;
				var min = FindMin(right);
				min.Right = RemoveMin(right);
				min.Left = left;
				return Balance(min);
			}
			return Balance(node);
		}
	}
}