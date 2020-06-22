using System;
using AIMA.Search;
using AIMA.Search.QueueSearch;
using NUnit.Framework;
using Optional.Unsafe;

namespace AIMA.Tests.Search.Uninformed
{
    [TestFixture]
    public class BreadthFirstSearchTests
    {
        [Test]
        public void TestBreadthFirstSuccessfulSearch()
        {
            var problem = new NQueenProblem(new NQueenBoard(8));
            var search = new BreadthFirstSearch<NQueenBoard, NQueenAction>();

            var solutionOptional = search.FindSolution(problem);
            Assert.IsTrue(solutionOptional.HasValue);

            var solution = solutionOptional.ValueOrDefault();

            AssertCorrectPlacement(solution);

            Assert.AreEqual(8.0, solution.Cost);
        }

        [Test]
        public void TestBreadthFirstUnSuccessfulSearch()
        {
            var problem = new NQueenProblem(new NQueenBoard(3));
            var search = new BreadthFirstSearch<NQueenBoard, NQueenAction>(new TreeSearch<NQueenBoard, NQueenAction>());
            var agent = new SearchAgent<Object, NQueenBoard, NQueenAction>(problem, search);

            var solutionOptional = agent.FindSolution();
            Assert.IsFalse(solutionOptional.HasValue);
        }

        //
        // PRIVATE METHODS
        //
        private void AssertCorrectPlacement(SearchResult<NQueenBoard, NQueenAction> solution)
        {
            var actions = solution.Actions;
            Assert.AreEqual(8, actions.Count);
            Assert.AreEqual("Action[name=PlaceQueen, location=(0, 0)]", actions[0].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(1, 4)]", actions[1].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(2, 7)]", actions[2].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(3, 5)]", actions[3].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(4, 2)]", actions[4].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(5, 6)]", actions[5].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(6, 1)]", actions[6].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(7, 3)]", actions[7].ToString());
        }
    }
}