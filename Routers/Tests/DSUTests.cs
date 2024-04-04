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

public class DSUTests
{
    [Test]
    public void TestShouldThrowException()
    {
        var dsu = new Utilities.DSU(5);
        try 
        {
            dsu.Unite(5, 6);
        }
        catch (IndexOutOfRangeException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void TestShouldWorkCorrectly()
    {
        var dsu = new Utilities.DSU(5);
        dsu.Unite(3, 4);
        dsu.Unite(2, 3);
        for (int i = 3; i <= 4; ++i)
        {
            if (dsu.GetParent(i) != dsu.GetParent(2))
            {
                Assert.Fail();
            }
        }
        if (dsu.GetParent(0) != 0 || dsu.GetParent(1) != 1)
        {
            Assert.Fail();
        }
    }
}