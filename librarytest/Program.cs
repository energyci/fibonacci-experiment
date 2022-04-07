using Newtonsoft.Json;

namespace StringLibraryTest;

class Program
{
    private const string FILE_PATH = "/sys/devices/virtual/powercap/intel-rapl/intel-rapl:0/energy_uj";

    private static Decimal read_rapl_value() {
        string raw_value = System.IO.File.ReadAllText(FILE_PATH);

        return Decimal.Parse(raw_value);
    }

    public static string SerializeIntegers()
        {
            StringWriter sw = new StringWriter();
            JsonTextWriter jsonTextWriter = new JsonTextWriter(sw);
            for (int i = 0; i < 10000; i++)
            {
                jsonTextWriter.WriteValue(i);
            }
            jsonTextWriter.Flush();

            return sw.ToString();
        }


    static void Main2(string[] args){
        string json = "{'Name': 'Phillip', 'Age': 25, 'HasCar' : true}";

        System.Console.WriteLine(SerializeIntegers());

        JsonTextReader reader = new JsonTextReader(new StringReader(json));
        while(reader.Read()){
            if (reader.Value != null){
                System.Console.WriteLine("Token: {0}, Value {1}", reader.TokenType, reader.Value);
            }
            else {
                System.Console.WriteLine("Token: {0}", reader.TokenType);
            }

        }

        /* int fibval = Int32.Parse(args[0]);
        Console.WriteLine(FibLibrary.GetFib(fibval));
        foreach(var x in args){
            Console.WriteLine(x);
        } */
        //GetFib(int args.)
    }
    
}