using System;

namespace Structures
{
    public abstract class Tree <T> where T : IComparable<T>
    {
		public abstract void Insert(T item);

	    public abstract void Remove(T item);

	    public abstract T GetMinOrThrow();
	    public abstract T GetMaxOrThrow();
    }
}
