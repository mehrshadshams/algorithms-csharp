using System;
using System.Collections.Generic;
using System.Linq;
using AIMA.Search;

namespace AIMA.Environments.Map
{
    public class BidirectionalMapProblem : GeneralProblem<string, MoveToAction>,
        IBidirectionalProblem<string, MoveToAction>
    {
        private IProblem<string, MoveToAction> reverseProblem;

        public BidirectionalMapProblem(IMap map, string initialState, string goalState) : this(map, initialState,
            goalState, state => String.Equals(state, goalState))
        {
        }

        public BidirectionalMapProblem(IMap map, string initialState, string goalState, Predicate<string> goalTest)
            : base(initialState, Actions(map), Result(), goalTest, DistanceStepCostFunction(map))
        {
            reverseProblem = new GeneralProblem<string, MoveToAction>(goalState,
                ReverseActions(map),
                Result(),
                goalTest,
                DistanceStepCostFunction(map));
        }

        public IProblem<string, MoveToAction> OriginalProblem => this;

        public IProblem<string, MoveToAction> ReverseProblem => reverseProblem;

        private static Func<string, MoveToAction, string> Result()
        {
            return (state, action) => action.To;
        }

        private static Func<string, IEnumerable<MoveToAction>> ReverseActions(IMap map)
        {
            return state => map.GetPossiblePrevLocations(state).Select(loc => new MoveToAction(loc));
        }

        private static Func<string, IEnumerable<MoveToAction>> Actions(IMap map)
        {
            return state => map.GetPossibleNextLocations(state).Select(loc => new MoveToAction(loc));
        }

        private static StepCostFunction<string, MoveToAction> DistanceStepCostFunction(IMap map)
        {
            return (state, action, delta) =>
            {
                double? distance = map.GetDistance(state, delta);
                return (distance.HasValue && distance > 0) ? distance.Value : 0.1;
            };
        }
    }
}