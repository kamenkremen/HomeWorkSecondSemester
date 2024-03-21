namespace Trie;

/// <summary>
/// Trie, tree data structure used for locating specific keys from within a set.
/// </summary>
public class Trie
{
    private Node root;

    /// <summary>
    /// Initializes a new instance of the <see cref="Trie"/> class.
    /// </summary>
    public Trie()
    {
        this.root = new Node();
    }

    /// <summary>
    /// Gets amount of words in dictionary.
    /// </summary>
    public int Size
    {
        get => this.root.NumberOfPrefixes;
    }

    /// <summary>
    /// Adds string to the trie.
    /// </summary>
    /// <param name="element">String to add.</param>
    /// <returns>True, if string not in trie, false otherwise.</returns>
    public bool Add(List<byte> element, int value)
    {
        if (this.Contains(element))
        {
            return false;
        }

        var node = this.root;
        int index = 0;
        ++node.NumberOfPrefixes;
        for (; index < element.Count && node.Childrens.ContainsKey(element[index]); ++index)
        {
            node = node.Childrens[element[index]];
            ++node.NumberOfPrefixes;
        }

        for (; index < element.Count; ++index)
        {
            var newNode = new Node(value);
            newNode.NumberOfPrefixes = 1;
            node.Childrens[element[index]] = newNode;
            node = newNode;
        }

        node.IsTerminal = true;
        return true;
    }

    /// <summary>
    /// Checks if string is contained.
    /// </summary>
    /// <param name="element">String.</param>
    /// <returns>Returns true if contained, false otherwise.</returns>
    public bool Contains(List<byte> element)
    {
        var node = this.root;
        for (int i = 0; i < element.Count; ++i)
        {
            if (!node.Childrens.ContainsKey(element[i]))
            {
                return false;
            }

            node = node.Childrens[element[i]];
        }

        return node.IsTerminal;
    }

    /// <summary>
    /// returns value of the node with the key.
    /// </summary>
    /// <param name="key">Key of the value.</param>
    /// <returns>Value of the key.</returns>
    public int GetValue(List<byte> key)
    {
        if (!this.Contains(key))
        {
            return -1;
        }

        var node = this.root;
        for (int i = 0; i < key.Count; ++i)
        {
            node = node.Childrens[key[i]];
        }
        return node.Value;
    }

    /// <summary>
    /// Removes string.
    /// </summary>
    /// <param name="element">String to remove.</param>
    /// <returns>Returns true if string is contained, false otherwise.</returns>
    public bool Remove(List<byte> element)
    {
        if (!this.Contains(element))
        {
            return false;
        }

        var node = this.root;
        for (int i = 0; i < element.Count; ++i)
        {
            --node.NumberOfPrefixes;

            if (!node.Childrens.ContainsKey(element[i]))
            {
                return false;
            }

            node = node.Childrens[element[i]];
        }

        --node.NumberOfPrefixes;
        if (!node.IsTerminal)
            {
                return false;
            }

        node.IsTerminal = false;
        return true;
    }

    /// <summary>
    /// Finds out how many strings is starting with prefix.
    /// </summary>
    /// <param name="prefix">Prefix.</param>
    /// <returns>Returns how many strigs in trie start with that prefix.</returns>
    public int HowManyStartsWithPrefix(List<byte> prefix)
    {
        var node = this.root;
        for (int i = 0; i < prefix.Count; ++i)
        {
            if (!node.Childrens.ContainsKey(prefix[i]))
            {
                return 0;
            }

            node = node.Childrens[prefix[i]];
        }

        return node.NumberOfPrefixes;
    }

    private class Node
    {
        public Node()
        {
            this.Childrens = new Dictionary<byte, Node>();
        }

        public Node(int value)
        {
            this.Childrens = new Dictionary<byte, Node>();
            this.Value = value;
        }

        public int Value { get; set; }

        public Dictionary<byte, Node> Childrens { get; set; }

        public int NumberOfPrefixes { get; set; }

        public bool IsTerminal { get; set; }
    }
}
