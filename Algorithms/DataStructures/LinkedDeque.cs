using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgorithmsLibrary.DataStructures
{
    public class LinkedDeque<T> : IDeque<T>
    {
        private readonly LinkedList<T> list;

        public LinkedDeque()
        {
            list = new LinkedList<T>();            
        }

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            AddFirst(item);
        }

        public void AddFirst(T item)
        {
            list.AddFirst(item);
        }

        public void AddLast(T item)
        {
            list.AddLast(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public T Dequeue()
        {
            T last = PeekLast();
            list.RemoveLast();
            return last;
        }

        public void Enqueue(T item)
        {
            AddLast(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public T Peek()
        {
            return PeekLast();
        }

        public T PeekFirst()
        {
            return list.First.Value;
        }

        public T PeekLast()
        {
            return list.Last.Value;
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public T RemoveFirst()
        {
            var first = PeekFirst();
            list.RemoveFirst();
            return first;
        }

        public T RemoveLast()
        {
            var last = PeekLast();
            list.RemoveLast();
            return last;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
