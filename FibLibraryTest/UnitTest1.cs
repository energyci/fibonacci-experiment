using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;

namespace StringLibraryTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestGetFib21()
    {
        Assert.AreEqual(FibLibrary.GetFib(42), 165580141);
    }
}