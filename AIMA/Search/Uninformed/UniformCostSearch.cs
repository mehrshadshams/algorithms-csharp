using System;
using System.Collections.Generic;
using AIMA.Agent;

namespace AIMA.Search.QueueSearch
{
    public class UniformCostSearch<TState, TAction> : QueueBasedSearch<TState, TAction>
    {
        public UniformCostSearch() : this(new GraphSearch<TState, TAction>())
        {
        }

        /// <summary>
        /// Combines UniformCostSearch queue definition with the specified
        /// search execution strategy.
        /// </summary>
        /// <param name="searchStrategy"></param>
        public UniformCostSearch(ISearchStrategy<TState, TAction> searchStrategy)
            : base(searchStrategy,
                QueueBuilder<StateNode<TState, TAction>>.PriorityQueue(StateNode<TState, TAction>.IncreasingPathCost))
        {
        }
    }
}