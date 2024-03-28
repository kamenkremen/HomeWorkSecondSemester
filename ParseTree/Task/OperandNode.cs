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

namespace ParseTree;

/// <summary>
/// Node of the parse tree that contains operand.
/// </summary>
/// <param name="operand">Operand that is going to be contained.</param>
public class OperandNode(double operand): IParseTreeNode
{
    /// <summary>
    /// Gets or sets operand.
    /// </summary>
    public double Operand { get; set; } = operand;

    /// <inheritdoc/>
    public double CalculateSubTree() => this.Operand;

    /// <inheritdoc/>
    public void PrintSubTree() => Console.Write($"{this.Operand} ");
}
