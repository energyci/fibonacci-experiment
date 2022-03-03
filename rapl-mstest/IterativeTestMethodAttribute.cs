namespace rapl_mstest;
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
        }

        return results.ToArray();
    }
}