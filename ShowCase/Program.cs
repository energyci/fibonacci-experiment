﻿using UtilityLibraries;

class Program
{
    static void Main(string[] args){
            Console.WriteLine("Starting");
            Console.WriteLine(FibLibrary.SlowGetFib(42));
            Console.WriteLine("Done");
            
            /* int fibval = Int32.Parse(args[0]);
            Console.WriteLine(FibLibrary.GetFib(fibval));
            foreach(var x in args){
                Console.WriteLine(x);
            } */
            //GetFib(int args.)
        }
}