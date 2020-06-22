using AIMA.Agent;

namespace AIMA.Search
{
    public interface IBidirectionalProblem<TState, TAction>
    {
        IProblem<TState, TAction> OriginalProblem { get; }
        IProblem<TState, TAction> ReverseProblem { get; }
    }
}