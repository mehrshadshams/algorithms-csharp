using System;
using AIMA.Informed;

namespace AIMA.Environments.Map
{
    public class MapFunctions
    {
        public static EvaluationFunction<string, MoveToAction> CreateSLDHeuristicFunction(
            string goal, IMap map)
        {
            return new EvaluationFunction<string, MoveToAction>(
                node => GetSLD(node.State, goal, map));
        }

        public static double GetSLD(String loc1, String loc2, IMap map)
        {
            double result = 0.0;
            var pt1 = map.GetPosition(loc1);
            var pt2 = map.GetPosition(loc2);
            var dist = pt1.Distance(pt2);
            return dist;
        }
    }
}