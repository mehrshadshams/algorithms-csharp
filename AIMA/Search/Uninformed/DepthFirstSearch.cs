using AIMA.Agent;

namespace AIMA.Search.QueueSearch
{
    public class DepthFirstSearch<TState, TAction> : QueueBasedSearch<TState, TAction>
    {
        public DepthFirstSearch() : this(new GraphSearch<TState, TAction>())
        {
        }
        
        public DepthFirstSearch(ISearchStrategy<TState, TAction> searchStrategy) : base(searchStrategy, QueueBuilder<StateNode<TState, TAction>>.Lifo())
        {
        }
    }
}