namespace Structures
{
	internal class AvlNode<T> : Node<T>
	{
		public byte Height { get; set; }

		public AvlNode(T value) : base(value)
		{
			Height = 1;
		}
	}

	public class AvlTree<T> : Tree<T> 
	{
		public override void Insert(T value)
		{
			Head = Insert((AvlNode<T>)Head, value);
			Count++;
		}

		public override void Remove(T item)
		{
			if (!Contains(item)) return;
			Head = Remove((AvlNode<T>)Head, item);
			Count--;
		}

		internal static int GetHeight(AvlNode<T> node)
		{
			return node?.Height ?? 0;
		}

		internal static int GetBalanceFactor(AvlNode<T> node)
		{
			return GetHeight((AvlNode<T>)node?.Right) - GetHeight((AvlNode<T>)node?.Left);
		}

		internal static void FixHeight(AvlNode<T> node)
		{
			var leftHeight = GetHeight((AvlNode<T>)node.Left);
			var rightHeight = GetHeight((AvlNode<T>)node.Right);
			node.Height = (byte)((leftHeight > rightHeight ? leftHeight : rightHeight) + 1);
		}

		internal AvlNode<T> RotateToRight(AvlNode<T> node)
		{
			var tmp = node.Left;
			if (tmp == null || Compare(tmp.Value, node.Value) == 0) return node;
			node.Left = tmp.Right;
			tmp.Right = node;
			FixHeight(node);
			FixHeight((AvlNode<T>)tmp);
			return (AvlNode<T>)tmp;
		}

		internal AvlNode<T> RotateToLeft(AvlNode<T> node)
		{
			var tmp = node.Right;
			if (tmp == null || Compare(tmp.Value, node.Value) == 0) return node;
			node.Right = tmp.Left;
			tmp.Left = node;
			FixHeight(node);
			FixHeight((AvlNode<T>)tmp);
			return (AvlNode<T>)tmp;
		}

		internal AvlNode<T> Balance(AvlNode<T> node)
		{
			FixHeight(node);
			var balanceFactor = GetBalanceFactor(node);
			switch (balanceFactor)
			{
				case 2:
					if (GetBalanceFactor((AvlNode<T>)node.Right) < 0) node.Right = RotateToRight((AvlNode<T>)node.Right);
					return RotateToLeft(node);
				case -2:
					if (GetBalanceFactor((AvlNode<T>)node.Left) > 0) node.Left = RotateToLeft((AvlNode<T>)node.Left);
					return RotateToRight(node);
			}
			return node;
		}

		internal AvlNode<T> Insert(AvlNode<T> node, T value)
		{
			if (node == null) return new AvlNode<T>(value);
			if (Compare(node.Value, value) > 0) node.Left = Insert((AvlNode<T>)node.Left, value);
			else node.Right = Insert((AvlNode<T>)node.Right, value);
			return Balance(node);
		}

		internal AvlNode<T> FindMin(AvlNode<T> node)
		{
			if (node == null) return null;
			while (true)
			{
				if (node?.Left == null) return node;
				node = (AvlNode<T>)node.Left;
			}
		}

		internal AvlNode<T> FindMax(AvlNode<T> node)
		{
			if (node == null) return null;
			while (true)
			{
				if (node?.Right == null) return node;
				node = (AvlNode<T>)node.Right;
			}
		}

		internal AvlNode<T> RemoveMin(AvlNode<T> node)
		{
			if (node.Left == null) return (AvlNode<T>)node.Right;
			node.Left = RemoveMin((AvlNode<T>)node.Left);
			return Balance(node);
		}

		internal AvlNode<T> Remove(AvlNode<T> node, T item)
		{
			if (node == null) return null;
			if (Compare(node.Value, item) > 0) node.Left = Remove((AvlNode<T>)node.Left, item);
			else if (!node.Value.Equals(item)) node.Right = Remove((AvlNode<T>)node.Right, item);
			else 
			{
				var left = (AvlNode<T>)node.Left;
				var right = (AvlNode<T>)node.Right;
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