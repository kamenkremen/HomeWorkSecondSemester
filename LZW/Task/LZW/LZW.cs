using Storages;
using Trie;
using BWT;

/// <summary>
/// Encodes and decodes files with LZW.
/// </summary>
public class LZW
{
    /// <summary>
    /// Encodes file.
    /// </summary>
    /// <param name="filePath">Path to the file that going to be encoded.</param>
    /// <param name="withBWT">Should encoder use BWT or not.</param>
    /// <returns>Compress ratio.</returns>
    public static double Encode(string filePath, bool withBWT = false)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException("File doesn`t exist.", nameof(filePath));
        }

        var fileInBytes = File.ReadAllBytes(filePath);
        if (fileInBytes.Length == 0)
        {
            throw new ArgumentException("File can`t be empty.", nameof(fileInBytes));
        }
        if (withBWT)
        {
            var (bytesAfterBWT, originalIndex) = BWT.BWT.ForwardBWT(fileInBytes);
            var indexByteForm = BitConverter.GetBytes(originalIndex);
            fileInBytes = new byte[bytesAfterBWT.Length + indexByteForm.Length];
            bytesAfterBWT.CopyTo(fileInBytes, 0);
            indexByteForm.CopyTo(fileInBytes, bytesAfterBWT.Length);
        }

        var newFilePath = filePath + ".zipped";
        File.WriteAllBytes(newFilePath, Encode(fileInBytes));
        var sizeBeforeLZW = new FileInfo(filePath).Length;
        var sizeAfterLZW = new FileInfo(newFilePath).Length;
        if (sizeAfterLZW == 0)
        {
            return 1;
        }

        return (double)sizeBeforeLZW / (double)sizeAfterLZW;
    }

    /// <summary>
    /// Decodes file encoded with LZW.
    /// </summary>
    /// <param name="filePath">Path to the file that going to be decoded.</param>
    /// <param name="withBWT">Was BWT used with encoding or not.</param>
    public static void Decode(string filePath, bool withBWT = false)
    {
        if (!File.Exists(filePath))
        {
            throw new ArgumentException("File doesn`t exist.", nameof(filePath));
        }

        var newFilePath = filePath.Substring(0, filePath.LastIndexOf('.'));
        var fileInBytes = File.ReadAllBytes(filePath);
        if (fileInBytes.Length == 0)
        {
            throw new ArgumentException("File can`t be empty.", nameof(fileInBytes));
        }
        fileInBytes = Decode(fileInBytes);

        if (withBWT)
        {
            var indexByteForm = new byte[4];
            for (int i = 0; i < 4; ++i)
            {
                indexByteForm[i] = fileInBytes[fileInBytes.Length - 4 + i];
            }

            var originalIndex = BitConverter.ToInt32(indexByteForm);
            Array.Resize(ref fileInBytes, fileInBytes.Length - 4);
            fileInBytes = BWT.BWT.ReverseBWT(fileInBytes, originalIndex);
        }

        File.WriteAllBytes(newFilePath, fileInBytes);
    }

    private static byte[] Encode(byte[] file)
    {
        var dictionary = new Trie.Trie();
        for (int i = 0; i < 256; ++i)
        {
            dictionary.Add([(byte)i], i);
        }

        var currentNumber = 256;
        var buffer = new List<byte>();
        var currentPhrase = new List<byte>();
        currentPhrase.Add(file[0]);
        var newPhrase = new List<byte>(currentPhrase);
        var coded = new EncodeByteStorage();
        for (int i = 0; i < file.Length; ++i)
        {
            if (i != file.Length - 1)
            {
                buffer.Add(file[i + 1]);
            }

            newPhrase = new (currentPhrase);
            newPhrase.AddRange(buffer);
            if (dictionary.Contains(newPhrase))
            {
                currentPhrase = new (newPhrase);
            }
            else
            {
                coded.Add(dictionary.GetValue(currentPhrase));
                if (coded.DifferentSymbolCapacity == dictionary.Size)
                {
                    ++coded.BitsInOneSymbol;
                }

                dictionary.Add(newPhrase, currentNumber);
                ++currentNumber;
                currentPhrase = new (buffer);
            }

            buffer.Clear();
        }

        coded.Add(dictionary.GetValue(currentPhrase));
        return coded.GetByteArray();
    }

    private static byte[] Decode(byte[] file)
    {
        var dictionary = new Dictionary<int, List<byte>>();
        for (var i = 0; i < 256; ++i)
        {
            dictionary.Add(i, [(byte)i]);
        }

        var currentNumber = 256;
        var decoded = new DecodeByteStorage();
        for (var i = 0; i < file.Length; ++i)
        {
            if (currentNumber > decoded.NumberCapacity)
            {
                ++decoded.BitsInOneNumber;
            }

            if (decoded.Add(file[i]))
            {
                ++currentNumber;
            }
        }

        var encodedNumbers = decoded.GetIntArray();
        var bytes = new List<byte>();
        currentNumber = 256;
        int oldCode = encodedNumbers[0];
        var buffer = new List<byte>(dictionary[oldCode]);
        foreach (var singleByte in buffer)
        {
            bytes.Add(singleByte);
        }

        var newCode = 0;
        byte firstBufferByte = buffer[0];
        for (int i = 1; i < encodedNumbers.Length; ++i)
        {
            newCode = encodedNumbers[i];
            if (!dictionary.TryGetValue(newCode, out List<byte>? value))
            {
                buffer = new (dictionary[oldCode]);
                buffer.Add(firstBufferByte);
            }
            else
            {
                buffer = value;
            }

            bytes.AddRange(buffer);
            var addBytes = new List<byte>(dictionary[oldCode]);
            firstBufferByte = buffer[0];
            addBytes.Add(firstBufferByte);
            dictionary.Add(currentNumber, addBytes);
            ++currentNumber;
            oldCode = newCode;
        }

        return [.. bytes];
    }
}
