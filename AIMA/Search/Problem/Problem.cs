using System.Collections.Generic;
using AIMA.Agent;

namespace AIMA.Search
{
    public interface IProblem<TState, TAction>
    {
        TState InitialState { get; }

        IEnumerable<TAction> GetActions(TState state);

        TState GetResult(TState state, TAction action);

        double GetStepCosts(TState state, TAction action, TState stateDelta);
        
        bool GoalTest(TState state);
    }
}