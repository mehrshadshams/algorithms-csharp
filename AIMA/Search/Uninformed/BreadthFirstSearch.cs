using AIMA.Agent;

namespace AIMA.Search.QueueSearch
{
    public class BreadthFirstSearch<TState, TAction> : QueueBasedSearch<TState, TAction>
    {
        public BreadthFirstSearch() : this(new GraphSearch<TState, TAction>())
        {
            
        }
        public BreadthFirstSearch(ISearchStrategy<TState, TAction> searchStrategy) : base(searchStrategy, QueueBuilder<StateNode<TState, TAction>>.Fifo())
        {
        }
    }
}