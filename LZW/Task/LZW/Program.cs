if (args.Length < 2 || args.Length > 3)
{
    Console.WriteLine("Incorrect input.");
    return;
}

string? path = args[0];
if (path == null)
{
    Console.WriteLine("Path cant be null.");
    return;
}

var command = args[1];
var withBWT = 0;
if (args.Length == 3 && args[2] == "-bwt")
{
    withBWT = 1;
}
else if (args.Length == 3 && args[2] == "-compare")
{
    withBWT = 2;
}

if (withBWT == 2 && command == "-u")
{
    Console.WriteLine("Can`t compare while encoding, choose with bwt or without it.");
    return;
}

switch (command)
{
    case "-c":
    Console.WriteLine("Encoding...");
    switch (withBWT)
    {
        case 0:
        try
        {
            double ratio = LZW.Encode(path, false);
            Console.WriteLine($"Compress ratio without BWT = {ratio}");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"Error:{exception.Message}");
            return;
        }

        break;
        case 1:
        try
        {
            double BWTratio = LZW.Encode(path, true);
            Console.WriteLine($"Compress ratio with BWT = {BWTratio}");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"Error:{exception.Message}");
            return;
        }

        break;
        case 2:
        try
        {
            double BWTratioToCompare = LZW.Encode(path, true);
            double ratioToCompare = LZW.Encode(path, false);
            Console.WriteLine($"Compress ratio with BWT = {BWTratioToCompare}, ratio without BWT = {ratioToCompare}");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"Error:{exception.Message}");
            return;
        }

        break;
    }

    break;
    case "-u":
    Console.WriteLine("Decoding...");
    switch (withBWT)
    {
        case 0:
        try
        {
            LZW.Decode(path, false);
            Console.WriteLine("Decoded!");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"Error:{exception.Message}");
            return;
        }

        break;
        case 1:
        try
        {
            LZW.Decode(path, true);
            Console.WriteLine("Decoded!");
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"Error:{exception.Message}");
            return;
        }

        break;
    }

    break;
    default:
    Console.WriteLine("Incorrect input.");
    return;
}
