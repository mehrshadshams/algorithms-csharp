using System;

namespace AIMA.Informed
{
    public interface IInformedSearch<TState, TAction> {
        void SetHeuristicFunction(Func<StateNode<TState, TAction>, Double> h);
    }
}