using BindOpen.Data.Objects;
using BindOpen.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for <see cref="IBdoExpression"/>, focusing on the distinction between literals and scripts.
/// </summary>
/// <remarks>
/// These tests verify that expressions are properly instantiated, categorized according to their
/// <see cref="BdoExpressionKind"/>, and preserve their underlying textual representation.
/// </remarks>
[TestFixture]
public class BdoExpressionTests
{
    private dynamic _valueSet;

    /// <summary>
    /// Gets or sets the <see cref="IBdoExpression"/> instance currently under test.
    /// </summary>
    public IBdoExpression _exp = null;

    /// <summary>
    /// Initializes the test value set using <see cref="BdoExpressionFaker"/> before any tests are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _valueSet = BdoExpressionFaker.GetValueSet();
    }

    /// <summary>
    /// Asserts that two <see cref="IBdoExpression"/> instances are structurally equivalent.
    /// </summary>
    /// <param name="exp1">The first expression to compare.</param>
    /// <param name="exp2">The second expression to compare.</param>
    /// <exception cref="AssertionException">
    /// Thrown when only one of the expressions is <see langword="null"/> or when their properties do not match.
    /// </exception>
    public static void AssertEquals(
        IBdoExpression exp1,
        IBdoExpression exp2)
    {
        if ((exp1 != null && exp2 == null) || (exp1 == null && exp2 != null))
        {
            Assert.That(Equals(exp1, exp2), Is.True, "One of the expressions is null while the other is not.");
        }

        exp1.Should().BeEquivalentTo(exp2);
    }

    /// <summary>
    /// Validates that the specified <see cref="IBdoExpression"/> matches the expected text
    /// from the test dataset according to its <see cref="BdoExpressionKind"/>.
    /// </summary>
    /// <param name="exp">The expression instance to validate.</param>
    public void Test(IBdoExpression exp)
    {
        switch (exp.ExpressionKind)
        {
            case BdoExpressionKind.Literal:
                exp.Text.Should().Be((string)_valueSet.Literal, "Literal expressions should store raw text.");
                break;
            case BdoExpressionKind.Script:
                exp.Text.Should().Be((string)_valueSet.Script, "Script expressions should store the interpretable script string.");
                break;
        }
    }

    /// <summary>
    /// Verifies the creation of a literal expression (<see cref="BdoExpressionKind.Literal"/>)
    /// using the <see cref="BdoData.NewExp"/> factory method.
    /// </summary>
    [Test]
    public void Create1Test()
    {
        _exp = BdoData.NewExp(
            _valueSet.Literal as string,
            BdoExpressionKind.Literal);

        Test(_exp);
    }

    /// <summary>
    /// Verifies the creation of a script expression (<see cref="BdoExpressionKind.Script"/>)
    /// using the fluent <c>AsScript()</c> builder syntax.
    /// </summary>
    [Test]
    public void Create2Test()
    {
        _exp = BdoData.NewExpression(_valueSet.Script as string)
            .AsScript();

        Test(_exp);
    }
}