namespace Lists;

/// <summary>
/// List, container for elements.
/// </summary>
/// <typeparam name="TData">Type of the data in list.</typeparam>
public class List<TData>
{
    private ListElement? first = default(ListElement);

    /// <summary>
    /// Gets amount of nodes in list.
    /// </summary>
    public int Size { get; private set; } = 0;

    /// <summary>
    /// Gets a value indicating whether list is empty or not.
    /// </summary>
    public bool IsEmpty { get => this.Size == 0; }

    /// <summary>
    /// Gets of sets value by index.
    /// </summary>
    /// <param name="index">Index of the element in list.</param>
    /// <returns>Value contained by node by index.</returns>
    public TData this[int index]
    {
        get => this.GetValue(index);
        set => this.SetValue(index, value);
    }

    /// <summary>
    /// Gets value by index.
    /// </summary>
    /// <param name="index">Index of the element in list.</param>
    /// <returns>Value contained by node by index.</returns>
    /// <exception cref="IndexOutOfRangeException">Exception that indicates that index is bigger than list size.</exception>
    public TData GetValue(int index)
    {
        if (index >= this.Size)
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        ListElement? current = this.first;
        for (int i = 0; i < index; ++i)
        {
            current = current!.Next;
        }

        return current!.Value;
    }

    /// <summary>
    /// Find index by which value is contained.
    /// </summary>
    /// <param name="value">Value, that is going to be founded.</param>
    /// <returns>Index of the value or -1 if value is not contained in the list.</returns>
    public int Find(TData value)
    {
        var current = this.first;
        for (int i = 0; i < this.Size; ++i)
        {
            if (EqualityComparer<TData>.Default.Equals(value, current!.Value))
            {
                return i;
            }

            current = current.Next;
        }

        return -1;
    }

    /// <summary>
    /// Sets value by index.
    /// </summary>
    /// <param name="index">Index of the element in list.</param>
    /// <param name="value">Value, that is going to be setted.</param>
    /// <exception cref="IndexOutOfRangeException">Exception that indicates that index is bigger than list size..</exception>
    public virtual void SetValue(int index, TData value)
    {
        if (index >= this.Size)
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        var current = this.first;
        for (int i = 0; i < index; ++i)
        {
            current = current!.Next;
        }

        current!.Value = value;
    }

    /// <summary>
    /// Adds value to the end of list.
    /// </summary>
    /// <param name="value">Value that is going to be added to the list.</param>
    public virtual void Add(TData value)
    {
        if (this.Size == 0)
        {
            this.first = new ListElement(value);
            ++this.Size;
            return;
        }

        var current = this.first;
        while (current!.Next != default(ListElement))
        {
            current = current.Next;
        }

        current.Next = new ListElement(value);
        ++this.Size;
    }

    /// <summary>
    /// Remvoes element from list by index.
    /// </summary>
    /// <param name="index">Index, bu which element is going to be removed.</param>
    /// <exception cref="IndexOutOfRangeException">Exception that indicates that index is bigger than list size.</exception>
    public void Remove(int index)
    {
        if (index >= this.Size || index < 0)
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        var current = this.first;
        --this.Size;
        if (index == 0)
        {
            this.first = this.first!.Next;
            return;
        }

        for (int i = 0; i < index - 1; ++i)
        {
            current = current!.Next;
        }

        current!.Next = current.Next!.Next;
    }

    /// <summary>
    /// Removes element by its value.
    /// </summary>
    /// <param name="value">Value by which element is going to be removed.</param>
    /// <exception cref="ValueNotInListException">Exception that throws if value is not in list.</exception>
    public void RemoveByValue(TData value)
    {
        int index = this.Find(value);
        if (index == -1)
        {
            throw new RemovingNotExistingElementException(nameof(value));
        }

        this.Remove(index);
    }

    private class ListElement(TData value)
    {
        public TData Value { get; set; } = value;

        public ListElement? Next { get; set; }
    }
}
