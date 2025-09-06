namespace Combinator.Core;

using System;
using System.Runtime.CompilerServices;

public static class Combinator
{
  /// <summary>
  /// Serves as the starting point for a combinator chain.
  /// </summary>
  /// <typeparam name="TSource">The type of the source object.</typeparam>
  /// <param name="predicate">The initial predicate function.</param>
  /// <returns>A new Combinator struct with the initial predicate.</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Combinator<TSource> Where<TSource>(Func<TSource, bool> predicate)
  {
    return new(predicate);
  }
}

public readonly struct Combinator<TSource>
{
  private readonly Func<TSource, bool> predicate;

  internal Combinator(Func<TSource, bool> predicate)
  {
    this.predicate = predicate;
  }

  /// <summary>
  /// Combines the current combinator with a new one using a logical AND.
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Combinator<TSource> And(Func<TSource, bool> next)
  {
    var prev = predicate;
    return new(s => prev(s) && next(s));
  }

  /// <summary>
  /// Combines the current combinator with a new one using a logical OR.
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Combinator<TSource> Or(Func<TSource, bool> next)
  {
    var prev = predicate;
    return new(s => prev(s) || next(s));
  }

  /// <summary>
  /// Combines the current combinator with a new one using a logical XOR.
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Combinator<TSource> Xor(Func<TSource, bool> next)
  {
    var prev = predicate;
    return new(s => prev(s) ^ next(s));
  }

  /// <summary>
  /// Combines the current combinator with a new one using a logical NAND (Not-AND).
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Combinator<TSource> Nand(Func<TSource, bool> next)
  {
    var prev = predicate;
    return new(s => !(prev(s) && next(s)));
  }

  /// <summary>
  /// Combines the current combinator with a new one using a logical NOR (Not-OR).
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Combinator<TSource> Nor(Func<TSource, bool> next)
  {
    var prev = predicate;
    return new(s => !(prev(s) || next(s)));
  }

  /// <summary>
  /// Inverts the result of the current combinator using a logical NOT.
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Combinator<TSource> Not()
  {
    var prev = predicate;
    return new(s => !prev(s));
  }

  /// <summary>
  /// Evaluates the current combinator against a source object.
  /// </summary>
  /// <param name="source">The source object to test.</param>
  /// <returns>True if the object satisfies the combinator; otherwise, false.</returns>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Using(TSource source) => predicate(source);
}
