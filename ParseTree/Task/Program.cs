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

Console.WriteLine("Enter the expression to calculate.");

var expression = Console.ReadLine();

var tree = new ParseTree.ParseTree();
try
{
    tree.BuildTree(expression!);
}
catch (ArgumentException)
{
    Console.WriteLine("Incorrect expression.");
    return;
}
catch (DivideByZeroException)
{
    Console.WriteLine("Division by zero error.");
    return;
}

try
{
    tree.PrintTree();
    Console.WriteLine($"= {tree.CalculateTree()}");
}
catch (ArgumentNullException)
{
    Console.WriteLine("Incorrect expression");
}
