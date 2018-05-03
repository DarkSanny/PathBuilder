using System;

namespace Structures
{
    public abstract class Tree <T>
    {

	    protected Func<T, T, int> Compare;

	    protected Tree()
	    {
		    if (default(T) is IComparable<T>) Compare = (f, s) => ((IComparable<T>) f).CompareTo(s);
		    else Compare = (f, s) => f.GetHashCode().CompareTo(s.GetHashCode());
	    }

		public abstract void Insert(T item);

	    public abstract void Remove(T item);

	    public abstract T GetMinOrThrow();
	    public abstract T GetMaxOrThrow();
    }
}
