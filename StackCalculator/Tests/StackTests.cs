namespace Tests;
using StackCalculator;
public class StackTests
{
    static IEnumerable<IStack> Stack()
    {
        yield return new StackLinkedList();
        yield return new StackList();
    }

    [TestCaseSource(nameof(Stack))]
    public void PopEmptyStackTest(IStack stack)
    {
        if (stack.Size != 0 || !stack.IsEmpty)
        {
            Assert.Fail();
        }
        try 
        {
            stack.Pop();
        }
        catch (IndexOutOfRangeException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

    [TestCaseSource(nameof(Stack))]
    public void PushStackTest(IStack stack)
    {
        stack.Push(0);
        stack.Push(1);
        stack.Push(2);
        if (stack.Size != 3 || stack.IsEmpty)
        {
            Assert.Fail();
        }
        if (stack.Pop() != 2)
        {
            Assert.Fail();
        }
        if (stack.Pop() != 1) 
        {
            Assert.Fail();
        }
        if (stack.Pop() != 0)
        {
            Assert.Fail();
        }
        if (stack.Size != 0 || !stack.IsEmpty)
        {
            Assert.Fail();
        }
        try
        {
            stack.Pop();
        }
        catch (IndexOutOfRangeException)
        {
            Assert.Pass();
        }
        Assert.Fail();
    }
}
