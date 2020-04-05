using System;
using System.Collections.Generic;

namespace algorithms.Algorithms.DataStructures
{
    public enum Operation
    {
        Min,
        Max,
        Sum,
        Multiply,
    }

    /*
     * Doc
    */
    public class SparseTable
    {
        // The number of elements in the input array
        private readonly int n;

        // The maximum power of 2 i.e. floor(log2(n))
        private readonly int P;

        // The operation this instance supports
        private readonly Operation operation;

        private int[] log2;

        private long[,] dp;

        private readonly Dictionary<Operation, Func<long, long, long>> operationFuncs = new Dictionary<Operation, Func<long, long, long>>
        {
            [Operation.Min] = Math.Min,
            [Operation.Max] = Math.Max,
            [Operation.Sum] = (arg1, arg2) => arg1 + arg2,
            [Operation.Multiply] = (arg1, arg2) => arg1 * arg2
        };


        public SparseTable(long[] values, Operation operation)
        {            
            this.operation = operation;
            n = values.Length;
            P = (int) Math.Floor(Math.Log(n, 2));
            dp = new long[P + 1, n];
            log2 = new int[n + 1];            

            for (int i=0; i<n; i++)
            {
                dp[0, i] = values[i];
            }

            for (int i=2; i<=n; i++)
            {
                log2[i] = log2[i / 2] + 1;
            }

            for (int i=1; i<=P; i++)
            {
                for (int j=0; j + (1 << i) <=n; j++)
                {
                    long left = dp[i - 1, j];
                    long right = dp[i - 1, j + (1 << (i - 1))];

                    dp[i, j] = operationFuncs[operation].Invoke(left, right);
                }
            }
        }

        public long Query(int left, int right)
        {
            int len = right - left + 1;
            int p = log2[len];
            switch (operation)
            {
                case Operation.Min:
                case Operation.Max:
                    return operationFuncs[operation].Invoke(dp[p, left], dp[p, right - (1 << p) + 1]);
                case Operation.Sum:
                    return SumQuery(left, right);
                case Operation.Multiply:
                    return MulitplyQuery(left, right);
                default:
                    throw new ArgumentException(message: $"Unknown operation ${operation}");
            }
        }

        private long SumQuery(int left, int right)
        {
            long sum = 0;
            for (int p = log2[right - left + 1]; left <= right; p = log2[right - left + 1])
            {
                sum += dp[p, left];
                left += (1 << p);
            }
            return sum;
        }

        private long MulitplyQuery(int left, int right)
        {
            long product = 1;
            for (int p = log2[right - left + 1]; left <= right; p = log2[right - left + 1])
            {
                product *= dp[p, left];
                left += (1 << p);
            }
            return product;
        }
    }
}
