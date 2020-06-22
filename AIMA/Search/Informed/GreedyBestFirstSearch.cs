namespace AIMA.Informed
{
    public class GreedyBestFirstSearch<TState, TAction> : BestFirstSearch<TState, TAction>
    {
        public GreedyBestFirstSearch(ISearchStrategy<TState, TAction> searchStrategy,
            EvaluationFunction<TState, TAction> evaluationFunction) : base(searchStrategy, evaluationFunction)
        {
        }
    }
}