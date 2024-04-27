namespace Lists;

/// <summary>
/// Throws after attempt to add allready existing element.
/// </summary>
public class ExistingValueException : InvalidOperationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExistingValueException"/> class.
    /// </summary>
    public ExistingValueException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExistingValueException"/> class.
    /// </summary>
    /// <param name="message">Specified error message.</param>
    public ExistingValueException(string message)
        : base(message)
    {
    }
}
