namespace Structures
{
	public class ArrayStack	<T>
	{
		private readonly ArrayList<T> _list = new ArrayList<T>();
		public int Count => _list.Count;

		public void Push(T item)
		{
			_list.Add(item);
		}

		public T Peek()
		{
			return _list[Count - 1];
		}

		public T Pop()
		{
			return _list.RemoteAt(Count - 1);
		}
	}
}
