namespace StackCalculator;

using System.Drawing;
using System.Dynamic;
using System.Reflection;
using Microsoft.VisualBasic;

/// <summary>
/// Stack, a first-in-last-out container.
/// </summary>
public class StackLinkedList : IStack
{
    private LinkedList<float> stack = new ();

    /// <inheritdoc/>
    public int Size { get => this.stack.Count; }

    /// <inheritdoc/>
    public void Push(float element)
    {
        this.stack.AddLast(element);
    }

    /// <inheritdoc/>
    public float Pop()
    {
        if (this.stack.Count == 0)
        {
            throw new IndexOutOfRangeException();
        }

        var returnValue = this.stack.Last.Value;
        this.stack.RemoveLast();
        return returnValue;
    }
}
