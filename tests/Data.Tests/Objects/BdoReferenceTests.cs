using BindOpen.Data.Objects;
using BindOpen.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for <see cref="IBdoReference"/>, verifying the encapsulation of data pointers.
/// </summary>
/// <remarks>
/// References in BindOpen allow pointing to metadata elements or external resources using either
/// literal paths or dynamic script-based expressions.
/// </remarks>
[TestFixture]
public class BdoReferenceTests
{
    private dynamic _valueSet;

    /// <summary>
    /// Gets or sets the <see cref="IBdoReference"/> instance currently under test.
    /// </summary>
    public IBdoReference _reference = null;

    /// <summary>
    /// Initializes the test dataset using <see cref="BdoReferenceFaker"/> before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _valueSet = BdoReferenceFaker.GetValueSet();
    }

    /// <summary>
    /// Asserts that two <see cref="IBdoReference"/> instances are structurally equivalent.
    /// </summary>
    /// <param name="ref1">The first reference instance to compare.</param>
    /// <param name="ref2">The second reference instance to compare.</param>
    /// <exception cref="AssertionException">
    /// Thrown when only one of the references is <see langword="null"/> or when their properties do not match.
    /// </exception>
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
    /// Verifies the creation of an <see cref="IBdoReference"/> backed by a literal expression (<see cref="BdoExpressionKind.Literal"/>).
    /// </summary>
    [Test]
    public void Create1Test()
    {
        // Create a reference pointing to a literal path
        _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Literal as string, BdoExpressionKind.Literal));

        _reference?.ToString().Should().Be((string)_valueSet.Literal, "The string representation of a literal reference should match its raw value.");
    }

    /// <summary>
    /// Verifies the creation of an <see cref="IBdoReference"/> backed by a script expression (<see cref="BdoExpressionKind.Script"/>).
    /// </summary>
    [Test]
    public void Create2Test()
    {
        // Create a reference pointing to a script-based expression
        _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Script as string, BdoExpressionKind.Script));

        _reference?.ToString().Should().Be((string)_valueSet.Script, "The string representation of a script reference should match the script source.");
    }
}