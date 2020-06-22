using System;
using System.Collections.Generic;

namespace AIMA.Informed
{
    public class EvaluationFunction<TState, TAction>
    {
        public virtual Func<StateNode<TState, TAction>, double> H { get; set; }

        public EvaluationFunction() : this(n => 0.0)
        {
        }

        public EvaluationFunction(Func<StateNode<TState, TAction>, double> h)
        {
            this.H = h;
        }

        public Comparison<StateNode<TState, TAction>> Comparison
        {
            get { return (a, b) => H.Invoke(a).CompareTo(H.Invoke(b)); }
        }
    }
}