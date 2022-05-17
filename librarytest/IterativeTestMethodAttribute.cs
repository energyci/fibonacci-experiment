using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace StringLibraryTest {


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IterativeTestMethodAttribute : TestMethodAttribute {
        private int stabilityThreshold;
        private const string CORE_FILE_PATH = "/sys/devices/virtual/powercap/intel-rapl/intel-rapl:0/intel-rapl:0:0/energy_uj";
        private const string PACKAGE_FILE_PATH = "/sys/devices/virtual/powercap/intel-rapl/intel-rapl:0/energy_uj";

        private string read_core_rapl_value() {
            return System.IO.File.ReadAllText(CORE_FILE_PATH);
        }

        private string read_package_rapl_value() {
            return System.IO.File.ReadAllText(PACKAGE_FILE_PATH);
        }

        public IterativeTestMethodAttribute(int stabilityThreshold) {
            this.stabilityThreshold = stabilityThreshold;
        }

        public override TestResult[] Execute(ITestMethod testMethod) {
            var results = new List<TestResult>();
            var tuples = new List<(decimal, decimal, long, int)>();
            var begin_watch = System.Diagnostics.Stopwatch.StartNew();
            TestResult[]? currentResults = null;

            while (begin_watch.ElapsedMilliseconds < stabilityThreshold * 1_000) {
                int iteration_counter = 0;
                string before_core_value = read_core_rapl_value();
                string before_package_value = read_package_rapl_value();
                long before_time = begin_watch.ElapsedTicks;
                long iteration_time = begin_watch.ElapsedMilliseconds;

                
                while (begin_watch.ElapsedMilliseconds - iteration_time < 200) {
                    currentResults = base.Execute(testMethod);
                    iteration_counter++;
                }

                string new_core_value = read_core_rapl_value();
                string new_package_value = read_package_rapl_value();
                long after_time = begin_watch.ElapsedTicks;

                var energy_consumption_core = Decimal.Parse(new_core_value) - Decimal.Parse(before_core_value);
                var energy_consumption_package = Decimal.Parse(new_package_value) - Decimal.Parse(before_package_value);
                // Time in ticks.
                var time_elapsed = after_time - before_time;
                tuples.Add((energy_consumption_core, energy_consumption_package, time_elapsed, iteration_counter));

            }

            if(currentResults != null)
                results.AddRange(currentResults);

            System.Console.WriteLine("Core;Package;Time;Iterations");
            foreach (var tuple in tuples) {
                System.Console.WriteLine(tuple.Item1 + ";" + tuple.Item2 + ";" + tuple.Item3 + ";" + tuple.Item4);
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