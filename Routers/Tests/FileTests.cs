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

using Utilities;

namespace Tests;

public class FileTests
{
    [Test]
    public void TestShouldThrowException()
    {
        try
        {
            Parser.GetGraph("../../../invalidFileTest.txt");
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void TestShouldWorkCorrectly()
    {
        Writer.WriteGraph(Parser.GetGraph("../../../inputFileTest.txt").GetMaxSpanningTree(), "../../../outputFileTest.txt");
        if (File.ReadAllText("../../../outputFileTest.txt") != File.ReadAllText("../../../expectedOutput.txt"))
        {
            Assert.Fail();
        }
    }
}

