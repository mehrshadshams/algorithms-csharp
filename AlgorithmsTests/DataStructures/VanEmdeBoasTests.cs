using System;
using AlgorithmsLibrary.DataStructures;
using NUnit.Framework;

namespace AlgorithmsTests.DataStructures
{
    [TestFixture()]
    public class VanEmdeBoasTests
    {
        [Test()]
        public void testInsertWorks()
        {
            var tree = new VanEmdeBoas(8);

            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(6);

            Assert.AreEqual(2, tree.Minimum);
            Assert.AreEqual(6, tree.Maximum);

            Assert.IsTrue(tree.Contains(3));
            Assert.IsFalse(tree.Contains(7));
        }

        [Test()]
        public void testPredecessorSuccessorWorks()
        {
            var tree = new VanEmdeBoas(8);

            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(6);

            Assert.AreEqual(3, tree.Successor(2));
            Assert.AreEqual(4, tree.Predecessor(6));
            Assert.AreEqual(6, tree.Successor(4));
        }

        [Test()]
        public void testDeleteWorks()
        {
            var tree = new VanEmdeBoas(8);

            tree.Insert(1);
            tree.Insert(0);
            tree.Insert(2);
            tree.Insert(4);

            Assert.IsTrue(tree.Contains(2));

            Assert.AreEqual(2, tree.Predecessor(4));
            Assert.AreEqual(2, tree.Successor(1));

            tree.Delete(2);

            Assert.IsFalse(tree.Contains(2));
            Assert.AreEqual(1, tree.Predecessor(4));
            Assert.AreEqual(4, tree.Successor(1));
        }
    }
}
