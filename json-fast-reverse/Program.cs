using System;
using Newtonsoft.Json.Linq;

namespace JsonFastReverse // Note: actual namespace depends on the project name.
{
    internal class Program2 {
        static void Main2(string[] args) {
            string nestedJsonText = (new string('[', 100_000)) + "1" + (new string(']', 100_000));
            JsonLoadSettings jls = new JsonLoadSettings();
            var nestedArray = JArray.Parse(nestedJsonText);
            Console.WriteLine("Hello World!");
            System.Console.WriteLine(nestedJsonText);
        }
    }
}