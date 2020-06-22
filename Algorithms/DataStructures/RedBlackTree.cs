using System.Collections;
using System.Collections.Generic;

namespace AlgorithmsLibrary.DataStructures
{
    public class RedBlackTree<TKey, TValue> : IOrderedDictionary<TKey, TValue>
        where TKey : struct
    {
        private Node root;
        private readonly IComparer<TKey> comparer;
        
        public RedBlackTree()
        {
            this.comparer = Comparer<TKey>.Default;
        }
        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        private void AddInternal(TKey key, TValue value)
        {
            if (root == null)
            {
                root = new Node(key, value, 1, NodeColor.Red);
                root.Color = NodeColor.Black;

                return;
            }

            Node parent = null;
            Node current = root;
            while (current != null)
            {
                int cmp = comparer.Compare(key, current.Key);
                if (cmp == 0)
                {
                    current.Value = value;
                    current.Color = NodeColor.Black;
                    return;
                }

                if (cmp < 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else
                {
                    parent = current;
                    current = current.Right;
                }
            }
            
            // if (Node.IsRed(parent.Right) && !Node.IsRed(parent.Left))
            // {
            //     parent = RotateLeft(parent);
            // }
            //
            // if (Node.IsRed(parent.Left) && Node.IsRed(parent.Left.Left))
            // {
            //     parent = RotateRight(parent);
            // }
            //
            // if (Node.IsRed(parent.Left) && Node.IsRed(parent.Right))
            // {
            //     parent.FlipColor();
            // }

            parent.Count = 1 + Size(parent.Left) + Size(parent.Right);

            // return parent;
        }

        private int Size(Node node)
        {
            return node?.Count ?? 0;
        }

        private Node RotateRight(Node h)
        {
            Node x = h.Left;
            h.Left = x.Right;
            x.Right = h;
            x.Color = h.Color;
            h.Color = NodeColor.Red;
            return x;
        }

        private Node RotateLeft(Node h)
        {
            Node x = h.Right;
            h.Right = x.Left;
            x.Left = h;
            x.Color = h.Color;
            h.Color = NodeColor.Red;
            return x;
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new System.NotImplementedException();
        }

        public int Count
        {
            get
            {
                if (root == null) return 0;
                return root.Count;
            }
        }

        public bool IsReadOnly => false;
        
        public void Add(TKey key, TValue value)
        {
            AddInternal(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new System.NotImplementedException();
        }

        public TValue this[TKey key]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public ICollection<TKey> Keys { get; }
        public ICollection<TValue> Values { get; }
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

        internal sealed class Node
        {
            public TKey Key { get; set; }

            public TValue Value { get; set; }

            public Node Left { get; set; }
            
            public Node Right { get; set; }

            public NodeColor Color { get; set; }

            public int Count { get; set; }

            public Node(TKey key, TValue value, int count, NodeColor color)
            {
                this.Key = key;
                this.Value = value;
                this.Color = color;
                this.Count = count;
            }

            public static bool IsRed(Node n)
            {
                if (n == null) return false;
                return n.Color == NodeColor.Red;
            }

            public void FlipColor()
            {
                this.Color = NodeColor.Red;
                if (this.Left != null)
                {
                    this.Left.Color = NodeColor.Black;
                }

                if (this.Right != null)
                {
                    this.Right.Color = NodeColor.Black;
                }
            }
        }

        internal enum NodeColor
        {
            Black,
            Red
        }
    }
}