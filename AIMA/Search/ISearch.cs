using AIMA.Agent;
using AIMA.Search;
using Optional;

namespace AIMA
{
    public interface ISearch<TState, TAction>
    {
        Option<SearchResult<TState, TAction>> FindSolution(IProblem<TState, TAction> problem);
    }
}