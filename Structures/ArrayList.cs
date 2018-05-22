using System.Linq;

namespace Structures
{
	public class ArrayList<T>
	{
		private T[] _array;
		public int Count { get; private set; }
		public int Capacity => _array.Length;

		public ArrayList()
		{
			_array = new T[2];
			Count = 0;
		}

		public void Add(T value)
		{
			if (Count >= Capacity) Enlarge();
			_array[Count] = value;
			Count++;
		}

		private void Enlarge()
		{
			var tmp = new T[_array.Length * 2];
			for (var i = 0; i < _array.Length; i++)
				tmp[i] = _array[i];
			_array = tmp;
		}

		private void Reduse()
		{
			if (Capacity == 2 || Count >= Capacity / 2) return;
			var tmp = new T[Capacity / 2];
			for (var i = 0; i < tmp.Length; i++)
				tmp[i] = _array[i];
			_array = tmp;
		}

		public T RemoteAt(int index)
		{
			var item = _array[index];
			Count--;
			for (var i = index; i < _array.Length - 1; i++)
				_array[i] = _array[i + 1];
			if (Count < (_array.Length / 2.0) / 3.0 * 2) Reduse();
			return item;
		}

		public T this[int index]
		{
			get => _array[index];
			set => _array[index] = value;
		}

		public override string ToString()
		{
			return string.Join(" ", _array.Take(Count));
		}
	}
}