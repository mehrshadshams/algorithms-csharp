using System;
using AIMA.Agent;
using AIMA.Search.QueueSearch;

namespace AIMA
{
    public class SearchAlgorithmBuilder
    {
        public static ISearch<TState, TAction> FromName<TState, TAction>(string alg)
        {
            switch (alg.ToLowerInvariant())
            {
                case "bfs":
                    return new BreadthFirstSearch<TState, TAction>();
                case "dfs":
                    return new DepthFirstSearch<TState, TAction>();
                default:
                    throw new ArgumentException($"Unknown search algorithm ${alg}");
            }
        }
    }
}