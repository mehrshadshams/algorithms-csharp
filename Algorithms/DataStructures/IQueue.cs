using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgorithmsLibrary.DataStructures
{
    public interface IQueue<T> : IEnumerable<T>, IEnumerable, ICollection<T>
    {
        void Enqueue(T item);

        T Peek();

        T Dequeue();
    }
}
