using System;
using System.Collections.Generic;
using System.Linq;

namespace Structures
{
	public class BinaryHeap<T> where T : IComparable<T>
	{
		private List<T> _list;

		public int Count => _list.Count;

		public BinaryHeap()
		{
			_list = new List<T>();
		}

		public void Add(T value)
		{
			_list.Add(value);
			var index = Count - 1;
			var parent = (index - 1) / 2;
			while (index > 0 && _list[parent].CompareTo(_list[index]) < 0)
			{
				SwapItemsInList(index, parent);
				index = parent;
				parent = (index - 1) / 2;
			}
		}

		public void BuildHeap(IEnumerable<T> array)
		{
			_list = array.ToList();
			for (var i = Count / 2; i >= 0; i--)
				Heapify(i);
		}

		public T GetAndRemoveMax()
		{
			if (Count <= 0) throw new Exception("Heap is empty");
			var result = _list[0];
			_list[0] = _list[Count - 1];
			_list.RemoveAt(Count - 1);
			Heapify(0);
			return result;
		}

		private void Heapify(int index)
		{
			while (true)
			{
				var leftChild = 2 * index + 1;
				var rightChild = 2 * index + 2;
				var parent = index;
				if (leftChild < Count && _list[leftChild].CompareTo(_list[parent]) > 0)
					parent = leftChild;
				if (rightChild < Count && _list[rightChild].CompareTo(_list[parent]) > 0)
					parent = rightChild;
				if (parent.Equals(index)) break;
				SwapItemsInList(index, parent);
				index = parent;
			}
		}

		private void SwapItemsInList(int firstIndex, int secondIndex)
		{
			var tmp = _list[firstIndex];
			_list[firstIndex] = _list[secondIndex];
			_list[secondIndex] = tmp;
		}

		public override string ToString()
		{
			return string.Join(" ", _list);
		}
	}
}