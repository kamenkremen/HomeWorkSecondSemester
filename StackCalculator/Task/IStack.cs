using System.Dynamic;

namespace StackCalculator;

/// <summary>
/// Represents a first-in-last-out collection of floats.
/// </summary>
public interface IStack
{
    /// <summary>
    /// Gets a number representing amount of elements in stack.
    /// </summary>
    int Size { get; }

    /// <summary>
    /// Gets a value indicating whether the stack is empty.
    /// </summary>
    bool IsEmpty { get; }

    /// <summary>
    /// Returns and remove top element from stack.
    /// </summary>
    /// <returns>Last element of the stack and flag, that indicates wheter element is popped or not.</returns>
    float Pop();

    /// <summary>
    /// Adds element to the top of the stack.
    /// </summary>
    /// <param name="element">Element to add to the stack.</param>
    void Push(float element);
}
