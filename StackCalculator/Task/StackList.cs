namespace StackCalculator;

using System.Drawing;
using System.Dynamic;
using System.Reflection;
using Microsoft.VisualBasic;

/// <summary>
/// Stack, a first-in-last-out container.
/// </summary>
public class StackList : IStack
{
    private List<float> stack = new ();

    /// <inheritdoc/>
    public bool IsEmpty { get => this.stack.Count == 0; }

    /// <inheritdoc/>
    public int Size { get => this.stack.Count; }

    /// <inheritdoc/>
    public void Push(float element)
    {
        this.stack.Add(element);
    }

    /// <inheritdoc/>
    public float Pop()
    {
        if (this.stack.Count == 0)
        {
            throw new IndexOutOfRangeException();
        }

        var returnValue = this.stack[^1];
        this.stack.RemoveAt(this.stack.Count - 1);
        return returnValue;
    }
}
