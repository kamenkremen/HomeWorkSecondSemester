namespace Lists;

/// <summary>
/// Throws after attempt to add allready existing element.
/// </summary>
public class AddingOrSettingExistingValueException : InvalidOperationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddingOrSettingExistingValueException"/> class.
    /// </summary>
    public AddingOrSettingExistingValueException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddingOrSettingExistingValueException"/> class.
    /// </summary>
    /// <param name="message">Specified error message.</param>
    public AddingOrSettingExistingValueException(string message)
        : base(message)
    {
    }
}
