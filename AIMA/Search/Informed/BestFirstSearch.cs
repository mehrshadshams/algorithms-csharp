using System;
using AIMA.Search.QueueSearch;
using AlgorithmsLibrary.DataStructures;

namespace AIMA.Informed
{
    public class BestFirstSearch<TState, TAction> : QueueBasedSearch<TState, TAction>, IInformedSearch<TState, TAction>
    {
        private readonly EvaluationFunction<TState, TAction> evaluationFunction;

        public BestFirstSearch(ISearchStrategy<TState, TAction> searchStrategy,
            EvaluationFunction<TState, TAction> evaluationFunction) : base(searchStrategy,
            QueueBuilder<StateNode<TState, TAction>>.PriorityQueue(evaluationFunction.Comparison))
        {
            this.evaluationFunction = evaluationFunction;
        }
        
        public void SetHeuristicFunction(Func<StateNode<TState, TAction>, double> h)
        {
            evaluationFunction.H = h;
        }
    }
}