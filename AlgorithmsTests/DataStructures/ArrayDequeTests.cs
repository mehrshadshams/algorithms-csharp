using System;
using AlgorithmsLibrary.DataStructures;
using NUnit.Framework;

namespace AlgorithmsTests.DataStructures
{
    [TestFixture]
    public class ArrayDequeTests
    {
        [Test]
        public void TestAddFirstWorks()
        {
            var deque = new ArrayDeque<int>();
            deque.AddFirst(1);
            deque.AddFirst(2);
            deque.AddFirst(3);

            Assert.AreEqual(3, deque.Count);
            Assert.AreEqual(1, deque.PeekLast());
            Assert.AreEqual(3, deque.PeekFirst());
        }

        [Test]
        public void TestAddLastWorks()
        {
            var deque = new ArrayDeque<int>();
            deque.AddLast(1);
            deque.AddLast(2);
            deque.AddLast(3);

            Assert.AreEqual(3, deque.Count);
            Assert.AreEqual(3, deque.PeekLast());
            Assert.AreEqual(1, deque.PeekFirst());
        }

        [Test]
        public void TestBothAddsWork()
        {
            var deque = new ArrayDeque<int>();
            deque.AddFirst(1);
            deque.AddFirst(2);
            deque.AddLast(3);
            deque.AddLast(4);

            Assert.AreEqual(4, deque.Count);
            Assert.AreEqual(4, deque.PeekLast());
            Assert.AreEqual(2, deque.PeekFirst());
        }
    }
}
