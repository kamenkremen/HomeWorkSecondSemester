using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.Linq.Expressions;

/// <summary>
/// Trie, tree data structure used for locating specific keys from within a set.
/// </summary>
public class Trie
{
    private int size = 0;
    private Node root;

    /// <summary>
    /// Initializes a new instance of the <see cref="Trie"/> class.
    /// </summary>
    public Trie()
    {
        this.root = new Node();
    }

    /// <summary>
    /// Gets ummary length of words in trie.
    /// </summary>
    public int Size
    {
        get => this.size;
        private set
        {
            this.size = value;
        }
    }

    /// <summary>
    /// Adds string to the trie.
    /// </summary>
    /// <param name="element">String to add.</param>
    /// <returns>True, if string not in trie, false otherwise.</returns>
    public bool Add(string element)
    {
        if (this.Contains(element))
        {
            return false;
        }

        var node = this.root;
        int index = 0;
        ++node.NumberOfPrefixes;
        for (; index < element.Length && node.Childrens.ContainsKey(element[index]); ++index)
        {
            node = node.Childrens[element[index]];
            ++node.NumberOfPrefixes;
        }

        for (; index < element.Length; ++index)
        {
            var newNode = new Node(element[index]);
            newNode.NumberOfPrefixes = 1;
            node.Childrens[element[index]] = newNode;
            node = newNode;
        }

        node.IsTerminal = true;
        this.Size += element.Length;
        return true;
    }

    /// <summary>
    /// Checks if string is contained.
    /// </summary>
    /// <param name="element">String.</param>
    /// <returns>Returns true if contained, false otherwise.</returns>
    public bool Contains(string element)
    {
        var node = this.root;
        for (int i = 0; i < element.Length; ++i)
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
    /// Removes string.
    /// </summary>
    /// <param name="element">String to remove.</param>
    /// <returns>Returns true if string is contained, false otherwise.</returns>
    public bool Remove(string element)
    {
        if (!this.Contains(element))
        {
            return false;
        }

        var node = this.root;
        for (int i = 0; i < element.Length; ++i)
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
    public int HowManyStartsWithPrefix(string prefix)
    {
        var node = this.root;
        for (int i = 0; i < prefix.Length; ++i)
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
            this.Childrens = new Dictionary<int, Node>();
        }

        public Node(char value)
        {
            this.Childrens = new Dictionary<int, Node>();
            this.Value = value;
        }

        public char Value { get; set; }

        public Dictionary<int, Node> Childrens { get; set; }

        public int NumberOfPrefixes { get; set; }

        public bool IsTerminal { get; set; }
    }
}
