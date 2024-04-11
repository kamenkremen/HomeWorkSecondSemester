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
using SparseVector;
public class Tests
{
    [Test]
    public void TestIsNull()
    {
        SparseVector first = new (3);
        Assert.That(first.IsNull);
    }

    [Test]
    public void TestSubtractionEqualVectors()
    {
        SparseVector first = new (3);
        SparseVector second = new (3);
        for (int i = 0; i < 3; i++) 
        {
            first[i] = i;
            second[i] = i;
        }
        var result = first.Subtract(second);
        Assert.That(result.IsNull);
    }

    [Test]
    public void TestSubtactionNotEqualVectors()
    {
        SparseVector first = new (3);
        SparseVector second = new (3);
        for (int i = 0; i < 3; i++) 
        {
            first[i] = i + 1;
            second[i] = i;
        }
        var result = first.Subtract(second);
        var expected = new SparseVector(3);
        for (int i = 0; i < 3; i++) 
        {
            expected[i] = 1;
        }
        Assert.That(result.Subtract(expected).IsNull);
    }

    [Test]
    public void TestSubtractionShouldThrowException()
    {
        SparseVector first = new (5);
        SparseVector second = new (3);
        try
        {
            var result = first.Subtract(second);
        }
        catch (VectorsWithDifferentSizeException)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void TestConcatenateWithNullVector()
    {
        SparseVector first = new (3);
        SparseVector second = new (3);
        for (int i = 0; i < 3; i++) 
        {
            first[i] = i + 1;
        }
        var result = first.Concatenate(second);
        Assert.That(result.Subtract(first).IsNull);
    }

    [Test]
    public void TestConcatenateWithNotNullVector()
    {
        SparseVector first = new (3);
        SparseVector second = new (3);
        for (int i = 0; i < 3; i++) 
        {
            first[i] = i + 1;
            second[i] = i + 1;
        }
        var result = first.Concatenate(second);
        Assert.That(result.Subtract(first).Subtract(second).IsNull);
    }
    
    [Test]
    public void TestConcatenateShouldThrowException()
    {
        SparseVector first = new (5);
        SparseVector second = new (3);
        try
        {
            var result = first.Concatenate(second);
        }
        catch (VectorsWithDifferentSizeException)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void TestSettingElement()
    {
        SparseVector first = new (5);
        var expectedVector = new List<int>([1, 2, 3, 4, 5]);
        for (int i = 0; i < 5; ++i) 
        {
            first[i] = i + 1;
        }
        var expectedSparseVector = new SparseVector(expectedVector, 5);
        Assert.That(expectedSparseVector.Subtract(first).IsNull);
    }

    [Test]
    public void TestSettingElementShouldThrowException()
    {
        SparseVector first = new (5);
        try
        {
            first[7] = 7;
        }
        catch (IndexOutOfRangeException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void TestScalarMultiplicationWithNullVector()
    {
        SparseVector first = new (5);
        SparseVector second = new (5);
        for (int i = 0; i < 5; ++i) 
        {
            first[i] = i + 1;
        }
        
        Assert.That(first.ScalarMultiplication(second), Is.EqualTo(0));
    }

    [Test]
    public void TestScalarMultiplicationWithNotNullVector()
    {
        SparseVector first = new (5);
        SparseVector second = new (5);
        var expected = 0;
        for (int i = 0; i < 5; ++i) 
        {
            first[i] = i + 1;
            second[i] = i + 1;
            expected += (i + 1) * (i + 1);
        }
        
        Assert.That(first.ScalarMultiplication(second), Is.EqualTo(expected));
    }

    [Test]
    public void TestScalarMultiplicationShouldThrowException()
    {
        SparseVector first = new (5);
        SparseVector second = new (8);
        try
        {
            first.ScalarMultiplication(second);
        }
        catch (VectorsWithDifferentSizeException)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    [Test]
    public void TestCopyInitalize()
    {
        SparseVector first = new (5);
        SparseVector second = new (5);
        for (int i = 0; i < 5; i++) 
        {
            first[i] = i + 1;
            second[i] = i + 1;
        }
        var third = new SparseVector(first);
        Assert.That(third.Subtract(second).IsNull);
    }
}
