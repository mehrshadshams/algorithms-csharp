using System.Collections.Generic;
using AIMA.Search;
using AIMA.Search.QueueSearch;
using NUnit.Framework;
using Optional.Unsafe;

namespace AIMA.Tests.Search.Uninformed
{
    [TestFixture]
    public class DepthFirstSearchTests
    {
        [Test]
        public void TestDepthFirstSuccessfulSearch()
        {
            var problem = new NQueenProblem(new NQueenBoard(8));
            var search = new DepthFirstSearch<NQueenBoard, NQueenAction>(new GraphSearch<NQueenBoard, NQueenAction>());
            var solutionOptional = search.FindSolution(problem);
            
            Assert.IsTrue(solutionOptional.HasValue);

            var solution = solutionOptional.ValueOrDefault();
            
            Assert.IsNotNull(solution);
            
            Assert.IsNotEmpty(solution.Actions);
            AssertCorrectPlacement(solution.Actions);
        }
        
        private void AssertCorrectPlacement(IList<NQueenAction> actions) {
            Assert.AreEqual(8, actions.Count);
            Assert.AreEqual("Action[name=PlaceQueen, location=(0, 7)]", actions[0].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(1, 3)]", actions[1].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(2, 0)]", actions[2].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(3, 2)]", actions[3].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(4, 5)]", actions[4].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(5, 1)]", actions[5].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(6, 6)]", actions[6].ToString());
            Assert.AreEqual("Action[name=PlaceQueen, location=(7, 4)]", actions[7].ToString());
        }
    }
}