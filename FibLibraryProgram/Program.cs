using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System;


class Program {

    static void TestSlowGetFib42(int time_secs) {
        var tuples = new List<(decimal, long)>();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        while(watch.ElapsedMilliseconds < time_secs * 1000){
            string before_value = read_rapl_value();
            long before_time = watch.ElapsedTicks;

            FibLibrary.GetSlowFib(42);

            string new_value = read_rapl_value;
            long after_time = watch.ElapsedTicks;

            var energy_consumption = Decimal.Parse(new_value) - Decimal.Parse(before_value);
            var time_elapsed= after_time - before_time;
            tuples.Add((energy_consumption, time_elapsed));
        }

        foreach(var tuple in tuples){
            System.Console.WriteLine(tuple.Item1 + ";" + tuple.Item2);
        }
    }

    static void TestGetFib42(int time_secs) {
        var tuples = new List<(decimal, long)>();
        var watch = System.Diagnostics.Stopwatch.StartNew();
        while(watch.ElapsedMilliseconds < time_secs * 1000){
            string before_value = read_rapl_value();
            long before_time = watch.ElapsedTicks;

            FibLibrary.GetFib(42);

            string new_value = read_rapl_value;
            long after_time = watch.ElapsedTicks;

            var energy_consumption = Decimal.Parse(new_value) - Decimal.Parse(before_value);
            var time_elapsed= after_time - before_time;
            tuples.Add((energy_consumption, time_elapsed));
        }

        foreach(var tuple in tuples){
            System.Console.WriteLine(tuple.Item1 + ";" + tuple.Item2);
        }
        
    }

    static void PrintHelp() {
        System.Console.WriteLine("Usage: FibLibraryProgram (slow|fast) <time_secs>");
    }
    static int Main(string[] args) {
        if (args.Length < 2) {
            PrintHelp();
            return 1;
        }
        var action = args[0].ToLower();
        string[] valid_actions = { "slow", "fast" };
        int num = 0;
        var is_arg1_int = int.TryParse(args[1], out num);

        if (!is_arg1_int || !valid_actions.Contains(action)) {
            PrintHelp();
            return 1;
        }
        if (action == valid_actions[0]) {
            TestSlowGetFib42(num);
        } else if (action == valid_actions[1]) {
            TestGetFib42(num);
        } else {
            System.Console.WriteLine("Bad.");
        }
        return 0;
    }

}