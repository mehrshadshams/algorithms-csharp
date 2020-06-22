namespace AIMA.Informed
{
    public class AStarSearch<TState, TAction> : BestFirstSearch<TState, TAction>
    {
        public AStarSearch(ISearchStrategy<TState, TAction> searchStrategy, EvaluationFunction<TState, TAction> h) 
            : base(searchStrategy, CreateEvalFn(h))
        {
        }

        private static EvaluationFunction<TState,TAction> CreateEvalFn(EvaluationFunction<TState,TAction> evalFn)
        {
            return new EvaluationFunction<TState, TAction>(node =>
            {
                var h = evalFn.H.Invoke(node);
                var g = node.PathCost;
                return g + h;
            });
        }
    }
}