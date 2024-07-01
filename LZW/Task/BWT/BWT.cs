namespace BWT;

/// <summary>
/// Class, that makes Burrows-Wheeler transformation and reverses it.
/// </summary>
public static class BWT
{
    /// <summary>
    /// Applies BWT to a array of bytes.
    /// </summary>
    /// <param name="bytes">Array of bytes, to which BWT is going to be applied.</param>
    /// <returns>Array of bytes with applied BWT to it and number of the original array of bytes.</returns>
    public static (byte[], int) ForwardBWT(byte[] bytes)
    {
        var suffixArray = BuildSuffixArray(bytes);
        var resultArray = new List<byte>();
        var originalStringNumber = 0;
        for (var i = 0; i < suffixArray.Count; ++i)
        {
            if (suffixArray[i] == 0)
            {
                originalStringNumber = i;
                resultArray.Add(bytes[^1]);
            }
            else
            {
                resultArray.Add(bytes[suffixArray[i] - 1]);
            }
        }

        return (resultArray.ToArray(), originalStringNumber + 1);
    }

    /// <summary>
    /// Reverses BWT.
    /// </summary>
    /// <param name="bytes">Array of bytes after BWT.</param>
    /// <param name="stringNumber">Original array of bytes number.</param>
    /// <returns>Array of bytes with reversed BWT.</returns>
    public static byte[] ReverseBWT(byte[] bytes, int stringNumber)
    {
        var shifts = Enumerable.Range(0, bytes.Length).ToArray();

        Array.Sort(shifts, (int a, int b) =>
        {
            if (bytes[a] < bytes[b])
            {
                return -1;
            }

            return bytes[a] > bytes[b] ? 1 : a.CompareTo(b);
        });
        var result = new List<byte>();
        var currentIndex = shifts[stringNumber - 1];
        for (var i = 0; i < bytes.Length; ++i)
        {
            result.Add(bytes[currentIndex]);
            currentIndex = shifts[currentIndex];
        }

        return result.ToArray();
    }

    private static void CountingSort(ref List<int> array, List<int> equalityClasses)
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

    private static List<int> BuildSuffixArray(byte[] suffixArrayString)
    {
        int length = suffixArrayString.Length;
        var suffixArray = new List<int>();
        var equalityClasses = new List<int>();
        {
            var sortingArray = new List<(int, int)>();
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
}
