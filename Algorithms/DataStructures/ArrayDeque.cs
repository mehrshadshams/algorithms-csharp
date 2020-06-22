using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgorithmsLibrary.DataStructures
{
    public class ArrayDeque<T> : IDeque<T>
    {
        private static readonly int DEFAULT_CAPACITY = 16;

        private T[] array;
        private int head;
        private int tail;

        public ArrayDeque() : this(DEFAULT_CAPACITY)
        {            
        }

        public ArrayDeque(int capacity)
        {
            array = new T[capacity];
            head = tail = 0;
        }

        public int Count => (tail - head) & (array.Length - 1);

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            AddLast(item);
        }

        public void AddFirst(T item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }

            head = (head - 1) & (array.Length - 1);
            array[head] = item;
            if (head == tail)
            {
                DoubleCapacity();
            }
        }

        public void AddLast(T item)
        {
            if (item == null)
            {
                throw new NullReferenceException();
            }

            array[tail] = item;
            tail = (tail + 1) & (array.Length - 1);

            if (head == tail)
            {
                DoubleCapacity();
            }
        }        

        public void Clear()
        {
            Array.Clear(array, 0, array.Length);
            head = 0;
            tail = 0;
        }

        public bool Contains(T item)
        {
            int index = IndexOf(item);
            return index >= 0;
        }

        public void CopyTo(T[] destinationArray, int arrayIndex)
        {
            Array.Copy(array, 0, destinationArray, arrayIndex, array.Length);
        }

        public T Dequeue()
        {
            return RemoveFirst();   
        }

        public void Enqueue(T item)
        {
            AddLast(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        public T Peek()
        {
            return PeekFirst();
        }

        public T PeekFirst()
        {
            if (Count == 0) return default(T);
            return array[head];
        }

        public T PeekLast()
        {
            if (Count == 0) return default(T);
            return array[(tail - 1) & (array.Length - 1)];
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                return false;

            }

            int mask = array.Length - 1;
            int i = head;
            T x;
            while ((x = array[i]) != null)
            {
                if (x.Equals(item))
                {
                    Delete(i);
                    return true;
                }

                i = (i + 1) & mask;
            }

            return false;
        }

        public T RemoveFirst()
        {
            int h = head;
            T elem = array[h];
            if (elem == null)
            {
                return default;
            }

            array[h] = default;
            head = (h + 1) & (array.Length - 1);
            return elem;
        }

        public T RemoveLast()
        {
            int t = (tail - 1) & (array.Length - 1);
            T elem = array[t];
            if (elem == null)
            {
                return default;
            }

            array[t] = default(T);
            tail = t;
            return elem;
        }

        private bool Delete(int i)
        {
            T[] elements = array;
            int mask = array.Length - 1;
            int h = head;
            int t = tail;

            int front = (i - h) & mask;
            int back = (t - i) & mask;

            // Invariant: head <= i < tail mod circularity
            if (front >= ((t - h) & mask))
                throw new InvalidOperationException("Concurrent Modification");

            if (front < back)
            {
                if (h <= i)
                {
                    Array.Copy(elements, h, elements, h + 1, front);
                }
                else
                {
                    // Wrap around
                    Array.Copy(elements, 0, elements, 1, i);
                    elements[0] = elements[mask];
                    Array.Copy(elements, h, elements, h + 1, mask - h);
                }
                elements[h] = default;
                head = (h + 1) & mask;
                return false;
            }
            else
            {
                if (i < t)
                { // Copy the null tail as well
                    Array.Copy(elements, i + 1, elements, i, back);
                    tail = t - 1;
                }
                else
                { // Wrap around
                    Array.Copy(elements, i + 1, elements, i, mask - i);
                    elements[mask] = elements[0];
                    Array.Copy(elements, 1, elements, 0, t);
                    tail = (t - 1) & mask;
                }
                return true;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void DoubleCapacity()
        {
            int p = head;
            int n = array.Length;
            int r = n - p; // number of elements to the right of p
            int newCapacity = n << 1;
            if (newCapacity < 0)
                throw new InvalidOperationException("Sorry, deque too big");
            T[] a = new T[newCapacity];
            Array.Copy(array, p, a, 0, r);
            Array.Copy(array, 0, a, r, p);
            array = a;
            head = 0;
            tail = n;
        }

        private int IndexOf(T item)
        {
            if (item != null)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (item.Equals(array[i]))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public struct Enumerator : IEnumerator<T>
        {
            private int cursor;

            private int fence;

            private readonly ArrayDeque<T> deque;

            private T current;

            public Enumerator(ArrayDeque<T> deque)
            {
                this.deque = deque;
                this.cursor = deque.head;
                this.fence = deque.tail;
                this.current = default;
            }

            public T Current
            {
                get
                {
                    if (deque.tail != fence)
                        throw new InvalidOperationException();
                    return current;
                }
            }

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {                
            }

            public bool MoveNext()
            {
                bool hasNext = cursor != fence;
                if (!hasNext) return hasNext;                
                T result = deque.array[cursor];
                if (deque.tail != fence || result == null)
                    throw new InvalidOperationException();
                current = result;
                cursor = (cursor + 1) & (deque.array.Length - 1);
                return hasNext;
            }

            public void Reset()
            {
                cursor = deque.head;
                current = default;
            }
        }
    }
}
