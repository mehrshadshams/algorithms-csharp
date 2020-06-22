using AIMA.Agent;
using AIMA.Search;
using Optional;

namespace AIMA
{
    public class SearchAgent<TPercept, TState, TAction> : SimpleAgent<TPercept, TAction>
    {
        private readonly IProblem<TState, TAction> problem;
        private readonly ISearch<TState, TAction> search;

        public SearchAgent(IProblem<TState, TAction> problem, ISearch<TState, TAction> search)
        {
            this.problem = problem;
            this.search = search;
        }

        public Option<SearchResult<TState, TAction>> FindSolution()
        {
            return search.FindSolution(problem);
        }
    }
}