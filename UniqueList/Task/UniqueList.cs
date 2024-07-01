namespace Lists;

/// <summary>
/// UniqueList, container for elements that contains only unique instances.
/// </summary>
/// <typeparam name="T">Data contained in list.</typeparam>
public class UniqueList<T> : List<T>
{
    /// <summary>
    /// Adds value to the end of list.
    /// </summary>
    /// <param name="value">Value that is going to be added to the list.</param>
    /// <exception cref="AlreadyExistingValueException">Throws when attempt to add already existing element in list are being made.</exception>
    public override void Add(T value)
    {
        if (this.Find(value) != -1)
        {
            throw new ExistingValueException(nameof(value));
        }

        base.Add(value);
    }

    /// <summary>
    /// Sets value by index.
    /// </summary>
    /// <param name="index">Index of the element in list.</param>
    /// <param name="value">Value, that is going to be setted.</param>
    /// <exception cref="AddingOrSettingExistingValueException">Throws when attempt to set already existing element in list are being made.</exception>
    public override void SetValue(int index, T value)
    {
        if (this.Find(value) != -1)
        {
            throw new ExistingValueException(nameof(value));
        }

        base.SetValue(index, value);
    }
}
