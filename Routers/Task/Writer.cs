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

/// <summary>
/// Writer that writes graph in file.
/// </summary>
public static class Writer
{
    /// <summary>
    /// Writes graph in file.
    /// </summary>
    /// <param name="graph">Graph to be written.</param>
    /// <param name="fileName">File where graph is going to be written.</param>
    public static void WriteGraph(Graph graph, string fileName)
    {
        var toWrite = string.Empty;
        var verticesToWrite = new List<List<(int, int)>>();
        for (int i = 0; i < graph.Size; ++i)
        {
            verticesToWrite.Add(new List<(int, int)>());
        }

        foreach (var edge in graph.Edges)
        {
            var minimalVertex = edge.Item1 < edge.Item2 ? edge.Item1 : edge.Item2;
            var maximalVertex = edge.Item1 < edge.Item2 ? edge.Item2 : edge.Item1;
            verticesToWrite[minimalVertex].Add((maximalVertex, edge.Item3));
        }

        for (int i = 0; i < graph.Size; ++i)
        {
            if (verticesToWrite[i].Count == 0)
            {
                continue;
            }

            toWrite += $"{i + 1}: ";
            for (int j = 0; j < verticesToWrite[i].Count; ++j)
            {
                var edge = verticesToWrite[i][j];
                toWrite += $"{edge.Item1 + 1} ({edge.Item2})";
                if (j != verticesToWrite[i].Count - 1)
                {
                    toWrite += ", ";
                }
                else
                {
                    toWrite += "\n";
                }
            }
        }

        File.WriteAllText(fileName, toWrite);
    }
}
