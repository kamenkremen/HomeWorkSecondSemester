namespace Lists;

/// <summary>
/// Throws after attempt to remove something that doesn't exist.
/// </summary>
public class RemovingNotExistingElementException : InvalidOperationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RemovingNotExistingElementException"/> class.
    /// </summary>
    public RemovingNotExistingElementException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RemovingNotExistingElementException"/> class.
    /// </summary>
    /// <param name="message">Specified error message.</param>
    public RemovingNotExistingElementException(string message)
        : base(message)
    {
    }
}
