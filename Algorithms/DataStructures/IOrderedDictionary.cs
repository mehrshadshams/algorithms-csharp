using System.Collections.Generic;

namespace AlgorithmsLibrary.DataStructures
{
    public interface IOrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : struct
    {
        TKey Max { get; }
        TKey Min { get; }
        TKey? Successor(TKey key);
        TKey? Predecessor(TKey key);
    }
}