// <copyright file="BubbleSort.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// Class that contains bubble sort function.
/// </summary>
public static class BubbleSort
{
    /// <summary>
    /// Applies function to every item of list and returns a list of the results.
    /// </summary>
    /// <typeparam name="T">Type of elements in the list.</typeparam>
    /// <param name="list">List of elements which is going to be sorted.</param>
    /// <param name="comparer">Object, that can compare two objects from the list.</param>
    /// <returns>Sorted list.</returns>
    public static IList<T> Sort<T>(IList<T> list, IComparer<T> comparer)
    {
        for (int i = 0; i < list.Count; ++i)
        {
            for (int j = 1; j < list.Count; j++)
            {
                if (comparer.Compare(list[j - 1], list[j]) > 0)
                {
                    (list[j - 1], list[j]) = (list[j], list[j - 1]);
                }
            }
        }

        return list;
    }
}
