using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace StringLibraryTest
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class IterativeTestMethodAttribute : TestMethodAttribute
{
    private int stabilityThreshold;

    public IterativeTestMethodAttribute(int stabilityThreshold)
    {
        this.stabilityThreshold = stabilityThreshold;
    }

    public override TestResult[] Execute(ITestMethod testMethod) 
    {
        var results = new List<TestResult>();
        for(int count = 0; count < this.stabilityThreshold; count++)
        {
            var currentResults = base.Execute(testMethod);
            results.AddRange(currentResults);
            Console.WriteLine(count);
        }

        return results.ToArray();
    }
}
}