namespace Combinator.Core;

using System.Runtime.CompilerServices;

public readonly struct Combinator<TSource>
{
    // [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // public static Combinator<TSource, TPredicate> With<TPredicate>(TPredicate predicate)
    //     where TPredicate : struct, IPredicate<TSource>
    // {
    //     return new(predicate);
    // }
}

public readonly struct Combinator<TSource, TPredicate>
    where TPredicate : struct, IPredicate<TSource>
{
    private readonly TPredicate predicate;

    internal Combinator(TPredicate predicate)
    {
        this.predicate = predicate;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Combinator<TSource, And<TSource, TPredicate, TNext>> And<TNext>(TNext next)
        where TNext : struct, IPredicate<TSource>
    {
        return new(new And<TSource, TPredicate, TNext>(predicate, next));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Combinator<TSource, Or<TSource, TPredicate, TNext>> Or<TNext>(TNext next)
        where TNext : struct, IPredicate<TSource>
    {
        return new(new Or<TSource, TPredicate, TNext>(predicate, next));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Combinator<TSource, Not<TSource, TPredicate>> Not()
    {
        return new(new Not<TSource, TPredicate>(predicate));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TPredicate Build() => predicate;
}
