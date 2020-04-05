using System;
using System.Diagnostics;
using algorithms.Algorithms.DataStructures;
using NUnit.Framework;

namespace AlgorithmsTests.DataStructures
{
    [TestFixture()]
    public class SparsTableTests
    {
        static readonly long[] ARRAY1 = { 1, 2, -3, 2, 4, -1, 5 };
        static readonly long[] ARRAY2 = { 4, 2, 3, 7, 1, 5, 3, 3, 9, 6, 7, -1, 4 };

        [Test()]
        public void TestMax()
        {
            var sparseTable = new SparseTable(ARRAY2, Operation.Max);

            Assert.AreEqual(7, sparseTable.Query(0, 3));
        }

        [Test()]
        public void TestMin()
        {
            var sparseTable = new SparseTable(ARRAY2, Operation.Min);

            Assert.AreEqual(1, sparseTable.Query(2, 7));
        }

        [Test()]
        public void TestSum()
        {
            var sparseTable = new SparseTable(ARRAY2, Operation.Sum);

            Assert.AreEqual(22, sparseTable.Query(2, 7));
        }

        [Test()]
        public void TestProduct()
        {
            var sparseTable = new SparseTable(ARRAY1, Operation.Multiply);

            Assert.AreEqual(-12, sparseTable.Query(1, 3));
        }

        [Test()]
        public void TestRandomSum()
        {
            long[] values = new long[1_000_000];
            var random = new Random(0);
            for (int i=0; i<values.Length; i++)
            {
                values[i] = random.Next(1000);
            }

            var left = 100;
            var right = values.Length - 100;

            var sparseTable = new SparseTable(values, Operation.Min);
            var sw = Stopwatch.StartNew();
            var resultSparse = sparseTable.Query(left, right);
            sw.Stop();
            var elapsed1 = sw.ElapsedMilliseconds;

            sw = Stopwatch.StartNew();
            var min = long.MaxValue;
            for (int i=left; i<=right; i++)
            {
                min = Math.Min(min, values[i]);
            }
            sw.Stop();
            var elapsed2 = sw.ElapsedMilliseconds;

            Assert.AreEqual(min, resultSparse);

            Console.WriteLine($"SparseTable=${elapsed1}, Linear: ${elapsed2}");
        }
    }
}
