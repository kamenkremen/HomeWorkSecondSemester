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
/// <summary>
/// Class that contains 3 functions - map, filter and fold.
/// </summary>
public static class Functions
{
    /// <summary>
    /// Applies function to every item of list and returns a list of the results.
    /// </summary>
    /// <typeparam name="TOldType">Type of elements in the given list.</typeparam>
    /// <typeparam name="TNewType">Type of element that function returns.</typeparam>
    /// <param name="list">List of elements to which function will be applied to.</param>
    /// <param name="function">Function that is going to be applied to elements in list.</param>
    /// <returns>List with elements that function returned.</returns>
    public static List<TNewType> Map<TOldType, TNewType>(List<TOldType> list, Func<TOldType, TNewType> function)
    {
        List<TNewType> newList = new ();
        for (int i = 0; i < list.Count; ++i)
        {
            newList.Add(function(list[i]));
        }

        return newList;
    }

    /// <summary>
    /// Returns new list with elements to which the filter function returned true.
    /// </summary>
    /// <typeparam name="T">Type of elements in the list.</typeparam>
    /// <param name="list">Given list.</param>
    /// <param name="function">Filter function.</param>
    /// <returns>List that contains elements to which the filter function returned true.</returns>
    public static List<T> Filter<T>(List<T> list, Func<T, bool> function)
    {
        List<T> newList = new ();
        for (int i = 0; i < list.Count(); ++i)
        {
            if (function(list[i]))
            {
                newList.Add(list[i]);
            }
        }

        return newList;
    }

    /// <summary>
    /// Changes the initial value by applying given function to initial value and next element in the list.
    /// </summary>
    /// <typeparam name="T">Type of elements in the list.</typeparam>
    /// <typeparam name="TAccumulated">Type of the initial value.</typeparam>
    /// <param name="list">Given list.</param>
    /// <param name="function">Fold function.</param>
    /// <param name="initialValue">Initial value.</param>
    /// <returns>Accumulated value.</returns>
    public static TAccumulated Fold<T, TAccumulated>(List<T> list, Func<TAccumulated, T, TAccumulated> function, TAccumulated initialValue)
    {
        for (int i = 0; i < list.Count; ++i)
        {
            initialValue = function(initialValue, list[i]);
        }

        return initialValue;
    }
}
