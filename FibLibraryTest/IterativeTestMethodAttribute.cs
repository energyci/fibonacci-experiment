using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace StringLibraryTest {


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IterativeTestMethodAttribute : TestMethodAttribute {
        private int stabilityThreshold;
        private const string FILE_PATH = "/sys/devices/virtual/powercap/intel-rapl/intel-rapl:0/intel-rapl:0:0/energy_uj";

        private string read_rapl_value() {
            return System.IO.File.ReadAllText(FILE_PATH);
        }

        public IterativeTestMethodAttribute(int stabilityThreshold) {
            this.stabilityThreshold = stabilityThreshold;
        }

        public override TestResult[] Execute(ITestMethod testMethod) {
            var results = new List<TestResult>();
            var tuples = new List<(decimal, long)>();
            var begin_watch = System.Diagnostics.Stopwatch.StartNew();
            TestResult[]? currentResults = null;

            while (begin_watch.ElapsedMilliseconds < stabilityThreshold * 1_000) {
                string before_value = read_rapl_value();
                long before_time = begin_watch.ElapsedTicks;

                currentResults = base.Execute(testMethod);

                string new_value = read_rapl_value();
                long after_time = begin_watch.ElapsedTicks;

                var energy_consumption = Decimal.Parse(new_value) - Decimal.Parse(before_value);
                // Time in ticks.
                var time_elapsed = after_time - before_time;
                tuples.Add((energy_consumption, time_elapsed));

            }
            
            if(currentResults != null)
                results.AddRange(currentResults);

            
            foreach (var tuple in tuples) {
                System.Console.WriteLine(tuple.Item1 + ";" + tuple.Item2);
            }
            var ticks_per_seconds = (decimal)1 / Stopwatch.Frequency;
            var ticks_per_milliseconds = (decimal)1 / Stopwatch.Frequency * 1000;
            var ticks_per_nanoseconds = (decimal)1 / Stopwatch.Frequency * 1000000000;
            Console.WriteLine("Sec per tick: {0}, ms per tick: {1}, ns per tick: {2}", ticks_per_seconds, ticks_per_milliseconds, ticks_per_nanoseconds);
            System.Console.WriteLine("Frequency: {0}", System.Diagnostics.Stopwatch.Frequency);
            return results.ToArray();
        }
    }
}