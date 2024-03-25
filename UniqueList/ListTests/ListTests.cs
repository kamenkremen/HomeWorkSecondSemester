using Lists;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace ListTests;

public class Tests
{
    private static class Comparator<TData>
    {
        public static bool IsEqual(Lists.List<TData> list, TData[] array)
        {
            if (list.Size != array.Length)
            {
                return false;
            }

            for (int i = 0; i < list.Size; ++i) 
            {
                if (!EqualityComparer<TData>.Default.Equals(list[i], array[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }

    static IEnumerable<Lists.List<int>> IntLists()
    {
        yield return new Lists.List<int> ();
        yield return new Lists.UniqueList<int> ();
    }

    static IEnumerable<Lists.List<string>> StringLists()
    {
        yield return new Lists.List<string> ();
        yield return new Lists.UniqueList<string> ();
    }

    [TestCaseSource(nameof(IntLists))]
    public void TestIntListAddAndSet(Lists.List<int> list)
    {
        int[] expected = [0, 1, 2, 3, 4, 5];
        for (int i = 0; i <= 4; ++i)
        {
            list.Add(i);
        }

        list.Add(6);
        list[5] = 5;
        if (!Comparator<int>.IsEqual(list, expected))
        {
            Assert.Fail();
        }

        Assert.Pass();
    }

    [TestCaseSource(nameof(IntLists))]
    public void TestIntListRemove(Lists.List<int> list)
    {
        int[] expected = [0, 1, 2, 3, 4, 5];
        for (int i = 0; i <= 8; ++i)
        {
            list.Add(i);
        }

        list.RemoveByValue(8);
        list.RemoveByValue(7);
        list.Remove(6);
        if (!Comparator<int>.IsEqual(list, expected))
        {
            Assert.Fail();
        }

        Assert.Pass();
    }

    [TestCaseSource(nameof(StringLists))]
    public void TestStringListAddAndSet(Lists.List<string> list)
    {
        string[] expected = ["0", "1", "2", "3", "4", "5"];
        for (int i = 0; i <= 4; ++i)
        {
            list.Add(i.ToString());
        }

        list.Add("6");
        list[5] = "5";
        if (!Comparator<string>.IsEqual(list, expected))
        {
            Assert.Fail();
        }

        Assert.Pass();
    }

    [TestCaseSource(nameof(StringLists))]
    public void TestStringListRemove(Lists.List<string> list)
    {
        string[] expected = ["0", "1", "2", "3", "4", "5"];
        for (int i = 0; i <= 8; ++i)
        {
            list.Add(i.ToString());
        }

        list.RemoveByValue("8");
        list.RemoveByValue("7");
        list.Remove(6);
        if (!Comparator<string>.IsEqual(list, expected))
        {
            Assert.Fail();
        }

        Assert.Pass();
    }

    [TestCaseSource(nameof(IntLists))]
    public void TestIntListRemoveShouldFailWithRemovingNotExistingElementException(Lists.List<int> list)
    {
        for (int i = 0; i <= 8; ++i)
        {
            list.Add(i);
        }

        try
        {
            list.RemoveByValue(9);
        }
        catch (RemovingNotExistingElementException)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    [TestCaseSource(nameof(IntLists))]
    public void TestIntListRemoveShouldFailWithIndexOutOfRangeException(Lists.List<int> list)
    {
        for (int i = 0; i <= 8; ++i)
        {
            list.Add(i);
        }

        try
        {
            list.Remove(100);
        }
        catch (IndexOutOfRangeException)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }
    
    [Test]
    public void TestIntListAddShouldFailWithAddingOrSettingExistingValueException()
    {
        Lists.UniqueList<int> list = new ();
        for (int i = 0; i <= 8; ++i)
        {
            list.Add(i);
        }

        try
        {
            list.Add(2);
        }
        catch (AddingOrSettingExistingValueException)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void TestIntListSetShouldFailWithAddingOrSettingExistingValueException()
    {
        Lists.UniqueList<int> list = new ();
        for (int i = 0; i <= 8; ++i)
        {
            list.Add(i);
        }

        try
        {
            list[3] = 2;
        }
        catch (AddingOrSettingExistingValueException)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }
}
