class Program
{
    private const string FILE_PATH = "/sys/class/powercap/intel-rapl/intel-rapl:0/intel-rapl:0:0/energy_uj";

    private static string read_rapl_value()
    {
        return System.IO.File.ReadAllText(FILE_PATH);
    }
    static void Main(string[] args)
    {
        int measurement_cnt = 200;
        long[] measurements = new long[measurement_cnt];
        for (int i = 0; i < measurement_cnt; i++)
        {
            bool changed = false;
            var inital_value = read_rapl_value();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            while (!changed)
            {
                var new_value = read_rapl_value();
                changed = new_value != inital_value;
            }
            watch.Stop();
            measurements[i] = watch.ElapsedTicks;
        }
        System.Console.WriteLine(measurements[0]);
        Array.Sort(measurements);
        foreach (var item in measurements)
        {
            System.Console.WriteLine(item);
        }
    }

}