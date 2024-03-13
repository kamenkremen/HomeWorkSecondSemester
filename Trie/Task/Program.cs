Console.WriteLine("List of operations:");
Console.WriteLine("0 - exit the program");
Console.WriteLine("1 - add string to the trie");
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
