using BindOpen.Data.Objects;
using BindOpen.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for <see cref="IBdoReference"/>, verifying the encapsulation of data pointers.
/// </summary>
/// <remarks>
/// References in BindOpen allow for pointing to metadata or resources using either literal paths 
/// or dynamic script-based expressions.
/// </remarks>
[TestFixture, Order(210)]
public class BdoReferenceTests
{
    private dynamic _valueSet;

    /// <summary>
    /// The reference instance currently being tested.
    /// </summary>
    public IBdoReference _reference = null;

    /// <summary>
    /// Initializes the test value set using the reference faker.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _valueSet = BdoReferenceFaker.GetValueSet();
    }

    /// <summary>
    /// Asserts that two reference objects are equivalent.
    /// </summary>
    /// <param name="ref1">The first reference to compare.</param>
    /// <param name="ref2">The second reference to compare.</param>
    public static void AssertEquals(
        IBdoReference ref1,
        IBdoReference ref2)
    {
        if ((ref1 != null && ref2 == null) || (ref1 == null && ref2 != null))
        {
            Assert.That(Equals(ref1, ref2), Is.True, "The reference objects do not match in their nullability state.");
        }

        ref1.Should().BeEquivalentTo(ref2);
    }

    /// <summary>
    /// Tests the creation of a reference based on a literal expression.
    /// </summary>
    [Test, Order(1)]
    public void Create1Test()
    {
        // We create a reference pointing to a literal path
        _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Literal as string, BdoExpressionKind.Literal));

        _reference?.ToString().Should().Be((string)_valueSet.Literal, "The string representation of a literal reference should match its raw value.");
    }

    /// <summary>
    /// Tests the creation of a reference based on a script expression.
    /// </summary>
    [Test, Order(2)]
    public void Create2Test()
    {
        // We create a reference pointing to a script-based expression
        _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Script as string, BdoExpressionKind.Script));

        _reference?.ToString().Should().Be((string)_valueSet.Script, "The string representation of a script reference should match the script source.");
    }
}