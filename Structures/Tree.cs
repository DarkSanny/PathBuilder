using System;
using System.Collections;
using System.Collections.Generic;

namespace Structures
{
	internal class Node<T>
	{
		public T Value { get; set; }
		public Node<T> Left { get; set; }
		public Node<T> Right { get; set; }

		public Node(T value)
		{
			Value = value;
		}
	}

    public abstract class Tree <T> : IEnumerable<T>
	{
	    internal Node<T> Head;

	    protected Func<T, T, int> Compare;

		public int Count { get; protected set; }

	    protected Tree()
	    {
		    if (default(T) is IComparable<T>) Compare = (f, s) => ((IComparable<T>) f).CompareTo(s);
		    else Compare = (f, s) => f.GetHashCode().CompareTo(s.GetHashCode());
	    }

		public abstract void Insert(T item);
	    public abstract void Remove(T item);
	    public abstract T GetMinOrThrow();
	    public abstract T GetMaxOrThrow();

	    internal virtual Node<T> Find(T value)
	    {
		    return Find(Head, value);
	    }

	    internal virtual Node<T> Find(Node<T> node, T value)
	    {
		    if (node == null) return null;
		    while (node != null)
		    {
				if (node.Value.Equals(value)) return node;
				 node = Compare(node.Value, value) > 0 ? node.Left : node.Right;
			}
		    return null;
		}

	    public virtual bool Contains(T value)
	    {
		    var node = Find(value);
			return node != null;
	    }

		public virtual IEnumerator<T> GetEnumerator()
		{
			if (Head == null) yield break;
			foreach (var e in EnumerateNode(Head))
				yield return e;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private static IEnumerable<T> EnumerateNode(Node<T> currentNode)
		{
			if (currentNode == null)
				yield break;
			foreach (var e in EnumerateNode(currentNode.Left))
				yield return e;
			yield return currentNode.Value;
			foreach (var e in EnumerateNode(currentNode.Right))
				yield return e;
		}
	}
}
