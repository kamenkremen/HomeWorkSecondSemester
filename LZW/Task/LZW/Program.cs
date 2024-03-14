Console.WriteLine("Enter the path to a file:");
string? path = Console.ReadLine();
if (path == null)
{
    Console.WriteLine("Path cant be null.");
    return;
}

Console.WriteLine("Do you wish to encode a file or decode a file?(Enter 1 to encode, 2 to decode).");
var command = 0;
if (!int.TryParse(Console.ReadLine(), out command) || command > 2 || command < 1)
{
    Console.WriteLine("Incorrect input");
    return;
}
var withBWT = 0;
switch (command)
{
    case 1:
    Console.WriteLine("Do you wish to encode with BWT without it?(Enter 1 to choose BWT, 2 otherwise).");
    withBWT = 0;
    if (!int.TryParse(Console.ReadLine(), out withBWT) || command > 2 || command < 1)
    {
        Console.WriteLine("Incorrect input");
        return;
    }

    switch (withBWT)
    {
        case 1:
        double ratio = LZW.Encode(path, true);
        Console.WriteLine($"Compress ratio with BWT = {ratio}");
        break;
        case 2:
        double BWTratio = LZW.Encode(path, true);
        Console.WriteLine($"Compress ratio with BWT = {BWTratio}");
        break;
    }

    break;
    case 2:
    Console.WriteLine("Was file encoded with BWT?(Enter 1 to choose BWT, 2 otherwise).");
    withBWT = 0;
    if (!int.TryParse(Console.ReadLine(), out withBWT) || command > 2 || command < 1)
    {
        Console.WriteLine("Incorrect input");
        return;
    }

    switch (withBWT)
    {
        case 1:
        LZW.Decode(path, true);
        Console.WriteLine("Decoded!");
        break;
        case 2:
        LZW.Decode(path, false);
        Console.WriteLine("Decoded!");
        break;
    }

    break;
}
