using AIMA.Agent;
using AlgorithmsLibrary.DataStructures;
using Optional;
using Optional.Unsafe;

namespace AIMA.Search.QueueSearch
{
    public class DepthLimitedSearch<TState, TAction> : ISearch<TState, TAction>
    {
        private static readonly StateNode<TState, TAction> CutoffNode = new StateNode<TState, TAction>(default);
        
        private readonly int limit;
        private readonly NodeFactory<TState, TAction> nodeFactory;

        public DepthLimitedSearch(int limit) : this(limit, new NodeFactory<TState, TAction>())
        {
        }

        public DepthLimitedSearch(int limit, NodeFactory<TState, TAction> nodeFactory)
        {
            this.limit = limit;
            this.nodeFactory = nodeFactory;
        }

        public Option<SearchResult<TState, TAction>> FindSolution(IProblem<TState, TAction> problem)
        {
            var output = RecursiveDLS(nodeFactory.Create(problem.InitialState), problem,
                limit);
            
            if (output.HasValue)
            {
                var finalState = output.ValueOrDefault();
                return Option.Some(new SearchResult<TState, TAction>(finalState.Actions, finalState.State,
                    finalState.PathCost));
            }
            
            return Option.None<SearchResult<TState, TAction>>();
        }

        private Option<StateNode<TState, TAction>> RecursiveDLS(StateNode<TState, TAction> node,
            IProblem<TState, TAction> problem, int limit)
        {
            if (problem.GoalTest(node.State))
            {
                return Option.Some(node);
            }
            else if (limit == 0)
            {
                return Option.Some(CutoffNode);
            }
            else
            {
                var cutoff = false;
                foreach (var child in nodeFactory.GetSuccessors(node, problem))
                {
                    var result = RecursiveDLS(child, problem, limit - 1);
                    if (result.HasValue && result.ValueOrDefault() == CutoffNode)
                    {
                        cutoff = true;
                    }
                    else if (result.HasValue)
                    {
                        return result;
                    }
                }

                return cutoff ? Option.Some(CutoffNode) : Option.None<StateNode<TState, TAction>>();
            }
        }
    }
}