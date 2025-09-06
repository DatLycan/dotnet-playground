namespace Combinator.Tests;

using Combinator.Core;
using NUnit.Framework;

public class LogicGatesTests
{
  [TestCase(true, true, true, TestName = "TrueAndTrue => True")]
  [TestCase(true, false, false, TestName = "TrueAndFalse => False")]
  [TestCase(false, true, false, TestName = "FalseAndTrue => False")]
  [TestCase(false, false, false, TestName = "FalseAndFalse => False")]
  public void AndTest(bool first, bool second, bool expected)
  {
    var combinatorChain = Combinator.Where<object>(_ => first).And(_ => second).Build();
    var result = combinatorChain(default!);
    Assert.That(result, Is.EqualTo(expected));
  }

  [TestCase(true, true, true, TestName = "TrueOrTrue => True")]
  [TestCase(true, false, true, TestName = "TrueOrFalse => True")]
  [TestCase(false, true, true, TestName = "FalseOrTrue => True")]
  [TestCase(false, false, false, TestName = "FalseOrFalse => False")]
  public void OrTest(bool first, bool second, bool expected)
  {
    var combinatorChain = Combinator.Where<object>(_ => first).Or(_ => second).Build();
    var result = combinatorChain(default!);
    Assert.That(result, Is.EqualTo(expected));
  }

  [TestCase(true, false, TestName = "NotTrue => False")]
  [TestCase(false, true, TestName = "NotFalse => True")]
  public void NotTest(bool input, bool expected)
  {
    var combinatorChain = Combinator.Where<object>(_ => input).Not().Build();
    var result = combinatorChain(default!);
    Assert.That(result, Is.EqualTo(expected));
  }
}
