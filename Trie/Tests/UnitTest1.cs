namespace Tests;

public class TrieTests
{
    [TestCase("Test1", "Test2", "Test3", "test4", "5tset")]
    public void TrieAddTest(params string[] strings)
    {
        Trie trie = new Trie();
        foreach (string str in strings)
        {
            if (!trie.Add(str))
            {
                Assert.Fail();
            }
        }
    }

    [TestCase("Test1", "Test1", "Test1", "Test1", "Test1")]
    public void TrieAddSimilarTest(params string[] strings)
    {
        Trie trie = new Trie();
        trie.Add(strings[0]);
        foreach (string str in strings)
        {
            if (trie.Add(str))
            {
                Assert.Fail();
            }
        }
    }

    [TestCase("Test1", "Test2", "Test3", "test4", "5tset")]
    public void TrieContainsTest(params string[] strings)
    {
        Trie trie = new Trie();
        foreach (string str in strings)
        {
            trie.Add(str);
        }
        foreach(string str in strings)
        {
            if (!trie.Contains(str))
            {
                Assert.Fail();
            }
        }
        if (trie.Contains("asdinsadlkzxpcqiowesjdklsabkjfnm"))
        {
            Assert.Fail();
        }
    }

    [TestCase("Test1", "Test2", "Test3", "test4", "5tset")]
    public void TrieRemoveTest(params string[] strings)
    {
        Trie trie = new Trie();
        foreach (string str in strings)
        {
            trie.Add(str);
        }
        foreach(string str in strings)
        {
            if (!trie.Remove(str))
            {
                Assert.Fail();
            }
            if (trie.Contains(str))
            {
                Assert.Fail();
            }
        }
        if (trie.Remove("aldjsajkdjksadn"))
        {
            Assert.Fail();
        }
    }

    [TestCase(4, "Test", "Test1", "Test2", "Test3", "test4", "5tset")]
    [TestCase(1, "ASDASDasdjkhuiwyqd", "test1", "test2", "ASDASD")]
    [TestCase(5, "", "asdsad", "asdsaqwe", "zxciwaosjd", "qoirwqpori")]
    public void TrieHowManyStartsWithPrefix(int correctResult, params string[] strings)
    {
        Trie trie = new Trie();
        foreach (string str in strings)
        {
            trie.Add(str);
        }
        if (trie.HowManyStartsWithPrefix(strings[0]) != correctResult)
        {
            Assert.Fail();
        }
    }
}
