namespace Combinator.Tests;

using Combinator.Core;

public class LogicGatesTests
{
    [Test]
    public void AndTest()
    {
        Assert.Pass();
    }

    [Test]
    public void OrTest()
    {
        Assert.Pass();
    }

    [Test]
    public void NotTest()
    {
        Assert.Pass();
    }

    public readonly struct True<TSource> : IPredicate<TSource>
    {
        public bool TestAgainst(TSource source) => true;
    }

    public readonly struct False<TSource> : IPredicate<TSource>
    {
        public bool TestAgainst(TSource source) => false;
    }
}
