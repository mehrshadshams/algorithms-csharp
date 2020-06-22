using System.Collections.Generic;
using AIMA.Agent;
using AIMA.Search;

namespace AIMA
{
    public class NodeFactory<TState, TAction>
    {
        public StateNode<TState, TAction> Create(TState state)
        {
            return new StateNode<TState, TAction>(state);
        }

        public StateNode<TState, TAction> Create(TState state, StateNode<TState, TAction> parent, TAction action, double stepCost)
        {
            return new StateNode<TState, TAction>(state, action, parent, 0, (parent?.PathCost ?? 0) + stepCost);
        }

        public IEnumerable<StateNode<TState,TAction>> GetSuccessors(StateNode<TState,TAction> node, IProblem<TState,TAction> problem)
        {
            foreach (TAction action in problem.GetActions(node.State))
            {
                var successorState = problem.GetResult(node.State, action);

                double stepCost = problem.GetStepCosts(node.State, action, successorState);
                yield return Create(successorState, node, action, stepCost);
            }
        }
    }
}