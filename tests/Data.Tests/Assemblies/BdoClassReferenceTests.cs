using Bogus;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Contains unit tests for class reference management within the BindOpen Kernel.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoClassReference"/> instances, which combine an
/// <see cref="IBdoAssemblyReference"/> with a class name and identifier, are properly
/// initialized and compared for equality.
/// </remarks>
[TestFixture]
public class BdoClassReferenceTests
{
    /// <summary>
    /// Gets or sets the first class reference instance used in comparison tests.
    /// </summary>
    public IBdoClassReference _classRef1 = null;

    /// <summary>
    /// Gets or sets the second class reference instance used in comparison tests.
    /// </summary>
    public IBdoClassReference _classRef2 = null;

    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    /// <summary>
    /// Asserts that two <see cref="IBdoClassReference"/> instances are structurally equivalent.
    /// </summary>
    /// <param name="reference1">The first class reference to compare.</param>
    /// <param name="reference2">The second class reference to compare.</param>
    /// <exception cref="AssertionException">
    /// Thrown when only one of the references is <see langword="null"/> or when their properties do not match.
    /// </exception>
    public static void AssertEquals(
        IBdoClassReference reference1,
        IBdoClassReference reference2)
    {
        if ((reference1 != null && reference2 == null) || (reference1 == null && reference2 != null))
        {
            Assert.That(Equals(reference1, reference2), "One of the class reference objects is null while the other is not.");
        }

        // Deep equivalence check using FluentAssertions
        reference1.Should().BeEquivalentTo(reference2);
    }

    /// <summary>
    /// Initializes <see cref="_classRef1"/> using an empty assembly reference and a randomized class name.
    /// </summary>
    public void Create1Test()
    {
        var tests = new BdoAssemblyReferenceTests();
        tests.Create1Test();

        var f = new Faker();
        _classRef1 = BdoData.Class(tests._reference1, f.Random.Word());
    }

    /// <summary>
    /// Initializes <see cref="_classRef2"/> using a populated assembly reference, a randomized class name, and a generated identifier.
    /// </summary>
    public void Create2Test()
    {
        var tests = new BdoAssemblyReferenceTests();
        tests.Create2Test();

        var f = new Faker();
        _classRef2 = BdoData.Class(tests._reference2, f.Random.Word());
        _classRef2.Identifier = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Verifies that two distinct class references are not evaluated as equal.
    /// </summary>
    [Test]
    public void ClassRefEqualsTest()
    {
        Create1Test();
        Create2Test();

        Assert.That(_classRef1, Is.Not.EqualTo(_classRef2), "The class references should not be equal.");
    }
}