using AIMA.Agent;
using Optional;
using Optional.Unsafe;

namespace AIMA.Search.QueueSearch
{
    public class IterativeDeepeningSearch<TState, TAction> : ISearch<TState, TAction>
    {
        private static readonly StateNode<TState, TAction> CutoffNode = new StateNode<TState, TAction>(default);
        
        private readonly int limit;
        private readonly NodeFactory<TState, TAction> nodeFactory;

        public IterativeDeepeningSearch(int limit) : this(limit, new NodeFactory<TState, TAction>())
        {
        }

        public IterativeDeepeningSearch(int limit, NodeFactory<TState, TAction> nodeFactory)
        {
            this.limit = limit;
            this.nodeFactory = nodeFactory;
        }

        public Option<SearchResult<TState, TAction>> FindSolution(IProblem<TState, TAction> problem)
        {
            for (int currentLimit = 0; currentLimit < limit; currentLimit++)
            {
                var depthLimitSearch = new DepthLimitedSearch<TState, TAction>(currentLimit);
                var solution = depthLimitSearch.FindSolution(problem);
                if (solution.HasValue)
                {
                    return solution;
                }
            }
            
            return Option.None<SearchResult<TState, TAction>>();
        }
    }
}