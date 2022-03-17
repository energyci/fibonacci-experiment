using UtilityLibraries;

class Program
{
    private const string FILE_PATH = "/sys/devices/virtual/powercap/intel-rapl/intel-rapl:0/energy_uj";

    private static Decimal read_rapl_value() {
        string raw_value = System.IO.File.ReadAllText(FILE_PATH);

        return Decimal.Parse(raw_value);
    }
    static async void Main(string[] args){
        Console.WriteLine("Iteration;RAPL-Value;Old-RAPL;New-RAPL;Ticks");
        for(int count = 0; count < 25; count++){
            Decimal before_value = read_rapl_value();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Console.WriteLine(FibLibrary.SlowGetFib(42));
            
            Decimal new_value = read_rapl_value();
            watch.Stop();
            Console.WriteLine("{0};{1};{2};{3};{4}", count, new_value-before_value, before_value, new_value, watch.ElapsedTicks);
        }
        
        /* int fibval = Int32.Parse(args[0]);
        Console.WriteLine(FibLibrary.GetFib(fibval));
        foreach(var x in args){
            Console.WriteLine(x);
        } */
        //GetFib(int args.)
    }
    
}