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

namespace SparseVector;

/// <summary>
/// Class that represents vector which contains many zeros.
/// </summary>
public class SparseVector
{
    private readonly int size = 0;

    private Dictionary<int, int> vector = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="SparseVector"/> class.
    /// </summary>
    /// <param name="original">Original instance of sparse vector, from which size and elements will be copied.</param>
    public SparseVector(SparseVector original)
    {
        this.size = original.Size;
        this.vector = new (original.vector);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SparseVector"/> class.
    /// </summary>
    /// <param name="size">Size of the vector.</param>
    public SparseVector(int size)
    {
        this.size = size;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SparseVector"/> class.
    /// </summary>
    /// <param name="vector">List, that contains pairs of (index, number) that is going to be contained in sparse vector.</param>
    /// <param name="size">Size of the sparse vector.</param>
    public SparseVector(List<(int, int)> vector, int size)
    {
        this.size = size;
        foreach (var element in vector)
        {
            if (element.Item1 >= size)
            {
                throw new IndexOutOfRangeException();
            }

            if (element.Item2 == 0)
            {
                continue;
            }

            if (this.vector.ContainsKey(element.Item1))
            {
                throw new ArgumentException("Initializer vector contained same index more than once.");
            }

            this.vector.Add(element.Item1, element.Item2);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SparseVector"/> class.
    /// </summary>
    /// <param name="givenVector">Vector with numbers that is going to be added to sparse vector.</param>
    /// <param name="size">Size of the vector.</param>
    /// <exception cref="IndexOutOfRangeException">Being thrown if given vector`s size is bigger than size of sparse vector. </exception>
    public SparseVector(List<int> givenVector, int size)
    {
        if (givenVector.Count > size)
        {
            throw new IndexOutOfRangeException();
        }

        this.size = size;
        for (int i = 0; i < givenVector.Count; i++)
        {
            if (givenVector[i] == 0)
            {
                continue;
            }

            this.vector.Add(i, givenVector[i]);
        }
    }

    /// <summary>
    /// Gets size of the vector.
    /// </summary>
    public int Size { get => this.size; }

    /// <summary>
    /// Gets a value indicating whether vector is empty.
    /// </summary>
    public bool IsNull { get => this.vector.Count == 0; }

    /// <summary>
    /// Gets or sets value by index.
    /// </summary>
    /// <param name="index">Index of the element in sparse vector.</param>
    /// <returns>Value contained by the sparse vector by given index.</returns>
    /// <exception cref="IndexOutOfRangeException">Throws if given index is greater than size of the sparse vector.</exception>
    public int this[int index]
    {
        get
        {
            if (index >= this.size)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            if (this.vector.ContainsKey(index))
            {
                return this.vector[index];
            }
            else
            {
                return 0;
            }
        }

        set
        {
            if (index >= this.size)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            if (this.vector.ContainsKey(index))
            {
                this.vector[index] = value;
                if (value == 0)
                {
                    this.vector.Remove(index);
                }
            }
            else if (value != 0)
            {
                this.vector.Add(index, value);
            }
        }
    }

    /// <summary>
    /// Concatenates this vector and other one with the same size.
    /// </summary>
    /// <param name="other">Vector, to be concatenated with.</param>
    /// <returns>Result of concatenation of two vectors.</returns>
    /// <exception cref="VectorsWithDifferentSizeException">Throws if vectors have different sizes.</exception>
    public SparseVector Concatenate(SparseVector other)
    {
        if (other.Size != this.Size)
        {
            throw new VectorsWithDifferentSizeException();
        }

        return this.Apply(other, (int first, int second) => first + second);
    }

    /// <summary>
    /// Subtract other vector from this vector.
    /// </summary>
    /// <param name="other">Vector that is going to be subtracted..</param>
    /// <returns>Result of subtraction.</returns>
    /// <exception cref="VectorsWithDifferentSizeException">Throws if vectors have different sizes.</exception>
    public SparseVector Subtract(SparseVector other)
    {
        if (other.Size != this.Size)
        {
            throw new VectorsWithDifferentSizeException();
        }

        return this.Apply(other, (int first, int second) => first - second);
    }

    /// <summary>
    /// Applies scalar multiplication ti this vector and other one with the same size.
    /// </summary>
    /// <param name="other">Vector, to which this vector is going to be multiplicated.</param>
    /// <returns>Result of scalar multiplication between two vectors.</returns>
    /// <exception cref="VectorsWithDifferentSizeException">Throws if vectors have different sizes.</exception>
    public int ScalarMultiplication(SparseVector other)
    {
        if (other.Size != this.Size)
        {
            throw new VectorsWithDifferentSizeException();
        }

        int result = 0;
        foreach (var element in this.vector)
        {
            if (other[element.Key] != 0)
            {
                result += other[element.Key] * this[element.Key];
            }
        }

        return result;
    }

    private SparseVector Apply(SparseVector other, Func<int, int, int> function)
    {
        SparseVector newVector = new (other);
        foreach (var element in this.vector)
        {
            newVector[element.Key] = function(element.Value, newVector[element.Key]);
        }

        return newVector;
    }
}
