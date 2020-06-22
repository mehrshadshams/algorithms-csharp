using System;
using System.Collections.Generic;

namespace AIMA.Util
{
    public static class Util
    {
        static Random random = new Random();
        public static bool CompareDoubles(double a, double b)
        {
            return Math.Abs(a - b) <= Double.Epsilon;
        }
        
        public static T SelectRandom<T>(this IList<T> list) {
            return list[random.Next(list.Count)];
        }
    }
}