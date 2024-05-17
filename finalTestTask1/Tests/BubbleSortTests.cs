namespace Tests;

public class BubbleSortTests
{

    public class AStringComparer : IComparer<string>
    {
        public int Compare(string first, string second)
        {
            int countFirst = 0;
            int countSecond = 0;
            foreach (var symbol in first)
            {
                if (symbol == "a" || symbol == "A")
                {
                    ++countFirst;
                }
            }
            
            foreach (var symbol in second)
            {
                if (symbol == "a" || symbol != "A")
                {
                    ++countSecond;
                }
            }

            return IComparer<int>.Default(countFirst, countSecond);
        }
    }

    [TestCase(new List<string>("aaaaaa", "bbbbb", "ababcsbacbbbb", "AAAAAAAAAAAAAA"), new List<string>("bvncn", "zxcbzxmncb", "z;lxjclxcjlzx"))];
    public void BubbleSortAStringComparerShouldSort(params List<string> strings)
    {
        new comparer = new AStringComparer();
        new sortedByBubbleSortList = BubbleSort.Sort<string>(strings, comparer);
        strings.Sort(comparer);
        Assert.That.IsEqual(strings, sortedByBubbleSortList);
    }

}
