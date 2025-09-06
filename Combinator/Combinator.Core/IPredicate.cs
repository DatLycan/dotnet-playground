namespace Combinator.Core;

using System.Runtime.CompilerServices;

public interface IPredicate<TSource>
{
    public bool TestAgainst(TSource source);
}

public readonly struct And<TSource, TA, TB>(TA a, TB b) : IPredicate<TSource>
    where TA : struct, IPredicate<TSource>
    where TB : struct, IPredicate<TSource>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TestAgainst(TSource source)
    {
        return a.TestAgainst(source) && b.TestAgainst(source);
    }
}

public readonly struct Or<TSource, TA, TB>(TA a, TB b) : IPredicate<TSource>
    where TA : struct, IPredicate<TSource>
    where TB : struct, IPredicate<TSource>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TestAgainst(TSource source)
    {
        return a.TestAgainst(source) || b.TestAgainst(source);
    }
}

public readonly struct Not<TSource, TA>(TA a) : IPredicate<TSource>
    where TA : struct, IPredicate<TSource>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TestAgainst(TSource source)
    {
        return !a.TestAgainst(source);
    }
}
