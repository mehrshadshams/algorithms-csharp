using System.Collections.Generic;
using System.Linq;
using AIMA.Agent;
using AIMA.Search;
using Optional;

namespace AIMA.Environments.Map
{
    public class MapAgent : ProblemSolvingAgent<DynamicPercept, string, MoveToAction>
    {
        private IMap map;
        private DynamicState state = new DynamicState();
        private readonly IList<string> goals = new List<string>();
        private ISearch<string,MoveToAction> search;
        private int nextGoalPos = 0;

        public MapAgent(IMap map, ISearch<string, MoveToAction> search, string goal) {
            this.map = map;
            this.search = search;
            goals.Add(goal);
        }

        public MapAgent(IMap map, ISearch<string, MoveToAction> search, IList<string> goals) {
            this.map = map;
            this.search = search;
            goals.ToList().ForEach(this.goals.Add);
        }
        
        protected override Option<SearchResult<string, MoveToAction>> SearchProblem(IProblem<string, MoveToAction> problem)
        {
            return search.FindSolution(problem);
        }

        protected override IProblem<string, MoveToAction> FormulateProblem(object goal)
        {
            return new BidirectionalMapProblem(map, state[AttNames.AGENT_LOCATION] as string, (string) goal);
        }

        protected override Option<object> FormulateGoal()
        {
            string goal = null;
            if (nextGoalPos < goals.Count) {
                goal = goals[nextGoalPos++];
                // if (hFnFactory != null && search instanceof Informed)
                // ((Informed<String, MoveToAction>) search).setHeuristicFunction(hFnFactory.apply(goal));
                // if (notifier != null)
                //     notifier.notify("Current location: In(" + state.getAttribute(AttNames.AGENT_LOCATION)
                //                                             + "), Goal: In(" + goal + ")");
            }

            return Option.Some<object>(goal);
        }

        protected override void UpdateState(DynamicPercept percept)
        {
            state[AttNames.AGENT_LOCATION] = percept[AttNames.PERCEPT_IN];
        }
    }
}