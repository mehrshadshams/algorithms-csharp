using AIMA.Agent;
using AlgorithmsLibrary.DataStructures;
using Optional;
using Optional.Unsafe;

namespace AIMA.Search.QueueSearch
{
    public abstract class QueueBasedSearch<TState, TAction> : ISearch<TState, TAction>
    {
        public static string MetricNodesExpanded = "nodesExpanded";
        public static string MetricQueueSize = "queueSize";
        public static string MetricMaxQueueSize = "maxQueueSize";
            
        private readonly ISearchStrategy<TState, TAction> searchStrategy;
        private readonly IQueue<StateNode<TState, TAction>> frontier;

        protected QueueBasedSearch(ISearchStrategy<TState, TAction> searchStrategy, IQueue<StateNode<TState, TAction>> queue)
        {
            this.searchStrategy = searchStrategy;
            this.frontier = queue;
        }
        
        public Option<SearchResult<TState, TAction>> FindSolution(IProblem<TState, TAction> problem)
        {
            var solution = searchStrategy.FindSolution(frontier, problem);
            if (solution.HasValue)
            {
                StateNode<TState, TAction> finalState = solution.ValueOrDefault();
                return Option.Some(new SearchResult<TState, TAction>(finalState.Actions, finalState.State, finalState.PathCost));
            }

            return Option.None<SearchResult<TState, TAction>>();
        }
    }
}