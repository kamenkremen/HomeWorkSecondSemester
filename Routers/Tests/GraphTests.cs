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

public class GraphTests
{
    [Test]
    public void TestShouldThrowExceptionIndexOutOfRange()
    {
        var graph = new Utilities.Graph(5);
        try 
        {
            graph.AddEdge(5, 6, 7);
        }
        catch (IndexOutOfRangeException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void TestShouldThrowExceptionGraphNotConnected()
    {
        var graph = new Utilities.Graph(5);
        graph.AddEdge(3, 4, 5);
        try 
        {
            graph.GetMaxSpanningTree();
        }
        catch (NotConnectedGraphException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void TestShouldWorkCorrectly()
    {
        var graph = new Utilities.Graph(3);
        graph.AddEdge(0, 1, 10);
        graph.AddEdge(0, 2, 5);
        graph.AddEdge(1, 2, 1);
        var expectedTree = new Utilities.Graph(3);
        expectedTree.AddEdge(0, 1, 10);
        expectedTree.AddEdge(0, 2, 5);
        var resultedTree = graph.GetMaxSpanningTree();
        Assert.That(resultedTree.Edges, Is.EqualTo(expectedTree.Edges));
    }
}
