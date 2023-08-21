using System.Runtime.CompilerServices;

namespace BigChungus.Managed;

internal class ReturnValueException : Exception
{
    private ReturnValueException(string message) : base(message) { }

    public static void ThrowIf(string functionName, bool condition, [CallerArgumentExpression(nameof(condition))] string conditionAsString = null)
    {
        if (!condition) return;
        throw new ReturnValueException($"{functionName} returned an invalid value ({conditionAsString}).");
    }
}
