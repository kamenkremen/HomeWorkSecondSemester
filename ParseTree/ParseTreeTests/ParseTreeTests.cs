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

namespace ParseTreeTests;

public class Tests
{
    static IEnumerable<TestCaseData> TestCases()
    {
        yield return new TestCaseData("(* (+ 1 1) 2)").Returns(4);
        yield return new TestCaseData("1").Returns(1);
        yield return new TestCaseData("+ -1 1").Returns(0);
        yield return new TestCaseData("* -1 1").Returns(-1);
        yield return new TestCaseData("- 1 -1").Returns(2);
        yield return new TestCaseData("/ 5 2").Returns(2.5);
        yield return new TestCaseData("/ 9 3").Returns(3);
    }

    [TestCaseSource(nameof(TestCases))]
    public double TestShouldWorkCorrectly(string expression)
    {
        var tree = new ParseTree.ParseTree();
        tree.BuildTree(expression);
        return tree.CalculateTree();
    }

    [TestCase("/ 1 0")]
    [TestCase("/ 1 0.00000000001")]
    public void TestShouldThrowDivisionByZeroException(string expression)
    {
        var tree = new ParseTree.ParseTree();
        tree.BuildTree(expression);
        try
        {
            tree.CalculateTree();
        }
        catch (DivideByZeroException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [TestCase("/")]
    [TestCase("+ / 1 2")]
    [TestCase("+ / + 1")]
    [TestCase("+ 1 2 3")]
    [TestCase("3 2 /")]
    public void TestShouldThrowArgumentException(string expression)
    {
        var tree = new ParseTree.ParseTree();
        try
        {
            tree.BuildTree(expression);
        }
        catch (ArgumentException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void TestPrintTreeShouldThrowArgumentNullException()
    {
        var tree = new ParseTree.ParseTree();
        try
        {
            tree.PrintTree();
        }
        catch (ArgumentNullException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [Test]
    public void TestCalculateTreeShouldThrowArgumentNullException()
    {
        var tree = new ParseTree.ParseTree();
        try
        {
            tree.CalculateTree();
        }
        catch (ArgumentNullException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }
}
