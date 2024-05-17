namespace Tests;
using BubbleSort;
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
                if (symbol == 'a' || symbol == 'A')
                {
                    ++countFirst;
                }
            }
            
            foreach (var symbol in second)
            {
                if (symbol == 'a' || symbol != 'A')
                {
                    ++countSecond;
                }
            }

            if (countFirst > countSecond)
            {
                return 1;
            }

            if (countSecond > countFirst)
            {
                return -1;
            }

            return 0;
        }
    }

    public class IntComparer : IComparer<int>
    {
        public int Compare(int first, int second)
        {
            if (first > second)
            {
                return -1;
            }

            if (second > first)
            {
                return 1;
            }

            return 0;
        }
    }

    [Test]
    public void BubbleSortStandartIntComparerShouldSort1()
    {
        var list = new List<int> {5, 2, 4, 1, 2};
        var comparer = new IntComparer();
        var sortedByBubbleSortList = BubbleSort.Sort<int>(list, comparer);
        list.Sort(comparer);
        Assert.That(list, Is.EqualTo(sortedByBubbleSortList));
    }

    [Test]
    public void BubbleSortAStringComparerShouldSort1()
    {
        var strings = new List<string>() {"aaaaaa", "bbbbb", "ababcsbacbbbb", "AAAAAAAAAAAAAA"};
        var comparer = new AStringComparer();
        var sortedByBubbleSortList = BubbleSort.Sort<string>(strings, comparer);
        strings.Sort(comparer);
        Assert.That(strings, Is.EqualTo(sortedByBubbleSortList));
    }

    [Test]
    public void BubbleSortAStringComparerShouldSort2()
    {
        var strings = new List<string>() {"bvncn", "zxcbzxmncb", "z;lxjclxcjlzx"};
        var comparer = new AStringComparer();
        var sortedByBubbleSortList = BubbleSort.Sort<string>(strings, comparer);
        strings.Sort(comparer);
        Assert.That(strings, Is.EqualTo(sortedByBubbleSortList));
    }

}
