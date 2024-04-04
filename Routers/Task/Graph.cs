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

namespace Utilities;

/// <summary>
/// Class that represents graph.
/// </summary>
public class Graph
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Graph"/> class.
    /// </summary>
    /// <param name="numberOfVertices">Amount of vertices in graph.</param>
    public Graph(int numberOfVertices)
    {
        this.Size = numberOfVertices;
        this.VerticesList = new ();
        for (int i = 0; i < numberOfVertices; ++i)
        {
            this.VerticesList.Add(new List<(int, int)>());
        }
    }

    /// <summary>
    /// Gets amount of vertices in graph.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Gets or sets connections between vertices.
    /// </summary>
    public List<List<(int, int)>> VerticesList { get; set; }

    /// <summary>
    /// Gets or sets list of edges between vertices.
    /// </summary>
    public List<(int, int, int)> Edges { get; set; } = new ();

    /// <summary>
    /// Adds edge between two vertices to the graph.
    /// </summary>
    /// <param name="firstVertex">First vertex.</param>
    /// <param name="secondVertex">Second vertex.</param>
    /// <param name="weight">Weight of the edge.</param>
    /// <exception cref="IndexOutOfRangeException">Throws if number of vertex is greater than amount of vertices in graph.</exception>
    public void AddEdge(int firstVertex, int secondVertex, int weight)
    {
        if (secondVertex >= this.Size || firstVertex >= this.Size)
        {
            throw new IndexOutOfRangeException();
        }

        this.Edges.Add((firstVertex, secondVertex, weight));
        this.VerticesList[firstVertex].Add((secondVertex, weight));
        this.VerticesList[secondVertex].Add((firstVertex, weight));
    }

    /// <summary>
    /// Indicates whether graph is connected or not.
    /// </summary>
    /// <returns>True if graph is connected and false otherwise.</returns>
    public bool IsConnected()
    {
        var used = new List<bool>();
        for (int i = 0; i < this.Size; ++i)
        {
            used.Add(false);
        }

        this.DFS(ref used, 0);
        foreach (var vertex in used)
        {
            if (!vertex)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Gets spanning tree with greatest weights of the edges.
    /// </summary>
    /// <returns>Spanning tree with greatest weights of the edges.</returns>
    /// <exception cref="NotConnectedGraphException">Throws if graph is not connected.</exception>
    public Graph GetMaxSpanningTree()
    {
        var spanningTree = new Graph(this.Size);
        var components = new DSU(this.Size);
        this.Edges.Sort((x, y) => -x.Item3.CompareTo(y.Item3));
        foreach (var edge in this.Edges)
        {
            if (components.GetParent(edge.Item1) != components.GetParent(edge.Item2))
            {
                components.Unite(edge.Item1, edge.Item2);
                spanningTree.AddEdge(edge.Item1, edge.Item2, edge.Item3);
            }
        }

        if (!spanningTree.IsConnected())
        {
            throw new NotConnectedGraphException();
        }

        return spanningTree;
    }

    private void DFS(ref List<bool> used, int current)
    {
        used[current] = true;
        foreach (var vertex in this.VerticesList[current])
        {
            if (!used[vertex.Item1])
            {
                this.DFS(ref used, vertex.Item1);
            }
        }
    }
}
