using StackCalculator;

Console.WriteLine("Enter your expression:");

var expression = Console.ReadLine();

var result = new StackCalculator.StackCalculator(new StackList()).Calculate(expression);

Console.WriteLine($"{expression} = {result}");
