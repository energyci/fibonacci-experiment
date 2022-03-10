using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;

namespace StringLibraryTest;

[TestClass]
public class UnitTest1
{
    [IterativeTestMethod(25)]
    public void TestGetFib41()
    {
        Assert.AreEqual(FibLibrary.GetFib(41), 102334155);
    }
    [IterativeTestMethod(25)]
    public void TestGetFib42()
    {
        Assert.AreEqual(FibLibrary.GetFib(42), 165580141);
    }
    [IterativeTestMethod(25)]
    public void TestGetFib43()
    {
        Assert.AreEqual(FibLibrary.GetFib(43), 267914296);
    }
}