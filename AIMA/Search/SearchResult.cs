using System.Collections.Generic;
using AIMA.Agent;

namespace AIMA
{
    public class SearchResult<TState, TAction>
    {
        public IList<TAction> Actions { get; }
        public TState Goal { get; }

        public double Cost { get; }

        public Dictionary<string, object> Metrics { get; set; }

        public SearchResult()
        {
            Metrics = new Dictionary<string, object>();
        }

        public SearchResult(IList<TAction> actions, TState goal, double cost)
        {
            this.Actions = actions;
            this.Goal = goal;
            this.Cost = cost;
        }
    }
}