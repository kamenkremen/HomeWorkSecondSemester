namespace Lists;

/// <summary>
/// Throws after attempt to remove something that doesn't exist.
/// </summary>
public class NotExistingElementException : InvalidOperationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotExistingElementException"/> class.
    /// </summary>
    public NotExistingElementException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotExistingElementException"/> class.
    /// </summary>
    /// <param name="message">Specified error message.</param>
    public NotExistingElementException(string message)
        : base(message)
    {
    }
}
