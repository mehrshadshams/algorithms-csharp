using System;
using System.Linq;
using AIMA.Agent;
using AIMA.Environments.Map;
using AIMA.Informed;
using AIMA.Search;
using AIMA.Search.QueueSearch;
using Newtonsoft.Json;
using Optional.Unsafe;

namespace AIMA
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // var algorithm = SearchAlgorithmBuilder.FromName<EightPuzzleBoard, DynamicAction>(args[0]);
            // var board = EightPuzzleBoard.FromValues(args[1].Split(',').AsEnumerable().Select(int.Parse).ToList());
            // var problem = new EightPuzzleProblem(board);
            // var agent = new SearchAgent<Object, EightPuzzleBoard, DynamicAction>(problem, algorithm);
            //
            // // var problem = new GeneralProblem<NQueenBoard, NQueenAction>(new NQueenBoard(8));
            // // var search = new BreadthFirstSearch<NQueenBoard, NQueenAction>();
            // // var agent = new SearchAgent<Object, NQueenBoard, NQueenAction>(problem, search);
            //
            // var result = agent.FindSolution();
            //
            // if (result.HasValue)
            // {
            //     var searchResult = result.ValueOrDefault();
            //     Console.WriteLine("Found solution Cost={0}", searchResult.Cost);
            //     Console.WriteLine(searchResult.Goal);
            // }
            // else
            // {
            //     Console.WriteLine("Unsuccessful Search.");
            // }
            // var romaniaMap = new SimplifiedRoadMapOfRomania();
            // var problem = new BidirectionalMapProblem(romaniaMap, SimplifiedRoadMapOfRomania.ARAD,
            //     SimplifiedRoadMapOfRomania.BUCHAREST);
            // var search = new GreedyBestFirstSearch<string, MoveToAction>(new GraphSearch<string, MoveToAction>(),
            //     MapFunctions.CreateSLDHeuristicFunction(SimplifiedRoadMapOfRomania.BUCHAREST, romaniaMap));
            //
            // var solution = search.FindSolution(problem).ValueOrDefault();
            // foreach (var action in solution.Actions)
            // {
            //     Console.WriteLine(action.To);
            // }
            
            
        }
    }
}