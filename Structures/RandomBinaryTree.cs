using System;

namespace Structures
{
	public class RandomBinaryTree<T> where T : IComparable<T>
	{
		private class Node<TNode> where TNode : IComparable<TNode>
		{
			public TNode Value { get; }
			public int Size { get; set; }
			public Node<TNode> Left { get; set; }
			public Node<TNode> Right { get; set; }

			public Node(TNode item)
			{
				Value = item;
				Size = 1;
			}
		}

		private static Random Random = new Random();
		private Node<T> _head;

		public int Count => GetSize(_head);

		private static int GetSize(Node<T> node) => node?.Size ?? 0;

		private static void FixSize(Node<T> node)
		{
			node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;
		}

		public void Add(T item)
		{
			_head = RandomInsert(_head, item);
		}

		public void Remove(T item)
		{
			_head = Remove(_head, item);
		}

		public T MinOrThrow()
		{
			if (_head == null) throw new Exception("Tree is empty");
			var currentNode = _head;
			while (currentNode.Left != null)
				currentNode = currentNode.Left;
			return currentNode.Value;
		}

		private static Node<T> RotateRight(Node<T> node)
		{
			var tmp = node.Left;
			if (tmp == null || tmp.Value.CompareTo(node.Value) == 0) return node;
			node.Left = tmp.Right;
			tmp.Right = node;
			tmp.Size = node.Size;
			FixSize(node);
			FixSize(tmp);
			return tmp;
		}

		private static Node<T> RotateLeft(Node<T> node)
		{
			var tmp = node.Right;
			if (tmp == null || tmp.Value.CompareTo(node.Value) == 0) return node;
			node.Right = tmp.Left;
			tmp.Left = node;
			FixSize(node);
			FixSize(tmp);
			return tmp;
		}

		private static Node<T> InsertRoot(Node<T> node, T item) 
		{
			if (node == null) return new Node<T>(item);
			if (node.Value.CompareTo(item) > 0)
			{
				node.Left = InsertRoot(node.Left, item);
				return RotateRight(node);
			}
				node.Right = InsertRoot(node.Right, item);
				return RotateLeft(node);
		}

		private static Node<T> RandomInsert(Node<T> node, T item) 
		{
			if (node == null) return new Node<T>(item);
			if (Random.Next(node.Size + 1) == 0)
				return InsertRoot(node, item);
			if (node.Value.CompareTo(item) > 0)
				node.Left = RandomInsert(node.Left, item);
			else
				node.Right = RandomInsert(node.Right, item);
			FixSize(node);
			return node;
		}

		private static Node<T> Insert(Node<T> node, T item)
		{
			if (node == null) return new Node<T>(item);
			if (node.Value.CompareTo(item) > 0)
				node.Left = Insert(node.Left, item);
			else
				node.Right = Insert(node.Right, item);
			FixSize(node);
			return node;
		}

		private static Node<T> Join(Node<T> first, Node<T> second) 
		{
			if (first == null) return second;
			if (second == null) return first;
			if (Random.Next(first.Size + second.Size) < first.Size)
			{
				first.Right = Join(first.Right, second);
				FixSize(first);
				return first;
			}
				second.Left = Join(first, second.Left);
				FixSize(second);
				return second;
		}

		private static Node<T> Remove(Node<T> node, T item)
		{
			if (node == null) return null;
			if (node.Value.Equals(item))
			{
				return Join(node.Left, node.Right);
			}
			if (node.Value.CompareTo(item) > 0)
				node.Left = Remove(node.Left, item);
			else
				node.Right = Remove(node.Right, item);
			FixSize(node);
			return node;
		}

	}
}