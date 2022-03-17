using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace StringLibraryTest
{

    public class RAPLTestResult : TestResult {

        public Decimal RAPLValue { get; }
        public RAPLTestResult(Decimal RAPLValue, TestResult data) : base(){
            this.RAPLValue = RAPLValue;
            this.DisplayName = data.DisplayName;
            this.Outcome = data.Outcome;
            this.TestFailureException = data.TestFailureException;
            this.LogOutput = data.LogOutput;
            this.LogError = data.LogError;
            this.DebugTrace = data.DebugTrace;
            this.TestContextMessages = data.TestContextMessages;
            this.ExecutionId = data.ExecutionId;
            this.ParentExecId = data.ParentExecId;
            this.InnerResultsCount = data.InnerResultsCount;
            this.Duration = data.Duration;
            this.DatarowIndex = data.DatarowIndex;
            this.ReturnValue = data.ReturnValue;
            this.ResultFiles = data.ResultFiles;

        }
    }

    public class RAPLPackage { 

        public String Name { get; }
        public Decimal Counter { get; }
        public Dictionary<String, Decimal> ZoneValues { get; }
        private void ReadRAPLZone(String file_path) {
            String name = System.IO.File.ReadAllText(Path.Join(file_path, "name"));
            Decimal value = Decimal.Parse(System.IO.File.ReadAllText(Path.Join(file_path, "energy_uj")));
            ZoneValues[name] = value;
        }

        // file_path = "/sys/devices/virtual/powercap/intel-rapl/intel-rapl:0"

        public RAPLPackage(String file_path) {
            ZoneValues = new Dictionary<string, decimal>();
            var x = Directory.GetDirectories(file_path, "intel-rapl*");
            foreach(var y in x ) {
                ReadRAPLZone(y);
            }
            Name = System.IO.File.ReadAllText(Path.Join(file_path, "name"));
            Counter = Decimal.Parse(System.IO.File.ReadAllText(Path.Join(file_path, "energy_uj")));

            
            Console.WriteLine("Name: {0}, Energy_uj: {1}", Name, Counter);
            foreach(var item in ZoneValues) {
                Console.WriteLine("  Name: {0}, Value: {1}", item.Key, item.Value);
            }
        }
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IterativeTestMethodAttribute : TestMethodAttribute
    {
        private int stabilityThreshold;
        private const string FILE_PATH = "/sys/devices/virtual/powercap/intel-rapl/intel-rapl:0/energy_uj";

        private Decimal read_rapl_value() {
            string raw_value = System.IO.File.ReadAllText(FILE_PATH);

            return Decimal.Parse(raw_value);
        }

        public IterativeTestMethodAttribute(int stabilityThreshold)
        {
            this.stabilityThreshold = stabilityThreshold;
        }

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var results = new List<RAPLTestResult>();
            Console.WriteLine("Iteration;RAPL-Value;Old-RAPL;New-RAPL;Ticks");
            for (int count = 0; count < this.stabilityThreshold; count++)
            {
                Decimal before_value = read_rapl_value();
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var currentResults = base.Execute(testMethod);

                Decimal new_value = read_rapl_value();
                watch.Stop();
                //Thread.Sleep(1000);
                foreach(var currentResult in currentResults) {
                    RAPLTestResult current = new RAPLTestResult(new_value-before_value, currentResult);
                    results.Add(current);
                }
                Console.WriteLine("{0};{1};{2};{3};{4}", count, new_value-before_value, before_value, new_value, watch.ElapsedTicks);

                
                //Console.WriteLine("Iteration: {0} RAPL-Value: {1}, Old: {2}, New: {3}, Time: {4}", count, new_value-before_value, before_value, new_value, watch.ElapsedTicks);
            }
            var ticks_per_seconds = (decimal) 1 / Stopwatch.Frequency;
            var ticks_per_milliseconds = (decimal) 1 / Stopwatch.Frequency * 1000;
            var ticks_per_nanoseconds = (decimal) 1 / Stopwatch.Frequency * 1000000000;
            Console.WriteLine("Sec per tick: {0}, ms per tick: {1}, ns per tick: {2}", ticks_per_seconds, ticks_per_milliseconds, ticks_per_nanoseconds);
            System.Console.WriteLine("Frequency: {0}", System.Diagnostics.Stopwatch.Frequency);
            return results.ToArray();
        }
    }
}