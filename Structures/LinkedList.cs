using System;
using System.Collections;
using System.Collections.Generic;

namespace Structures
{
	internal class ListNode<T>
	{
		public T Value { get; set; }
		public ListNode<T> Next { get; set; }
	}

	public class LinkedList	<T>	: IEnumerable<T>
	{
		private ListNode<T> _head;
		private ListNode<T> _tail;
		public int Count { get; private set; }

		public void AddEnd(T item)
		{
			if (_tail == null)
			{
				_tail = new ListNode<T>() { Value = item };
				_head = _tail;
			}
			else
			{
				_tail.Next = new ListNode<T> { Value = item };
				_tail = _tail.Next;
			}
			Count++;
		}

		public void AddStart(T item)
		{
			if (_head == null)
			{
				_head = new ListNode<T>() { Value = item };
				_tail = _head;
			}
			else
			{
				_head = new ListNode<T>() { Value = item, Next = _head };
			}
			Count++;
		}

		public T RemoveAt(int index)
		{
			if (Count <= index) throw new IndexOutOfRangeException();
			var result = _head.Value;
			if (index == 0) _head = _head.Next;
			else
			{
				var previousNode = Find(index - 1);
				if (previousNode.Next == null) throw new IndexOutOfRangeException();
				result = previousNode.Next.Value;
				previousNode.Next = previousNode.Next.Next;
			}
			Count--;
			return result;
		}

		public void Revert()
		{
			ListNode<T> previousNode = null;
			var currectNode = _head;
			while (currectNode != null)
			{
				var nextNode = currectNode.Next;
				currectNode.Next = previousNode;
				previousNode = currectNode;
				currectNode = nextNode;
			}
			var tmp = _head;
			_head = _tail;
			_tail = tmp;
		}

		private ListNode<T> Find(int index)
		{
			if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
			if (index == Count - 1) return _tail;
			var i = 0;
			var currentNode = _head;
			while (currentNode != null)
			{
				if (i == index) return currentNode;
				i++;
				currentNode = currentNode.Next;
			}
			throw new IndexOutOfRangeException();
		}

		public IEnumerator<T> GetEnumerator()
		{
			var currentNode = _head;
			while (currentNode != null)
			{
				yield return currentNode.Value;
				currentNode = currentNode.Next;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public T this[int index]
		{
			get => Find(index).Value;
			set => Find(index).Value = value;
		}
	}
}
