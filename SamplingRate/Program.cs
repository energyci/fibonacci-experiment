using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

class Program {
    private const string FILE_PATH = "/sys/class/powercap/intel-rapl/intel-rapl:0/intel-rapl:0:0/energy_uj";

    private static string read_rapl_value() {
        return System.IO.File.ReadAllText(FILE_PATH);
    }

    private static void rapl_update_interval(int measurement_cnt) {
        long[] measurements = new long[measurement_cnt];

        for (int i = 0; i < measurement_cnt; i++) {
            bool changed = false;
            var inital_value = read_rapl_value();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            while (!changed) {
                var new_value = read_rapl_value();
                changed = new_value != inital_value;
            }
            watch.Stop();
            measurements[i] = watch.ElapsedTicks;
        }
        System.Console.WriteLine(String.Join("\n", measurements));
    }

    private static void log_while_true_rapl(int update_counts) {
        int target_iteration = update_counts - 1;

        List<(string, long)> measurements = new List<(string, long)>(1_000_000_000);

        bool count_reached = false;
        int curr_iteration = 0;
        var old_rapl_value = read_rapl_value();
        var rapl_value = old_rapl_value;
        var watch = Stopwatch.StartNew();

        while (!count_reached) {
            rapl_value = read_rapl_value();
            measurements.Add((rapl_value, watch.ElapsedTicks));
            if (rapl_value != old_rapl_value) {
                old_rapl_value = rapl_value;
                curr_iteration += 1;
            }
            count_reached = curr_iteration == target_iteration;
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