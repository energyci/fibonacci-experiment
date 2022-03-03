using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace StringLibraryTest
{

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

        private Decimal rapl_value;
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
            var results = new List<TestResult>();
            rapl_value = read_rapl_value();
            var x = new RAPLPackage("/sys/devices/virtual/powercap/intel-rapl/intel-rapl:0");
            for (int count = 0; count < this.stabilityThreshold; count++)
            {
                var currentResults = base.Execute(testMethod);
                results.AddRange(currentResults);
                Console.WriteLine(count);
            }
            Decimal new_value = read_rapl_value();
            Console.WriteLine("Energy usage: {0}", new_value - rapl_value);
            return results.ToArray();
        }
    }
}