namespace Tests;
using StackCalculator;
public class Tests
{
    static IEnumerable<(IStack, string, double, bool)> Calculator()
    {
        yield return (new StackLinkedList(), "1 3 5 + *", 5, true);
        yield return (new StackList(), "1 3 5 + *", 5, true);
        yield return (new StackLinkedList(), "1 0 /", 0, false);
        yield return (new StackList(), "1 0 /", 0, false);
        yield return (new StackLinkedList(), "1 2 )", 0, false);
        yield return (new StackList(), "1 2 )", 0, false);
        yield return (new StackLinkedList(), "1 2 + +", 0, false);
        yield return (new StackList(), "1 2 + +", 0, false);
        yield return (new StackLinkedList(), "10 3 /", 3.333333333, true);
        yield return (new StackList(), "10 3 /", 3.333333333, true);
        yield return (new StackLinkedList(), "123 74 + 56 / 6 -", -2.48214285714, true);
        yield return (new StackList(), "123 74 + 56 / 6 -", -2.48214285714, true);
    }

    [TestCaseSource(nameof(Calculator))]
    public void CalculateTest((IStack, string, double, bool) testInput)
    {
        (var stackRealization, var expression, var expectedResult, var isCountable) = testInput;
        try
        {
            var result = new StackCalculator(new StackList()).Calculate(expression);
            Assert.That(isCountable && Math.Abs(expectedResult - result) > 1e-9);
        }

        catch (Exception)
        {
            Assert.That(!isCountable);
        }
        Assert.Pass();
    }
}
