using System.Collections.Generic;

namespace AlgorithmsLibrary.DataStructures
{
    public interface IOrderedSet<TKey> : ICollection<TKey>
        where TKey : struct
    {
        TKey Max { get; }
        TKey Min { get; }
        TKey? Successor(TKey key);
        TKey? Predecessor(TKey key);
    }
}