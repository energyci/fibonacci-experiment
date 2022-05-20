using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace StringLibraryTest;

[TestClass]
public class jsonnettest {
    [IterativeTestMethod(6)]
    public void TestNested1() {
        string nestedJsonText = (new string('[', 1)) + "1" + (new string(']', 1));
        var nestedArray = JArray.Parse(nestedJsonText);
        JToken elem = nestedArray;
        for (int i = 0; i < 1; i++) {
            elem = elem[0];
        }
        var p = elem.Path;
        var expected = new System.Text.StringBuilder().Insert(0, "[0]", 1).ToString(); 
        Assert.AreEqual(expected, p);
    }

    [IterativeTestMethod(6)]
    public void TestNested100() {
        string nestedJsonText = (new string('[', 100)) + "1" + (new string(']', 100));
        var nestedArray = JArray.Parse(nestedJsonText);
        JToken elem = nestedArray;
        for (int i = 0; i < 100; i++) {
            elem = elem[0];
        }
        var p = elem.Path;
        var expected = new System.Text.StringBuilder().Insert(0, "[0]", 100).ToString(); 
        Assert.AreEqual(expected, p);
    }


    [IterativeTestMethod(6)]
    public void TestNested10k() {
        string nestedJsonText = (new string('[', 10_000)) + "1" + (new string(']', 10_000));
        var nestedArray = JArray.Parse(nestedJsonText);
        JToken elem = nestedArray;
        for (int i = 0; i < 10_000; i++) {
            elem = elem[0];
        }
        var p = elem.Path;
        var expected = new System.Text.StringBuilder().Insert(0, "[0]", 10_000).ToString(); 
        Assert.AreEqual(expected, p);
    }
}