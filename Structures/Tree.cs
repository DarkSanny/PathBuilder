using System;

namespace Structures
{
    public abstract class Tree <T> where T : IComparable<T>
    {

        public virtual void Insert(T item)
        {
            
        }

        public virtual void Remove(T item)
        {
            
        }

        public virtual T GetMinOrThrow()
        {
            throw new Exception("Tree is empty");
        }

    }
}
