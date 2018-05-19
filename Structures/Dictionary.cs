using System;
using System.Collections.Generic;
using System.Text;

namespace Structures
{
	internal class DictNode<TKey, TValue> 
	{
		public int Index { get; set; }
		public TKey Key { get; set; }
		public TValue Value { get; set; }
		public DictNode<TKey, TValue> Next { get; set; }

		public void Update(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		public override string ToString()
		{
			return $"{Key}:{Value}";
		}
	}

	public class Dictionary	<TKey, TValue>
	{
		private DictNode<TKey, TValue>[] _table;
		private DictNode<TKey, TValue>[] _buckets;
		private DictNode<TKey, TValue> _empty;

		public Dictionary()
		{
			_table = new DictNode<TKey, TValue>[19];
			_buckets = new DictNode<TKey, TValue>[39];
			_buckets[_buckets.Length - 1] = new DictNode<TKey, TValue>() {Index = _buckets.Length - 1 };
			for (var i = _buckets.Length - 2; i >= 0; i--)
				_buckets[i] = new DictNode<TKey, TValue>(){Next = _buckets[i+1], Index = i};
			_empty = _buckets[0];
		}

		private static int CalculateIndex(IReadOnlyCollection<DictNode<TKey, TValue>> table, TKey key)
		{
			return Math.Abs(key.GetHashCode() % table.Count);
		}

		public void Add(TKey key, TValue value)
		{
			AddInTable(_table, _buckets, ref _empty, key, value);
			if (_empty.Index > _buckets.Length / 4 * 3) Enlarge();
		}

		public void Remove(TKey key)
		{
			var index = CalculateIndex(_table, key);
			var currentNode = _buckets[_table[index]?.Index ?? _empty.Index];
			if (currentNode == _empty) return;
			if (!currentNode.Key.Equals(key))
			{
				while (currentNode.Next != null)
				{
					if (currentNode.Next.Key.Equals(key)) break;
					currentNode = currentNode.Next;
				}
				if (currentNode.Next == null) return;
				var tmp = currentNode;
				currentNode = currentNode.Next;
				tmp.Next = currentNode.Next;
			}
			if (_table[index] == currentNode) _table[index] = currentNode.Next;
			currentNode.Next = _empty;
			_empty = currentNode;
		}

		private static void AddInTable(
			DictNode<TKey, TValue>[] table,
			DictNode<TKey, TValue>[] buckets,
			ref DictNode<TKey, TValue> empty,
			TKey key,
			TValue value)
		{
			var index = CalculateIndex(table, key);
			var currentNode = buckets[table[index]?.Index ?? empty.Index];
			if (currentNode != empty)
			{
				while (currentNode.Next != null)
				{
					if (currentNode.Key.Equals(key)) throw new ArgumentException("Key already exist");
					currentNode = currentNode.Next;
				}
				currentNode.Next = empty;
				currentNode = currentNode.Next;
			}
			empty = empty.Next;
			currentNode.Update(key, value);
			currentNode.Next = null;
			if (table[index] == null) table[index] = currentNode;
		}

		private void Enlarge()
		{
			var table = new DictNode<TKey, TValue>[_buckets.Length];
			var buckets = new DictNode<TKey, TValue>[_buckets.Length * 2 + 1];
			buckets[buckets.Length - 1] = new DictNode<TKey, TValue>() { Index = buckets.Length - 1 };
			for (var i = buckets.Length - 2; i >= 0; i--)
				buckets[i] = new DictNode<TKey, TValue>() { Next = buckets[i + 1], Index = i };
			var empty = buckets[0];
			foreach (var dictNode in _table)
			{
				var currentNode = dictNode;
				while (currentNode != null)
				{
					AddInTable(table, buckets, ref empty, currentNode.Key, currentNode.Value);
					currentNode = currentNode.Next;
				}
			}
			_table = table;
			_buckets = buckets;
			_empty = empty;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			for (var i = 0; i < _table.Length; i++)
			{
				builder.Append($"{i}:");
				var currentNode = _table[i];
				while (currentNode != null)
				{
					builder.Append($" {currentNode}");
					currentNode = currentNode.Next;
				}
				builder.Append("\n");
			}
			return builder.ToString();
		}
	}
}
