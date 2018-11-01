using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Benchmark.Abstract;

namespace Benchmark
{
    public class BenchmarkRunner
    {
        private static readonly Type[] BenchmarkTypes;

        private readonly IBenchmark[] _benchmarkInstances;
        private readonly Dictionary<string, Action<IBenchmark>> _benchmarkActions
            = new Dictionary<string, Action<IBenchmark>>
            {
            { "With intercepted calling", b => b.SwitchToIntercepting() },
            { "With direct calling     ", b => b.SwitchToDirectCalling() }
        };

        static BenchmarkRunner()
        {
            BenchmarkTypes = typeof(BenchmarkRunner).Assembly.GetTypes()
                .Where(t => typeof(IBenchmark).IsAssignableFrom(t) && !t.IsAbstract).ToArray();
        }

        public BenchmarkRunner()
        {
            _benchmarkInstances = BenchmarkTypes.Select(t => (IBenchmark)Activator.CreateInstance(t)).ToArray();
        }

        public void Start(int benchmarkRepeats = 2, int cycleRepeats = 1000)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"Benchmark will be run {benchmarkRepeats} times.");
            Console.WriteLine($"Cycle with validation will be repeated {cycleRepeats} times. \n");

            Console.ResetColor();

            for (int i = 0; i < benchmarkRepeats; i++)
            {
                if (benchmarkRepeats > 1)
                {
                    Console.WriteLine($"Benchmark № {i+1} \n");
                }

                foreach (var benchmarkInstance in _benchmarkInstances)
                {
                    

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(benchmarkInstance.GetType().Name);
                    Console.ResetColor();

                    foreach (var keyValuePair in _benchmarkActions)
                    {
                        var stopWatch = new Stopwatch();

                        keyValuePair.Value(benchmarkInstance);

                        stopWatch.Start();

                        benchmarkInstance.Start(cycleRepeats);

                        stopWatch.Stop();

                        Console.WriteLine($"   {keyValuePair.Key}  =====> {stopWatch.Elapsed.TotalSeconds} seconds");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    }
}
