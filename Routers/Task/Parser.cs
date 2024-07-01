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
/// Parser that parses the input file.
/// </summary>
public static class Parser
{
    /// <summary>
    /// Gets a graph that was written in the file.
    /// </summary>
    /// <param name="fileName">Name of the file that contains the graph.</param>
    /// <returns>Graph, that was written in the file.</returns>
    /// <exception cref="ArgumentException">Thtows if the input file was incorrect.</exception>
    public static Graph GetGraph(string fileName)
    {
        var lines = File.ReadAllLines(fileName);
        var edges = new List<(int, int, int)>();
        var size = lines.Length;
        foreach (var line in lines)
        {
            var parsedLine = line.Replace(':', ',').Split(", ");
            if (!int.TryParse(parsedLine[0], out int currentRouter))
            {
                throw new ArgumentException();
            }

            for (int i = 1; i < parsedLine.Length; ++i)
            {
                var currentEdge = parsedLine[i].Replace('(', ' ').Replace(')', ' ').Split(' ',  StringSplitOptions.RemoveEmptyEntries);
                if (!int.TryParse(currentEdge[0], out int secondVertex))
                {
                    throw new ArgumentException();
                }

                size = size > secondVertex ? size : secondVertex;
                if (!int.TryParse(currentEdge[1], out int weight))
                {
                    throw new ArgumentException();
                }

                edges.Add((currentRouter - 1, secondVertex - 1, weight));
            }
        }

        var graph = new Graph(size);
        foreach (var edge in edges)
        {
            graph.AddEdge(edge.Item1, edge.Item2, edge.Item3);
        }

        return graph;
    }
}