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

Console.WriteLine("Enter name of the input file:");
var inputFileName = Console.ReadLine();
if (inputFileName == string.Empty)
{
    Console.WriteLine("File name can`t be empty.");
    return;
}

Console.WriteLine("Enter name of the output file:");
var outputFileName = Console.ReadLine();
if (outputFileName == string.Empty)
{
    Console.WriteLine("File name can`t be empty.");
    return;
}

Utilities.Graph? graph = null;
try
{
    graph = Utilities.Parser.GetGraph(inputFileName!);
}
catch (ArgumentException)
{
    Console.WriteLine("Incorrect input.");
    return;
}

var spanningTree = graph.GetMaxSpanningTree();

Writer.WriteGraph(spanningTree, outputFileName!);

Console.WriteLine("Succesfully writed!");