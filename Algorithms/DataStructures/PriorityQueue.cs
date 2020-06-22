using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgorithmsLibrary.DataStructures
{
    [Serializable]
    public class PriorityQueue<T> : IQueue<T>, IReadOnlyCollection<T>
    {
        private static readonly int DEFAULT_INITIAL_CAPACITY = 11;
        private static readonly int MAX_ARRAY_SIZE = int.MaxValue - 8;

        private T[] queue;
        private readonly IComparer<T> comparer;
        private int size;
        private int version;

        public PriorityQueue() : this(DEFAULT_INITIAL_CAPACITY, null)
        {            
        }

        public PriorityQueue(IComparer<T> comparer) : this(DEFAULT_INITIAL_CAPACITY, comparer)
        {
        }

        public PriorityQueue(int initialCapacity, IComparer<T> comparer)
        {
            if (initialCapacity < 1) throw new ArgumentException();

            this.queue = new T[initialCapacity];
            this.comparer = comparer;
        }

        public PriorityQueue(ICollection<T> collection)
        {
            this.comparer = null;            
            InitializeFromCollection(collection);
        }

        public int Count => size;

        public bool IsSynchronized => false;

        public object SyncRoot => null;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (item == null) throw new NullReferenceException();
            int sz = size;
            if (sz >= queue.Length)
            {
                Grow(sz + 1);
            }

            size = sz + 1;
            if (sz == 0)
            {
                queue[0] = item;
            }
            else
            {
                SiftUp(sz, item);
            }

            version++;
        }

        public T this[int index]
        {
            get
            {
                return queue[index];
            }
        }

        public void Enqueue(T item)
        {
            Add(item);
        }

        public T Peek()
        {            
            if (Count == 0) return default(T);
            return queue[0];
        }

        public T Dequeue()
        {
            if (size == 0) return default(T);
            int sz = --size;
            T result = queue[0];
            T last = queue[sz];
            queue[sz] = default(T);
            if (sz != 0)
            {
                SiftDown(0, last);
            }

            version++;

            return result;
        }

        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                queue[i] = default(T);
            }

            size = 0;
            version++;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(queue, 0, array, arrayIndex, array.Length);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }        

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1) return false;

            bool result = false;
            int i = --size;
            if (i == index)
            {
                queue[i] = default(T);
                result = true;
            }
            else
            {
                T moved = queue[i];
                queue[i] = default(T);
                SiftDown(index, moved);
                if ((object) queue[index] == (object) moved)
                {
                    SiftUp(index, moved);
                    if ((object) queue[index] != (object) moved)
                    {
                        result = true;
                    }
                }
            }

            if (result) version++;

            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int IndexOf(T item)
        {
            if ((object)item != null)
            {
                for (int i = 0; i < size; i++)
                {
                    if (item.Equals(queue[i]))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private void InitializeFromCollection(ICollection<T> items)
        {
            queue = new T[items.Count];
            items.CopyTo(queue, 0);
            size = queue.Length;
            Heapify();
        }

        private void Heapify()
        {
            for (int i = (size >> 1) - 1; i >= 0; i--)
            {
                SiftDown(i, queue[i]);
            }
        }

        private void Grow(int newSize)
        {
            int oldCap = queue.Length;
            int newCap = oldCap + ((oldCap < 64 ? oldCap + 2 : oldCap >> 1));

            if (newCap - MAX_ARRAY_SIZE > 0)
            {
                newCap = HugeCapacity(newSize);
            }

            Array.Resize(ref queue, newCap);
        }        

        private int CompareTo(T a, T b)
        {
            if (this.comparer != null)
            {
                return comparer.Compare(a, b);
            }

            if (a is IComparable<T>)
            {
                return ((IComparable<T>)a).CompareTo(b);
            }

            throw new Exception($"Unable to compare instances of type {typeof(T)}");
        }

        private void SiftUp(int k, T item)
        {
            while (k > 0)
            {
                int parent = (k - 1) >> 1;
                T p = queue[parent];
                if (CompareTo(item, p) >= 0)
                {
                    break;
                }

                queue[k] = p;
                k = parent;
            }
            queue[k] = item;
        }        

        private void SiftDown(int k, T item)
        {
            int half = size >> 1;
            while (k < half)
            {
                int leftChildIndex = (k << 1) + 1;
                int rightChildIndex = leftChildIndex + 1;
                int childIndex = leftChildIndex;
                T child = queue[leftChildIndex];
                if (rightChildIndex < size && CompareTo(child, queue[rightChildIndex]) > 0)
                {
                    childIndex = rightChildIndex;
                    child = queue[childIndex];
                }

                if (CompareTo(item, child) <= 0)
                {
                    break;
                }

                queue[k] = child;
                k = childIndex;
            }
            queue[k] = item;
        }

        private static int HugeCapacity(int minCapacity)
        {
            if (minCapacity < 0) throw new OutOfMemoryException();
            return minCapacity > MAX_ARRAY_SIZE ? int.MaxValue : MAX_ARRAY_SIZE;
        }

        [Serializable]
        public struct Enumerator : IEnumerator<T>
        {
            private int cursor;
            private readonly int version;
            private readonly PriorityQueue<T> pq;
            private T current;

            public Enumerator(PriorityQueue<T> pq)
            {
                this.version = pq.version;
                this.pq = pq;
                this.current = default(T);
                this.cursor = 0;
            }

            public T Current
            {
                get
                {
                    if (cursor > pq.size) throw new InvalidOperationException("Enumeration overflowed");
                    if (version != pq.version) throw new InvalidOperationException("Collection modified");
                    return current;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                PriorityQueue<T> local = pq;
                if (version != local.version) throw new InvalidOperationException("Collection modified");
                if (cursor > pq.size - 1) return false;
                current = pq[cursor];
                cursor++;
                return true;
            }

            public void Reset()
            {
                if (version != pq.version) throw new InvalidOperationException("Collection modified");

                cursor = 0;
                current = default(T);
            }
        }
    }
}
