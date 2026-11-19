using Bogus;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Contains unit tests for class reference management within the BindOpen Kernel.
/// </summary>
/// <remarks>
/// These tests verify that class references, which combine an assembly reference 
/// with a class name, are correctly instantiated and compared.
/// </remarks>
[TestFixture, Order(210)]
public class BdoClassReferenceTests
{
    /// <summary>
    /// The first class reference used for comparison.
    /// </summary>
    public IBdoClassReference _classRef1 = null;

    /// <summary>
    /// The second class reference used for comparison.
    /// </summary>
    public IBdoClassReference _classRef2 = null;

    /// <summary>
    /// Performs one-time setup for the class reference tests.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    /// <summary>
    /// Asserts that two class references are equivalent.
    /// </summary>
    /// <param name="reference1">The first class reference to compare.</param>
    /// <param name="reference2">The second class reference to compare.</param>
    /// <exception cref="AssertionException">Thrown when the references are not equivalent.</exception>
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
    /// Creates a class reference using a default/null assembly reference.
    /// </summary>
    public void Create1Test()
    {
        var tests = new BdoAssemblyReferenceTests();
        tests.Create1Test();

        var f = new Faker();
        _classRef1 = BdoData.Class(tests._reference1, f.Random.Word());
    }

    /// <summary>
    /// Creates a randomized class reference using a fully populated assembly reference.
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
    /// Tests that two different class references are not considered equal.
    /// </summary>
    [Test, Order(1)]
    public void ClassRefEqualsTest()
    {
        Create1Test();
        Create2Test();

        Assert.That(_classRef1, Is.Not.EqualTo(_classRef2), "The class references should not be equal.");
    }
}