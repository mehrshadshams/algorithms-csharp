using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgorithmsLibrary.DataStructures
{
    /// <summary>
    /// http://web.stanford.edu/class/archive/cs/cs166/cs166.1166/lectures/15/Small15.pdf
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class YFastTrie<TKey> : IOrderedSet<TKey>
        where TKey : struct
    {
        private readonly int universeSize;
        private readonly int halfLogU;
        private readonly int twoLogU;
        private List<SortedSet<TKey>> trees;
        private readonly Comparer<TKey> comparer;
        
        public YFastTrie(int universeSize)
        {
            this.universeSize = universeSize;
            this.halfLogU = (int)(0.5 * Math.Log(universeSize, 2.0));
            this.twoLogU = (int) (2 * Math.Log(universeSize, 2.0));
            this.trees = new List<SortedSet<TKey>>();
            this.comparer = Comparer<TKey>.Default;
        }
        
        public IEnumerator<TKey> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TKey item)
        {
            if (trees.Count == 0)
            {
                trees.Add(new SortedSet<TKey>());
                trees[0].Add(item);
                return;
            }
            
            // Find the tree that contains the successor of key using binary search
            int lo = 0, hi = trees.Count;
            int candidate = -1;
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                var tree = trees[mid];
                int cmp = comparer.Compare(item, tree.Min); 
                if (cmp < 0)
                {
                    candidate = mid;
                    lo = mid + 1;
                } else if (cmp > 0)
                {
                    hi = mid - 1;
                }
                else
                {
                    // key already exists
                    return;
                }
            }

            if (candidate >= 0)
            {
                trees[candidate].Add(item);
            }
            else
            {
                var newTree = new SortedSet<TKey>();
                newTree.Add(item);
                trees.Add(newTree);
            }
            
            
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(TKey item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(TKey[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(TKey item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public TKey Max { get; }
        public TKey Min { get; }
        public TKey? Successor(TKey key)
        {
            throw new System.NotImplementedException();
        }

        public TKey? Predecessor(TKey key)
        {
            throw new System.NotImplementedException();
        }
    }
}