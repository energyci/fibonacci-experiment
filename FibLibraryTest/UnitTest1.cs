using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;

namespace StringLibraryTest;

[TestClass]
public class UnitTest1 {

    [IterativeTestMethod(25)]
    public void TestGetFib42() {
        Assert.AreEqual(FibLibrary.GetFib(42), 267914296);
    }

    [IterativeTestMethod(25)]
    public void TestSlowGetFib42() {
        Assert.AreEqual(FibLibrary.SlowGetFib(42), 267914296);
    }
}