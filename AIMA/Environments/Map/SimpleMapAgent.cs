using System;
using System.Collections.Generic;
using AIMA.Agent;
using AIMA.Search;
using Optional;

namespace AIMA.Environments.Map
{
    public class SimpleMapAgent : SimpleProblemSolvingAgent<DynamicPercept, string, MoveToAction>
    {
        protected IMap map;
        protected DynamicState state = new DynamicState();

        private ISearch<string, MoveToAction> search;
        private List<string> goals;
        private int nextGoalPos = 0;

        /** Randomly generates goals forever. */
        public SimpleMapAgent(IMap map, ISearch<string, MoveToAction> search)
        {
            this.map = map;
            this.search = search;
        }

        public SimpleMapAgent(IMap map, ISearch<string, MoveToAction> search, string goal)
        {
            this.map = map;
            this.search = search;
            goals = new List<string>();
            goals.Add(goal);
        }

        public SimpleMapAgent(IMap map, ISearch<string, MoveToAction> search, List<string> goals)
        {
            this.map = map;
            this.search = search;
            this.goals = goals;
        }

        //
        // PROTECTED METHODS
        //
        protected override void UpdateState(DynamicPercept percept)
        {
            state[AttNames.AGENT_LOCATION] = percept[AttNames.PERCEPT_IN].ToString();
        }

        protected override Option<Object> FormulateGoal()
        {
            Object goal = null;
            if (goals == null)
                goal = map.RandomlyGenerateDestination();
            else if (nextGoalPos < goals.Count)
                goal = goals[nextGoalPos++];
            return Option.Some(goal);
        }

        public override Option<SearchResult<string, MoveToAction>> SearchProblem(IProblem<string, MoveToAction> problem)
        {
            return search.FindSolution(problem);
        }

        protected override IProblem<string, MoveToAction> FormulateProblem(Object goal)
        {
            return new BidirectionalMapProblem(map, state[AttNames.AGENT_LOCATION] as string,  goal as string);
        }
    }
}