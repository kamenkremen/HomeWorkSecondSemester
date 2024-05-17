// Copyright 2024 Ivan Zakarlyuka.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Tests;
using BubbleSort;
using System.Collections;
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
        var list = new List<int> {5, 5, 5, 5, 5};
        var comparer = new IntComparer();
        var sortedByBubbleSortList = BubbleSort.Sort<int>(list, comparer);
        list.Sort(comparer);
        Assert.That(list, Is.EqualTo(sortedByBubbleSortList));
    }

    [Test]
    public void BubbleSortStandartIntComparerShouldSort2()
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

