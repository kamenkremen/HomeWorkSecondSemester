using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.Linq.Expressions;
Console.WriteLine("List of operations:");
Console.WriteLine("0 - exit the programm");
Console.WriteLine("1 - add string to the trie\n2 - check, if the trie contains string");
Console.WriteLine("2 - checks, if string contained in the trie");
Console.WriteLine("3 - remove the string from the trie");
Console.WriteLine("4 - check how many prefixes starts with the string");
Console.WriteLine("5 - returns size of the trie");
bool isWorking = true;
var trie = new Trie();
while (isWorking)
{
    Console.WriteLine("Enter number of the operation:");
    var operation = 0;
    if (!int.TryParse(Console.ReadLine(), out operation))
    {
        Console.WriteLine("Incorrect input, try again");
        continue;
    }

    string? str = string.Empty;
    switch (operation)
    {
        case 0:
        Console.WriteLine("Farewell");
        isWorking = false;
        break;
        case 1:
        Console.Write("Enter the string to add:");
        str = Console.ReadLine();
        if (str is null)
        {
            Console.WriteLine("Incorrect input, try again");
            break;
        }

        if (!trie.Add(str))
        {
            Console.WriteLine("This string was already in the trie");
        }
        else
        {
            Console.WriteLine("String succesfully added to the trie");
        }

        break;
        case 2:
        Console.Write("Enter the string to check if contained:");
        str = Console.ReadLine();
        if (str is null)
        {
            Console.WriteLine("Incorrect input, try again");
            break;
        }

        if (!trie.Contains(str))
        {
            Console.WriteLine("This string is not contained");
        }
        else
        {
            Console.WriteLine("This string is contained");
        }

        break;
        case 3:
        Console.Write("Enter the string to remove:");
        str = Console.ReadLine();
        if (str is null)
        {
            Console.WriteLine("Incorrect input, try again");
            break;
        }

        if (!trie.Remove(str))
        {
            Console.WriteLine("This string is not contained");
        }
        else
        {
            Console.WriteLine("String is sucessfully removed");
        }

        break;
        case 4:
        Console.Write("Enter the string to find out how many prefixes starts with it:");
        str = Console.ReadLine();
        if (str is null)
        {
            Console.WriteLine("Incorrect input, try again");
            break;
        }

        Console.WriteLine($"{trie.HowManyStartsWithPrefix(str)} prefixes starts with this string.");
        break;
        case 5:
        Console.WriteLine($"Size of the trie is {trie.Size}");
        break;
        default:
        Console.WriteLine("Incorrect input, try again");
        break;
    }
}

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
        this.Size += element.Length;
        return this.Add(0, element, this.root);
    }

    /// <summary>
    /// Checks if string is contained.
    /// </summary>
    /// <param name="element">String.</param>
    /// <returns>Returns true if contained, false otherwise.</returns>
    public bool Contains(string element)
    {
        return this.Contains(0, element, this.root);
    }

    /// <summary>
    /// Removes string.
    /// </summary>
    /// <param name="element">String to remove.</param>
    /// <returns>Returns true if string is contained, false otherwise.</returns>
    public bool Remove(string element)
    {
        var returnValue = this.Remove(0, element, this.root);
        if (returnValue)
        {
            this.Size -= element.Length;
        }

        return returnValue;
    }

    /// <summary>
    /// Finds out how many strings is starting with prefix.
    /// </summary>
    /// <param name="prefix">Prefix.</param>
    /// <returns>Returns how many strigs in trie start with that prefix.</returns>
    public int HowManyStartsWithPrefix(string prefix)
    {
        return this.HowManyStartsWithPrefix(0, prefix, this.root);
    }

    private bool Add(int index, string element, Node node)
    {
        ++node.NumberOfPrefixes;
        if (index == element.Length)
        {
            node.IsTerminal = true;
            return false;
        }

        if (!node.Childrens.ContainsKey(element[index]))
        {
            Node newNode = new Node(element[index]);
            this.Add(index + 1, element, newNode);
            node.Childrens[element[index]] = newNode;
            return true;
        }

        return this.Add(index + 1, element, node.Childrens[element[index]]);
    }

    private bool Contains(int index, string element, Node node)
    {
        if (index == element.Length)
        {
            return node.IsTerminal;
        }

        if (!node.Childrens.ContainsKey(element[index]))
        {
            return false;
        }

        return this.Contains(index + 1, element, node.Childrens[element[index]]);
    }

    private bool Remove(int index, string element, Node node)
    {
        if (index == element.Length & node.IsTerminal)
        {
            node.IsTerminal = false;
            --node.NumberOfPrefixes;
            return true;
        }

        if (!node.Childrens.ContainsKey(element[index]))
        {
            return false;
        }

        bool isContained = this.Remove(index + 1, element, node.Childrens[element[index]]);
        if (isContained)
        {
            --node.NumberOfPrefixes;
        }

        return isContained;
    }

    private int HowManyStartsWithPrefix(int index, string prefix, Node node)
    {
        if (index == prefix.Length)
        {
            return node.NumberOfPrefixes;
        }

        if (!node.Childrens.ContainsKey(prefix[index]))
        {
            return 0;
        }

        return this.HowManyStartsWithPrefix(index + 1, prefix, node.Childrens[prefix[index]]);
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
