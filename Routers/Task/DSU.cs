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
/// Data structure disjoint set union, that represents disjoint sets and allow to unite them.
/// </summary>
public class DSU
{
    private List<int> parent = new ();
    private List<int> size = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="DSU"/> class.
    /// </summary>
    /// <param name="amountOfVertices">Amount of vertices in DSU.</param>
    public DSU(int amountOfVertices)
    {
        for (int i = 0; i < amountOfVertices; ++i)
        {
            this.parent.Add(i);
            this.size.Add(1);
        }
    }

    /// <summary>
    /// Gets amount of elements in DSU.
    /// </summary>
    public int Size { get => this.parent.Count; }

    /// <summary>
    /// Gets a parent set of the vertex.
    /// </summary>
    /// <param name="vertex">Vertex, whose parent will be found.</param>
    /// <returns>Number of parent set of the vertex.</returns>
    /// <exception cref="IndexOutOfRangeException">Throws if vertex number is greater than amount of vertexes in DSU.</exception>
    public int GetParent(int vertex)
    {
        if (vertex >= this.Size)
        {
            throw new IndexOutOfRangeException();
        }

        if (this.parent[vertex] == vertex)
        {
            return vertex;
        }

        this.parent[vertex] = this.GetParent(this.parent[vertex]);
        return this.parent[vertex];
    }

    /// <summary>
    /// Unites two sets.
    /// </summary>
    /// <param name="firstVertex">Representative of the first set.</param>
    /// <param name="secondVertex">Representative of the second set.</param>
    /// <exception cref="IndexOutOfRangeException">Throws if vertex number is greater than amount of vertexes in DSU.</exception>
    public void Unite(int firstVertex, int secondVertex)
    {
        if (firstVertex >= this.Size || secondVertex >= this.Size)
        {
            throw new IndexOutOfRangeException();
        }

        if (this.size[secondVertex] > this.size[firstVertex])
        {
            (secondVertex, firstVertex) = (firstVertex, secondVertex);
        }

        int parent1 = this.GetParent(firstVertex);
        int parent2 = this.GetParent(secondVertex);
        this.size[parent1] += this.size[parent2];
        this.parent[parent2] = parent1;
    }
}
