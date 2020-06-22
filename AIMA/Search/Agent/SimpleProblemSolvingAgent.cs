using System.Collections.Generic;
using System.Linq;
using AIMA.Agent;
using AIMA.Search;
using AlgorithmsLibrary.DataStructures;
using AlgorithmsLibrary.Utils;
using Optional;
using Optional.Unsafe;

namespace AIMA
{
    public abstract class SimpleProblemSolvingAgent<TPercept, TState, TAction> : SimpleAgent<TPercept, TAction>
    {
        private IDeque<TAction> seq = new LinkedDeque<TAction>();
        
        public SimpleProblemSolvingAgent()
        {
            Alive = true;
        }

        public override Option<TAction> Act(TPercept percept)
        {
            UpdateState(percept);

            if (seq.IsEmpty())
            {
                Option<object> goal = FormulateGoal();
                if (goal.HasValue)
                {
                    var problem = FormulateProblem(goal);
                    var solution = SearchProblem(problem);
                    if (solution.HasValue)
                    {
                        solution.ValueOrDefault().Actions.ToList().ForEach(item => seq.AddLast(item));
                    }
                }
                else
                {
                    Alive = false;
                }
            }

            return seq.IsEmpty() ? Option.None<TAction>() : Option.Some(seq.RemoveFirst());
        }

        public abstract Option<SearchResult<TState, TAction>> SearchProblem(IProblem<TState,TAction> problem);

        protected abstract IProblem<TState, TAction> FormulateProblem(object goal);

        protected abstract Option<object> FormulateGoal();

        protected abstract void UpdateState(TPercept percept);
    }
}