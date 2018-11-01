using System;
using System.Diagnostics;
using Benchmark.Benchmarks.DataAnnotation;

namespace Benchmark
{
    class Program
    {
        private const int DefaultCycleRepeats = 100000;
        private const int DefaultBenchmarkRepeats = 5;

        static void Main(string[] args)
        {
            int benchmarkRepeats = GetValue(args, 0, "BenchmarkRepeats" ,DefaultBenchmarkRepeats);
            int cycleRepeats = GetValue(args, 1, "CycleRepeats", DefaultCycleRepeats);

            new BenchmarkRunner().Start(benchmarkRepeats, cycleRepeats);
            Console.ReadLine();
        }

        private static int GetValue(string[] args, int index, string argName, int defaultValue)
        {
            if (args.Length == 0 || index >= args.Length)
            {
                return defaultValue;
            }
            else if (int.TryParse(args[index], out int value))
            {
                return value;
            }

            Console.WriteLine($"Argument \"{args[index]}\" isn't number and {argName} default value will be used: {defaultValue}.");
            return defaultValue;
        }
    }
}
