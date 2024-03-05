using System;
using System.Text;

void CountingSort(ref List<int> array, List<int> equalityClasses)
{
    int size = array.Count;
    var count = new List<int>();
    var position = new List<int>();
    var newArray = new List<int>();
    for (int i = 0; i < size; ++i)
    {
        position.Add(0);
        count.Add(0);
        newArray.Add(0);
    }

    foreach (var equalityClass in equalityClasses)
    {
        ++count[equalityClass];
    }

    for (int i = 1; i < size; ++i)
    {
        position[i] = position[i - 1] + count[i - 1];
    }

    foreach (var element in array)
    {
        newArray[position[equalityClasses[element]]++] = element;
    }

    array = newArray;
}

List<int> BuildSuffixArray(string suffixArrayString)
{
    int length = suffixArrayString.Length;
    var suffixArray = new List<int>();
    var equalityClasses = new List<int>();
    {
        var sortingArray = new List<(char, int)>();
        for (var i = 0; i < length; ++i)
        {
            sortingArray.Add((suffixArrayString[i], i));
        }

        sortingArray.Sort();
        for (var i = 0; i < length; ++i)
        {
            suffixArray.Add(sortingArray[i].Item2);
        }
    }

    for (var i = 0; i < length; ++i)
    {
        equalityClasses.Add(0);
    }

    for (var i = 1; i < length; ++i)
    {
        equalityClasses[suffixArray[i]] = suffixArrayString[suffixArray[i]] != suffixArrayString[suffixArray[i - 1]] ?
            equalityClasses[suffixArray[i - 1]] + 1 : equalityClasses[suffixArray[i - 1]];
    }

    for (var substringLength = 1; substringLength < length; substringLength <<= 1)
    {
        for (var i = 0; i < length; ++i)
        {
            suffixArray[i] = (suffixArray[i] - substringLength + length) % length;
        }

        CountingSort(ref suffixArray, equalityClasses);
        var newEqualityClasses = new List<int>();
        for (int i = 0; i < length; ++i)
        {
            newEqualityClasses.Add(0);
        }

        for (int i = 1; i < length; ++i)
        {
            var previous = (equalityClasses[suffixArray[i - 1]],
                equalityClasses[(suffixArray[i - 1] + substringLength) % length]);
            var current = (equalityClasses[suffixArray[i]],
                equalityClasses[(suffixArray[i] + substringLength) % length]);
            newEqualityClasses[suffixArray[i]] = previous != current ?
                newEqualityClasses[suffixArray[i - 1]] + 1 : newEqualityClasses[suffixArray[i - 1]];
        }

        equalityClasses = newEqualityClasses;
    }

    return suffixArray;
}

(string, int) BWT(string str)
{
    var suffixArray = BuildSuffixArray(str);
    string resultString = string.Empty;
    int stringNumber = 0;
    for (var i = 0; i < suffixArray.Count; ++i)
    {
        if (suffixArray[i] == 0)
        {
            stringNumber = i;
            resultString += str[^1];
        }
        else
        {
            resultString += str[suffixArray[i] - 1];
        }
    }

    return (resultString, stringNumber + 1);
}

string ReverseBWT(string str, int stringNumber)
{
    var shifts = new List<int>();
    for (var i = 0; i < str.Length; ++i)
    {
        shifts.Add(i);
    }

    shifts.Sort((int a, int b) => str[a].CompareTo(str[b]));
    var result = string.Empty;
    var currentIndex = shifts[stringNumber - 1];
    for (var i = 0; i < str.Length; ++i)
    {
        result += str[currentIndex];
        currentIndex = shifts[currentIndex];
    }

    return result;
}

bool BWTTest()
{
    if (BWT("ABACABA") != ("BCABAAA", 3))
    {
        Console.WriteLine("Error in BWT test, case 1");
        return false;
    }

    if (BWT("TEXTUEL") != ("UTELXTE", 4))
    {
        Console.WriteLine("Error in BWT test, case 2");
        return false;
    }

    return true;
}

bool ReverseBWTTest()
{
    if (ReverseBWT("BCABAAA", 3) != "ABACABA")
    {
        Console.WriteLine("Error in reverse BWT test, case 1");
        return false;
    }

    if (ReverseBWT("UTELXTE", 4) != "TEXTUEL")
    {
        Console.WriteLine("Error in reverse BWT test, case 2");
        return false;
    }

    return true;
}

bool Test()
{
    return BWTTest() && ReverseBWTTest();
}

if (!Test())
{
    return 1;
}

Console.WriteLine("Type 1 to make Burrows-Wheeler transformation or any other number to reverse it");

int option = Convert.ToInt32(Console.ReadLine());
switch (option)
{
    case 1:
        Console.WriteLine("Enter your string:");
        var str = Console.ReadLine();
        var (resultString, number) = BWT(str);
        Console.WriteLine($"Resulted string: {resultString}\nIndex of your string: {number}");
        break;
    default:
        Console.WriteLine("Enter string after BWT and string index");
        var input = Console.ReadLine();
        var inputSplitted = input.Split(' ');
        var stringNumber = 0;
        if (inputSplitted.Length != 2 || !int.TryParse(inputSplitted[1], out stringNumber) ||
            stringNumber <= 0 || stringNumber > inputSplitted[0].Length)
        {
            Console.WriteLine("Incorrect input");
            return 2;
        }

        Console.WriteLine($"Your string after reversing BWT: {ReverseBWT(inputSplitted[0], stringNumber)}");
        break;
}

return 0;
