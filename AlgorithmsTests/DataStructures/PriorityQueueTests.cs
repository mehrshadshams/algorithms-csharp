using System;
using System.Collections.Generic;
using AlgorithmsLibrary.DataStructures;
using AlgorithmsLibrary.Utils;
using NUnit.Framework;

namespace AlgorithmsTests.DataStructures
{
    [TestFixture()]
    public class PriorityQueueTests
    {
        [Test()]
        public void TestPqWorksWithComparer()
        {
            var pq = new PriorityQueue<(int age, string name)>
                (Comparer<(int age, string name)>.Create((o1,o2)=>o1.age.CompareTo(o2.age)));

            pq.Add((20, "John"));
            pq.Add((18, "Mary"));
            pq.Add((22, "Tom"));
            pq.Add((17, "Sara"));

            Assert.AreEqual((17, "Sara"), pq.Peek());
        }

        [Test()]
        public void TestPqWorksWithComparableClass()
        {
            var pq = new PriorityQueue<Person>();

            pq.Add(new Person(20, "John"));
            pq.Add(new Person(18, "Mary"));
            pq.Add(new Person(22, "Tom"));
            pq.Add(new Person(17, "Sara"));

            Assert.AreEqual("Sara", pq.Peek().Name);
        }

        [Test()]
        public void TestPqWorksWithDescendingComparer()
        {
            var pq = new PriorityQueue<Person>(Person.DESCENDING_AGE);

            pq.Add(new Person(20, "John"));
            pq.Add(new Person(18, "Mary"));
            pq.Add(new Person(22, "Tom"));
            pq.Add(new Person(17, "Sara"));

            Assert.AreEqual("Tom", pq.Peek().Name);
        }

        [Test()]
        public void TestDequeWorks()
        {
            var pq = new PriorityQueue<Person>();

            pq.Add(new Person(20, "John"));
            pq.Add(new Person(18, "Mary"));
            pq.Add(new Person(22, "Tom"));
            pq.Add(new Person(17, "Sara"));

            var p = pq.Dequeue();

            Assert.AreEqual("Sara", p.Name);
            Assert.AreEqual("Mary", pq.Peek().Name);
        }

        [Test()]
        public void TestClearWorks()
        {
            var pq = new PriorityQueue<Person>();

            pq.Add(new Person(20, "John"));
            pq.Add(new Person(18, "Mary"));
            pq.Add(new Person(22, "Tom"));
            pq.Add(new Person(17, "Sara"));

            pq.Clear();

            Assert.AreEqual(0, pq.Count);            
            Assert.IsNull(pq.Peek());
        }

        [Test()]
        public void TestRemoveWorks()
        {
            var pq = new PriorityQueue<Person>();

            var candidate = new Person(17, "Sara");

            pq.Add(new Person(20, "John"));
            pq.Add(new Person(18, "Mary"));
            pq.Add(new Person(22, "Tom"));
            pq.Add(candidate);

            pq.Remove(candidate);

            Assert.AreEqual("Mary", pq.Peek().Name);
        }

        [Test()]
        public void TestContainsWorks()
        {
            var pq = new PriorityQueue<Person>();

            var candidate = new Person(17, "Sara");

            pq.Add(new Person(20, "John"));
            pq.Add(new Person(18, "Mary"));
            pq.Add(new Person(22, "Tom"));
            pq.Add(candidate);

            Assert.IsTrue(pq.Contains(candidate));
            Assert.IsFalse(pq.Contains(new Person(1, "")));
        }

        [Test()]
        public void TestGrowWorks()
        {
            var pq = new PriorityQueue<Person>();

            for (int i = 0; i < 20; i++)
            {
                pq.Add(new Person(i + 1, Guid.NewGuid().ToString()));                
            }            

            Assert.AreEqual(20, pq.Count);
            Assert.IsNotNull(pq.Peek());
            Assert.AreEqual(1, pq.Peek().Age);
        }

        [Test()]
        public void TestPqWorksFromCollection()
        {
            var list = new List<Person>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(new Person(i + 1, Guid.NewGuid().ToString()));
            }

            list.Shuffle();

            var pq = new PriorityQueue<Person>(list);

            Assert.AreEqual(list.Count, pq.Count);            
            Assert.AreEqual(1, pq.Peek().Age);
        }

        [Test()]
        public void TestEnumertorWorks()
        {
            int size = 20;
            var pq = new PriorityQueue<Person>();

            for (int i = 0; i < size; i++)
            {
                pq.Add(new Person(i + 1, Guid.NewGuid().ToString()));
            }

            int count = 0;
            foreach (var p in pq)
            {
                count++;
            }

            Assert.AreEqual(size, count);
        }

        [Test]
        public void TestEnumertorPreventsModification()
        {
            int size = 20;
            var pq = new PriorityQueue<Person>();

            for (int i = 0; i < size; i++)
            {
                pq.Add(new Person(i + 1, Guid.NewGuid().ToString()));
            }

            Assert.That(() =>
            {
                var e = pq.GetEnumerator();
                if (e.MoveNext())
                {
                    pq.Add(new Person(0, ""));
                    _ = e.Current;
                }

            }, Throws.InvalidOperationException);
        }
    }

    public class Person : IComparable<Person>
    {
        public static IComparer<Person> DESCENDING_AGE = Comparer<Person>.Create((o1, o2) => o2.Age.CompareTo(o1.Age));

        public int Age { get; private set; }
        public string Name { get; private set; }

        public Person(int age, string name)
        {
            Age = age;
            Name = name;
        }

        public int CompareTo(Person other)
        {
            return Age.CompareTo(other.Age);
        }
    }
}
