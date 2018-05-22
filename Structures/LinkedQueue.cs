namespace Structures
{
	public class LinkedQueue <T>
	{
		readonly LinkedList<T> _queue = new LinkedList<T>();
		public int Count => _queue.Count;

		public void Enqueue(T item)
		{
			_queue.AddStart(item);
		}

		public T Dequeue()
		{
			return _queue.RemoveAt(Count - 1);
		}
	}
}
