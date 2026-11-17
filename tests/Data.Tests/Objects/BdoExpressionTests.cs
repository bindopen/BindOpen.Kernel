using BindOpen.Data.Objects.Interfaces;
using BindOpen.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for <see cref="IBdoExpression"/>, focusing on the distinction between literals and scripts.
/// </summary>
/// <remarks>
/// These tests verify that expressions are correctly categorized and store their textual representation
/// according to their <see cref="BdoExpressionKind"/>.
/// </remarks>
[TestFixture, Order(210)]
public class BdoExpressionTests
{
    private dynamic _valueSet;

    /// <summary>
    /// The expression instance currently being tested.
    /// </summary>
    public IBdoExpression _exp = null;

    /// <summary>
    /// Initializes the test value set using the expression faker.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _valueSet = BdoExpressionFaker.GetValueSet();
    }

    /// <summary>
    /// Asserts that two expressions are equivalent.
    /// </summary>
    /// <param name="exp1">The first expression to compare.</param>
    /// <param name="exp2">The second expression to compare.</param>
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
    /// Validates an expression based on its kind and expected value from the test data set.
    /// </summary>
    /// <param name="exp">The expression to validate.</param>
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
    /// Tests the creation of a literal expression using the short factory method.
    /// </summary>
    [Test, Order(1)]
    public void Create1Test()
    {
        _exp = BdoData.NewExp(
            _valueSet.Literal as string,
            BdoExpressionKind.Literal);

        Test(_exp);
    }

    /// <summary>
    /// Tests the creation of a script expression using the fluent API.
    /// </summary>
    [Test, Order(2)]
    public void Create2Test()
    {
        _exp = BdoData.NewExpression(_valueSet.Script as string)
            .AsScript();

        Test(_exp);
    }
}