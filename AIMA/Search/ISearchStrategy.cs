using AIMA.Agent;
using AIMA.Search;
using AlgorithmsLibrary.DataStructures;
using Optional;

namespace AIMA
{
    public interface ISearchStrategy<TState, TAction>
    {
        Option<StateNode<TState, TAction>> FindSolution(IQueue<StateNode<TState, TAction>> frontier, IProblem<TState, TAction> problem);
    }
}