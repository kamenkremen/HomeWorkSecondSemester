namespace Storages;

/// <summary>
/// Decodes byte storage.
/// </summary>
public class DecodeByteStorage
{
    private const int BITS_IN_BYTE = 8;

    private int currentInt = 0;
    private int currentIntSize = 0;
    private List<int> numbersList = new ();

    /// <summary>
    /// Get decoded array of integers.
    /// </summary>
    /// <returns>Decoded array of integers.</returns>
    public int[] GetIntArray()
    {
        if (currentInt != 0)
        {
            this.numbersList.Add(this.currentInt);
            this.currentInt = 0;
            this.currentIntSize = 0;
        }
        //this.numbersList.RemoveAt(numbersList.Count - 1);
        return this.numbersList.ToArray();
    }

    /// <summary>
    /// Gets or sets how many bits in one symbol.
    /// </summary>
    public int BitsInOneNumber { get; set; } = 8;

    /// <summary>
    /// Gets how many different numbers there is.
    /// </summary>
    public int NumberCapacity { get => 1 << BitsInOneNumber; }

    /// <summary>
    /// Add byte to storage.
    /// </summary>
    /// <param name="byteToAdd">Byte to add to storage.</param>
    /// <returns>True if byte is added to list of numbers and false otherwise.</returns>
    public bool Add(byte byteToAdd)
    {
        var binaryRepresentation = this.GetBinaryRepresentation(byteToAdd);
        var isAdded = false;
        foreach (var bit in binaryRepresentation)
        {
            this.currentInt <<= 1;
            this.currentInt += bit ? 1 : 0;
            ++this.currentIntSize;
            
            if (this.currentIntSize == this.BitsInOneNumber)
            {
                this.numbersList.Add(this.currentInt);
                this.currentInt = 0;
                this.currentIntSize = 0;
                isAdded = true;
            }
        }

        return isAdded;
    }

    private bool[] GetBinaryRepresentation(byte number)
    {
        var bitRepresentation = new bool[BITS_IN_BYTE];
        for (int i = BITS_IN_BYTE - 1; i >= 0; --i)
        {
            bitRepresentation[i] = number % 2 == 1;
            number >>= 1;
        }

        return bitRepresentation;
    }
}
