using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;

namespace StringLibraryTest;

[TestClass]
public class UnitTest1
{
    [IterativeTestMethod(25)]
    public void TestGetFib41()
    {
        Assert.AreEqual(FibLibrary.GetFib(40), 102334155);
    }
    [IterativeTestMethod(25)]
    public void TestGetFib42()
    {
        Assert.AreEqual(FibLibrary.GetFib(41), 165580141);
    }
    [IterativeTestMethod(25)]
    public void TestGetFib43()
    {
        Assert.AreEqual(FibLibrary.GetFib(42), 267914296);
    }
    [IterativeTestMethod(25)]
    public void TestSlowGetFib43()
    {
        Assert.AreEqual(FibLibrary.SlowGetFib(42), 267914296);
    }

    

    [IterativeTestMethod(25)]
    [DeploymentItem(@"test.csv", "optionalOutFolder")]
    public void TestMathSqrt() {
        using (TextFieldParser parser = new TextFieldParser("test.csv")) {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while(!parser.EndOfData) {
                string[] fields = parser.ReadFields();
                var input_number = Double.Parse(fields[0]);
                var output_number = Double.Parse(fields[1]);
                Assert.AreEqual(output_number, Math.Sqrt(input_number));
            }
        }
    }
}