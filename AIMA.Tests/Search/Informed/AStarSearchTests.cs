using System;
using AIMA.Agent;
using AIMA.Environments.Map;
using AIMA.Informed;
using AIMA.Search;
using AIMA.Search.QueueSearch;
using NUnit.Framework;
using Optional.Unsafe;

namespace AIMA.Tests.Search.Informed
{
    [TestFixture]
    public class AStarSearchTests
    {
        [Test]
        public void TestPuzzle()
        {
            var board = new EightPuzzleBoard(new[]
            {
                7, 1, 8,
                0, 4, 6,
                2, 3, 5
            });

            var problem = new EightPuzzleProblem(board);
            var search = new AStarSearch<EightPuzzleBoard, DynamicAction>(
                new GraphSearch<EightPuzzleBoard, DynamicAction>(),
                new EvaluationFunction<EightPuzzleBoard, DynamicAction>(EightPuzzleBoard.GetManhattanDistance));

            var solutionOptional = search.FindSolution(problem);
            Assert.IsNotNull(solutionOptional);
            Assert.IsTrue(solutionOptional.HasValue);

            var solution = solutionOptional.ValueOrDefault();

            Assert.AreEqual(23, solution.Actions.Count);
        }

        [Test]
        public void TestAIMA3eFigure3_15()
        {
            var romaniaMap = new SimplifiedRoadMapOfRomania();
            var problem = new BidirectionalMapProblem(romaniaMap, SimplifiedRoadMapOfRomania.ARAD,
                SimplifiedRoadMapOfRomania.BUCHAREST);
            var search = new AStarSearch<string, MoveToAction>(new GraphSearch<string, MoveToAction>(),
                MapFunctions.CreateSLDHeuristicFunction(SimplifiedRoadMapOfRomania.BUCHAREST, romaniaMap));

            var solution = search.FindSolution(problem);

            var actions = solution.ValueOrDefault().Actions;
            Assert.AreEqual(SimplifiedRoadMapOfRomania.SIBIU, actions[0].To);
            Assert.AreEqual(SimplifiedRoadMapOfRomania.RIMNICU_VILCEA, actions[1].To);
            Assert.AreEqual(SimplifiedRoadMapOfRomania.PITESTI, actions[2].To);
            Assert.AreEqual(SimplifiedRoadMapOfRomania.BUCHAREST, actions[3].To);
            Assert.AreEqual(418.0, solution.ValueOrDefault().Cost);
        }
    }
}