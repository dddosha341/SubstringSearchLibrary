using SubstringSearchLibrary.Algorithms;

namespace SearchAlgorithmsUnitTests;

public class BoyerMooreAlgorithmTest
{
    private BoyerMooreAlgorithm _searcher;

    [SetUp]
    public void Setup()
    {
        _searcher = new BoyerMooreAlgorithm();
    }

    [Test]
    public void Search_FindsSingleOccurrence()
    {
        var result = _searcher.IndexesOf("abcxabcdabxabcdabcdabcy", "abcdabcy");
        Assert.That(result, Is.EquivalentTo(new[] { 15 }));
    }

    [Test]
    public void Search_FindsMultipleOccurrences()
    {
        var result = _searcher.IndexesOf("ababcabcabababd", "ab");
        Assert.That(result, Is.EquivalentTo(new[] { 0, 2, 5, 8, 10, 12 }));
    }

    [Test]
    public void Search_ReturnsEmpty_WhenNoMatch()
    {
        var result = _searcher.IndexesOf("abcdefgh", "xyz");
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Search_FindsPatternAtStart()
    {
        var result = _searcher.IndexesOf("patterntext", "pattern");
        Assert.That(result, Is.EquivalentTo(new[] { 0 }));
    }

    [Test]
    public void Search_FindsPatternAtEnd()
    {
        var result = _searcher.IndexesOf("textpattern", "pattern");
        Assert.That(result, Is.EquivalentTo(new[] { 4 }));
    }

    [Test]
    public void Search_WhenPatternIsWholeText()
    {
        var result = _searcher.IndexesOf("hello", "hello");
        Assert.That(result, Is.EquivalentTo(new[] { 0 }));
    }

    [Test]
    public void Search_WhenTextIsEmpty()
    {
        var result = _searcher.IndexesOf("", "pattern");
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Search_WhenPatternIsEmpty()
    {
        var result = _searcher.IndexesOf("text", "");
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Search_WhenPatternLongerThanText()
    {
        var result = _searcher.IndexesOf("short", "verylongpattern");
        Assert.That(result, Is.Empty);
    }
}