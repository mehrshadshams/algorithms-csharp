using System;
using System.Collections;
using System.Collections.Generic;
using AlgorithmsLibrary.DataStructures;

namespace AIMA
{
    public class QueueBuilder<T>
    {
        public static IQueue<T> Fifo()
        {
            return new ArrayDeque<T>();
        }

        public static IQueue<T> Lifo()
        {
            return new LifoQueue(new ArrayDeque<T>());
        }

        public static IQueue<T> PriorityQueue(Comparison<T> comparison)
        {
            return new PriorityQueue<T>(Comparer<T>.Create(comparison));
        }
        
        private class LifoQueue : IQueue<T>
        {
            private readonly IDeque<T> deque;

            public LifoQueue(IDeque<T> deque)
            {
                this.deque = deque;
            }
            
            public IEnumerator<T> GetEnumerator()
            {
                return deque.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(T item)
            {
                deque.Add(item);
            }

            public void Clear()
            {
                deque.Clear();
            }

            public bool Contains(T item)
            {
                return deque.Contains(item);
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                deque.CopyTo(array, arrayIndex);
            }

            public bool Remove(T item)
            {
                return deque.Remove(item);
            }

            public int Count => deque.Count;
            public bool IsReadOnly => deque.IsReadOnly;
            public void Enqueue(T item)
            {
                deque.AddLast(item);
            }

            public T Peek()
            {
                return deque.PeekLast();
            }

            public T Dequeue()
            {
                return deque.RemoveLast();
            }
        }
    }
}