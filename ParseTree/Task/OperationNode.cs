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
/// Node of the parse tree that contains operation.
/// </summary>
/// <param name="operaion">Operation that going to be contained in the node.</param>
public class OperationNode(char operaion): IParseTreeNode
{
    /// <summary>
    /// Gets or sets contained operation.
    /// </summary>
    public char Operaion { get; set; } = operaion;

    /// <summary>
    /// Gets or sets left child of the Node.
    /// </summary>
    public IParseTreeNode? LeftChild { get; set; }

    /// <summary>
    /// Gets or sets right child of the node.
    /// </summary>
    public IParseTreeNode? RightChild { get; set; }

        /// <inheritdoc/>
    public double CalculateSubTree()
    {
        switch (this.Operaion)
        {
            case '+':
                return this.CalculateLeftChild() + this.CalculateRightChild();
            case '-':
                return this.CalculateLeftChild() - this.CalculateRightChild();
            case '*':
                return this.CalculateLeftChild() * this.CalculateRightChild();
            case '/':
                if (Math.Abs(this.CalculateRightChild()) < 1e-7)
                {
                    throw new DivideByZeroException();
                }

                return this.CalculateLeftChild() / this.CalculateRightChild();
            default:
                throw new ArgumentException("Unknown operation.");
        }
    }

    /// <inheritdoc/>
    public void PrintSubTree()
    {
        this.LeftChild?.PrintSubTree();
        Console.Write($"{this.Operaion} ");
        this.RightChild?.PrintSubTree();
    }

    private double CalculateLeftChild()
    {
        return this.LeftChild != null ? this.LeftChild.CalculateSubTree() : 0;
    }

    private double CalculateRightChild()
    {
        return this.RightChild != null ? this.RightChild.CalculateSubTree() : 0;
    }
}
