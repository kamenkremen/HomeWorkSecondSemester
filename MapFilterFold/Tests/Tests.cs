namespace Tests;

public class Tests
{

    [Test]
    public void MapIntTestShouldReturnExpected()
    {
        var list = new List<int> {1, 2, 3, 4, 5};
        var expected = new List<int> {0, 1, 2, 3, 4};
        var function = (int n) => n - 1;
        Assert.That(Functions.Map(list, function), Is.EqualTo(expected));
    }

    [Test]
    public void MapStringTestShouldReturnExpected()
    {
        var list = new List<string> {"11", "20", "34", "45", "5123"};
        var expected = new List<string> {"1", "2", "3", "4", "5"};
        var function = (string s) => s[..1];
        Assert.That(Functions.Map(list, function), Is.EqualTo(expected));
    }

    [Test]
    public void MapTestShouldReturnEmptyList()
    {
        Assert.That(Functions.Map([], (int i) => i), Is.Empty);
    }

    [Test]
    public void FilterTestIntReturnExpected()
    {
        var list = new List<int> {1, 2, 3, 4};
        var expected = new List<int> {1};
        var function = (int n) => n == 1;
        Assert.That(Functions.Filter(list, function), Is.EqualTo(expected));
    }

    [Test]
    public void FilterTestStringReturnExpected()
    {
        var list = new List<string> {"aba", "b", "bab"};
        var expected = new List<string> {"aba", "bab"};
        var function = (string s) => s.Length == 3;
        Assert.That(Functions.Filter(list, function), Is.EqualTo(expected));
    }

    [Test]
    public void FoldSumOfIntsReturnsExpected()
    {
        var list = new List<int> {1, 2, 3, 4, 5};
        var function = (int a, int v) => a + v;
        Assert.That(Functions.Fold(list, function, 0) == 15); 
    }

    [Test]
    public void FoldSumOfStringsReturnsExpected()
    {
        var list = new List<string> {"aba", "2", "bab"};
        var function = (string a, string v) => a + v;
        Assert.That(Functions.Fold(list, function, string.Empty) == "aba2bab"); 
    }
}