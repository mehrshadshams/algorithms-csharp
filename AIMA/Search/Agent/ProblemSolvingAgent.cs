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
    public abstract class ProblemSolvingAgent<TPercept, TState, TAction> : SimpleAgent<TPercept, TAction>
    {
        private IDeque<TAction> plan = new LinkedDeque<TAction>();
        
        public ProblemSolvingAgent()
        {
            Alive = true;
        }

        public override Option<TAction> Act(TPercept percept)
        {
            UpdateState(percept);

            while (plan.IsEmpty())
            {
                Option<object> goal = FormulateGoal();
                if (goal.HasValue)
                {
                    var problem = FormulateProblem(goal);
                    var solution = SearchProblem(problem);
                    if (solution.HasValue)
                    {
                        solution.ValueOrDefault().Actions.ToList().ForEach(item => plan.AddLast(item));
                    }
                }
                else
                {
                    Alive = false;
                    break;
                }
            }

            return plan.IsEmpty() ? Option.None<TAction>() : Option.Some<TAction>(plan.RemoveFirst());
        }

        protected abstract Option<SearchResult<TState, TAction>> SearchProblem(IProblem<TState,TAction> problem);

        protected abstract IProblem<TState, TAction> FormulateProblem(object goal);

        protected abstract Option<object> FormulateGoal();

        protected abstract void UpdateState(TPercept percept);
    }
}