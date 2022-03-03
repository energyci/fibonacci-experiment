using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;

namespace StringLibraryTest;

[TestClass]
public class UnitTest1
{
    [IterativeTestMethod(25)]
    public void TestGetFib42()
    {
        Assert.AreEqual(FibLibrary.GetFib(42), 165580141);
    }
}