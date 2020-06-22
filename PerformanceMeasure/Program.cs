using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using AlgorithmsLibrary.DataStructures;
using AlgorithmsLibrary.Utils;

namespace PerformanceMeasurement
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            // for (var i = 250; true; i += i)
            // {
            //     var set = new SortedSet<int>();
            //
            //     long res;
            //     res = TestAdd(set, i);
            //     // res = TestFind(set, i);
            //     res = TestRemoveMin(set, i);
            //     Console.WriteLine($"{i}\t{res}");
            //     GC.Collect();
            // }

            var sw = Stopwatch.StartNew();

            
            var localRandom = new ThreadLocal<Random>(() =>
            {
                Console.WriteLine("Creating Random");
                return new Random(Guid.NewGuid().GetHashCode());
            });
            var result = Observable.Range(0, 1_000_000)
                .SubscribeOn(ThreadPoolScheduler.Instance)
                .Select(i => localRandom.Value.Next(0, 1_000_000))
                .ToEnumerable()
                .Count();

            Console.WriteLine(result);
                
            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        static async Task Foo(CancellationToken cancellationToken, IProgress<int> progress)
        {
            for (int i = 0; i < 10; i++) {
                progress.Report(i);
                await Task.Delay (1000, cancellationToken); 
            }
        } 

        public static long TestAdd(ICollection<int> set, int n)
        {
            var random = new Random(0);

            var sw = Stopwatch.StartNew();
            for (var i = 0; i < n; i++)
            {
                set.Add(random.Next(2 * n));
            }

            sw.Stop();
            
            return sw.ElapsedMilliseconds;
        }

        private static long TestFind(ICollection<int> set, int n)
        {
            var random = new Random(0);
            
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < n; i++)
            {
                _ = set.Contains(random.Next(n));
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
        
        private static long TestRemoveMin(ICollection<int> pq, int n)
        {
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < n; i++)
            {
                _ = pq.Min();
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}