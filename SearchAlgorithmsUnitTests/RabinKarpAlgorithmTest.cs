using SubstringSearchLibrary.Algorithms;

namespace SearchAlgorithmsUnitTests;

public class RabinKarpAlgorithmTest
{
    private RabinKarpAlgorithm _searcher;

    [SetUp]
    public void Setup()
    {
        _searcher = new RabinKarpAlgorithm();
    }

    [Test]
    public void Test1()
    {
        var result = _searcher.IndexesOf("abracadabra", "abra");
        Console.WriteLine(result);
        Assert.AreEqual(new List<int> { 0, 7 }, result);
    }

    [Test]
    public void Test2()
    {
        var result = _searcher.IndexesOf("hello world", "world").ToList();
        Assert.AreEqual(new List<int> { 6 }, result);
    }

    [Test]
    public void Test3_NoMatch()
    {
        var result = _searcher.IndexesOf("abcdef", "xyz").ToList();
        Assert.IsEmpty(result);
    }
}
