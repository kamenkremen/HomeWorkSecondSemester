namespace StackCalculator;

/// <summary>
/// Stack calculator, class that calculates expressions in postfix forms.
/// </summary>
/// <param name="stack">Stack realization.</param>
public class StackCalculator(IStack stack)
{
    private List<string> operations = new List<string>();

    private IStack numbers = stack;

    /// <summary>
    /// Calculates the expression.
    /// </summary>
    /// <param name="expression">Expression to calculate.</param>
    /// <returns>Value that indicates if there was an error and result of the expression.</returns>
    public float Calculate(string expression)
    {
        this.ParseExpression(expression);
        while (this.numbers.Size > 1)
        {
            var firstNumber = this.numbers.Pop();
            var secondNumber = this.numbers.Pop();
            var operation = this.operations[0];
            this.operations.RemoveAt(0);
            float result = 0;
            switch (operation)
            {
                case "/":
                if (Math.Abs(firstNumber) < 1e-9)
                {
                    throw new DivideByZeroException();
                }

                result = secondNumber / firstNumber;
                this.numbers.Push(result);
                break;
                case "*":
                result = firstNumber * secondNumber;
                this.numbers.Push(result);
                break;
                case "+":
                result = firstNumber + secondNumber;
                this.numbers.Push(result);
                break;
                case "-":
                result = secondNumber - firstNumber;
                this.numbers.Push(result);
                break;
                default:
                throw new InvalidOperationException();
            }
        }

        return this.numbers.Pop();
    }

    private void ParseExpression(string expression)
    {
        var splittedExpression = expression.Split(' ');
        foreach (var element in splittedExpression)
        {
            float floatElement = 0;
            if (float.TryParse(element, out floatElement))
            {
                this.numbers.Push(floatElement);
            }
            else
            {
                this.operations.Add(element);
            }
        }
        if (this.operations.Count() != this.numbers.Size - 1)
        {
            throw new InvalidDataException();
        }
    }
}
