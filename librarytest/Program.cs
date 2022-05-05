using Newtonsoft.Json;

namespace StringLibraryTest;

class Program
{
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
    
}