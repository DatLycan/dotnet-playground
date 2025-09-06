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
  /// Inverts the result of the current combinator using a logical NOT.
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Combinator<TSource> Not()
  {
    var prev = predicate;
    return new(s => !prev(s));
  }

  /// <summary>
  /// Gets the underlying predicate function.
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public Func<TSource, bool> Build() => predicate;
}
