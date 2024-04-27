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
/// Tree, that parses expressions.
/// </summary>
public class ParseTree
{
    private IParseTreeNode? root;

    /// <summary>
    /// Calculate expression in parse tree.
    /// </summary>
    /// <returns>Result of the expression.</returns>
    /// <exception cref="ArgumentNullException">Throws if there is no expression in tree.</exception>
    public double CalculateTree()
    {
        if (this.root == null)
        {
            throw new ArgumentNullException("Tree can`t be empty.");
        }

        return this.root.CalculateSubTree();
    }

    /// <summary>
    /// Gets expression in tree in string.
    /// </summary>
    /// <returns>Expression in tree as a string.</returns>
    /// <exception cref="ArgumentNullException">Throws if there is no expression in tree.</exception>
    public string GetTree()
    {
        if (this.root == null)
        {
            throw new ArgumentNullException("Tree can`t be empty.");
        }

        return this.root.PrintSubTree();
    }

    /// <summary>
    /// Builds tree by expression.
    /// </summary>
    /// <param name="expression">Expresion, by which tree will be builded.</param>
    public void BuildTree(string expression)
    {
        ArgumentException.ThrowIfNullOrEmpty(expression);
        expression = expression.Replace('(', ' ').Replace(')', ' ');
        var splittedExpression = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        IsCorrect(splittedExpression);
        var index = 0;
        this.root = Build(splittedExpression, ref index);
        if (index != splittedExpression.Length)
        {
            throw new ArgumentException("Incorrect input.");
        }
    }

    private static IParseTreeNode? Build(string[] expression, ref int index)
    {
        if (index == expression.Length)
        {
            return null;
        }

        if (index > expression.Length)
        {
            throw new ArgumentException("Incorrect input.");
        }

        if (double.TryParse(expression[index], out double number))
        {
            ++index;
            return new OperandNode(number);
        }
        else
        {
            OperationNode newNode = new (expression[index][0]);
            ++index;
            newNode.LeftChild = Build(expression, ref index);
            newNode.RightChild = Build(expression, ref index);
            if (newNode.RightChild == null || newNode.LeftChild == null)
            {
                throw new ArgumentException("Incorrect input.");
            }

            return newNode;
        }
    }

    private static void IsCorrect(string[] expression)
    {
        var numbers = 0;
        var operations = 0;
        foreach (var node in expression)
        {
            if (double.TryParse(node, out double result))
            {
                ++numbers;
            }
            else
            {
                ++operations;
            }
        }

        if (operations != numbers - 1)
        {
            throw new ArgumentException("Incorrect input.");
        }
    }
}
