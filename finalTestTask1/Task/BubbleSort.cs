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

namespace BubbleSort;

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
