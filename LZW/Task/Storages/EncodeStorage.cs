namespace Storages;

/// <summary>
/// Encodes byte storage.
/// </summary>
public class EncodeByteStorage
{
    private const int BITS_IN_BYTE = 8;
    private byte currentByte = 0;
    private int currentByteSize = 0;
    private List<byte> byteList = new ();

    /// <summary>
    /// Get encoded byte array.
    /// </summary>
    /// <returns>Encoded byte array.</returns>
    public byte[] GetByteArray()
    {
        if (currentByteSize != 0)
        {
            this.currentByte <<= BITS_IN_BYTE - this.currentByteSize;
            this.byteList.Add(this.currentByte);
            this.currentByte = 0;
            this.currentByteSize = 0;
        }

        return this.byteList.ToArray();
    }

    /// <summary>
    /// Gets or sets how many bits in one symbol.
    /// </summary>
    public int BitsInOneSymbol { get; set; } = 8;

    /// <summary>
    /// Gets how many how many different symbols can be.
    /// </summary>
    public int DifferentSymbolCapacity { get => 1 << this.BitsInOneSymbol; }

    /// <summary>
    /// Adds a number to the byte container.
    /// </summary>
    /// <param name="number">Number to add to byte container.</param>
    public void Add(int number)
    {
        var binaryRepresentation = this.GetBinaryRepresentation(number);
        foreach (var bit in binaryRepresentation)
        {
            this.currentByte <<= 1;
            ++this.currentByteSize;
            this.currentByte += (byte)(bit ? 1 : 0);
            if (this.currentByteSize == BITS_IN_BYTE)
            {
                this.byteList.Add(this.currentByte);
                this.currentByte = 0;
                this.currentByteSize = 0;
            }
        }
    }

    private bool[] GetBinaryRepresentation(int number)
    {
        var bitRepresentation = new bool[this.BitsInOneSymbol];
        for (int i = this.BitsInOneSymbol - 1; i >= 0; --i)
        {
            bitRepresentation[i] = number % 2 == 1;
            number >>= 1;
        }

        return bitRepresentation;
    }
}
