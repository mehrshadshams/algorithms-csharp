﻿using System;
namespace AlgorithmsLibrary.DataStructures
{
    public interface IDeque<T> : IQueue<T>
    {
        void AddFirst(T item);

        void AddLast(T item);

        T RemoveFirst();

        T RemoveLast();

        T PeekFirst();

        T PeekLast();
    }
}
