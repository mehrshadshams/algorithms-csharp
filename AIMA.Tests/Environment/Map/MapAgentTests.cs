using System;
using System.Collections.Generic;
using System.Linq;
using AIMA.Environments.Map;
using AIMA.Search.QueueSearch;
using NUnit.Framework;
using Optional.Unsafe;

namespace AIMA.Tests.Environment.Map
{
    [TestFixture]
    public class MapAgentTests
    {
        private ExtendableMap map;

        [SetUp]
        public void Setup()
        {
            map = new ExtendableMap();
            map.AddBidirectionalLink("A", "B", 5.0);
            map.AddBidirectionalLink("A", "C", 6.0);
            map.AddBidirectionalLink("B", "C", 4.0);
            map.AddBidirectionalLink("C", "D", 7.0);
            map.AddUnidirectionalLink("B", "E", 14.0);
        }

        [Test]
        public void TestAlreadyAtGoal()
        {
            var goal = "A";
            var search = new UniformCostSearch<string, MoveToAction>();
            var agent = new SimpleMapAgent(map, search, goal);
            
            var problem = new BidirectionalMapProblem(map, goal, goal);

            var solution = agent.SearchProblem(problem);

            Assert.IsNotNull(solution);
            Assert.IsTrue(solution.HasValue);

            Assert.IsEmpty(solution.ValueOrDefault().Actions);
        }

        [Test]
        public void TestNormalSearch()
        {
            var goal = "D";
            var search = new UniformCostSearch<string, MoveToAction>();
            var agent = new SimpleMapAgent(map, search, goal);

            var problem = new BidirectionalMapProblem(map, "A", goal);

            var solutionOptional = agent.SearchProblem(problem);

            Assert.IsNotNull(solutionOptional);
            Assert.IsTrue(solutionOptional.HasValue);

            var solution = solutionOptional.ValueOrDefault();

            var actions = solution.Actions;

            Assert.IsNotEmpty(solution.Actions);
            Assert.AreEqual("C", actions[0].To);
            Assert.AreEqual("D", actions[1].To);
            Assert.AreEqual(13.0, solution.Cost);
        }
    }
}