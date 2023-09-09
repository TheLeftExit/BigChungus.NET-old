using System.Numerics;
using System.Runtime.CompilerServices;

namespace BigChungus.Managed;

public static class ReturnValueExceptionExtensions
{
    public static T ThrowIf<T>(this T value, Predicate<T> toThrow, [CallerArgumentExpression(nameof(value))] string valueAsString = null)
    {
        if (toThrow(value))
        {
            throw new ReturnValueException(valueAsString);
        }
        return value;
    }

    public static T ThrowIf<T>(this T value, T check, [CallerArgumentExpression(nameof(value))] string valueAsString = null) where T : unmanaged, IEqualityOperators<T, T, bool>
    {
        return ThrowIf(value, x => x == check, valueAsString);
    }

    public static T ThrowIfNot<T>(this T value, T check, [CallerArgumentExpression(nameof(value))] string valueAsString = null) where T : unmanaged, IEqualityOperators<T, T, bool>
    {
        return ThrowIf(value, x => x != check, valueAsString);
    }

    public static bool ThrowIfFalse(this bool value, [CallerArgumentExpression(nameof(value))] string valueAsString = null)
    {
        return ThrowIf(value, x => !x, valueAsString);
    }
}

public class ReturnValueException : Exception
{
    public ReturnValueException(string message) : base(message) { }
}
