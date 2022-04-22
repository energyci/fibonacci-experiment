using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

class Program {
    private const string FILE_PATH = "/sys/class/powercap/intel-rapl/intel-rapl:0/intel-rapl:0:0/energy_uj";

    private static string read_rapl_value() {
        return System.IO.File.ReadAllText(FILE_PATH);
    }

    private static void rapl_update_interval(int measurement_time_secs) {
        List<long> measurements = new List<long>(1_000_000_000);
        var outer_watch = Stopwatch.StartNew();

        while (outer_watch.ElapsedMilliseconds < measurement_time_secs * 1000) {
            bool changed = false;
            var inital_value = read_rapl_value();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            while (!changed) {
                var new_value = read_rapl_value();
                changed = new_value != inital_value;
            }
            watch.Stop();
            measurements.Add(watch.ElapsedTicks);
        }
        System.Console.WriteLine(String.Join("\n", measurements));
    }

    private static void log_while_true_rapl(int measurement_time_secs) {

        List<(string, long)> measurements = new List<(string, long)>(1_000_000_000);

        var watch = Stopwatch.StartNew();

        while (watch.ElapsedMilliseconds < measurement_time_secs * 1000) {
            var rapl_value = read_rapl_value();
            measurements.Add((rapl_value, watch.ElapsedTicks));
        }
        foreach (var tuple in measurements) {
            System.Console.WriteLine(tuple.Item1.Replace("\n", String.Empty) + ";" + tuple.Item2);
        }

    }

    static void PrintHelp() {
        System.Console.WriteLine("Usage: SamplingRate (sampling_rate|loop_rate) <num_iteration>");
    }
    static int Main(string[] args) {
        if (args.Length < 2) {
            PrintHelp();
            return 1;
        }
        var action = args[0].ToLower();
        string[] valid_actions = { "sampling_rate", "loop_rate" };
        int num = 0;
        var is_arg1_int = int.TryParse(args[1], out num);

        if (!is_arg1_int || !valid_actions.Contains(action)) {
            PrintHelp();
            return 1;
        }
        if (action == valid_actions[0]) {
            rapl_update_interval(num);
        } else if (action == valid_actions[1]) {
            log_while_true_rapl(num);
        } else {
            System.Console.WriteLine("Bad.");
        }
        return 0;
    }

}