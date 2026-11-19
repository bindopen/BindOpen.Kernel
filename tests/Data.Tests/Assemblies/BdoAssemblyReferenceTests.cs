using Bogus;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Contains unit tests for assembly reference management within the BindOpen Kernel.
/// </summary>
/// <remarks>
/// These tests ensure that assembly identifiers, versions, and aliases are 
/// correctly handled and compared.
/// </remarks>
[TestFixture, Order(210)]
public class BdoAssemblyReferenceTests
{
    /// <summary>
    /// The first assembly reference used for comparison.
    /// </summary>
    public IBdoAssemblyReference _reference1 = null;

    /// <summary>
    /// The second assembly reference used for comparison.
    /// </summary>
    public IBdoAssemblyReference _reference2 = null;

    /// <summary>
    /// Performs one-time setup for the assembly reference tests.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    /// <summary>
    /// Asserts that two assembly references are equivalent.
    /// </summary>
    /// <param name="reference1">The first assembly reference to compare.</param>
    /// <param name="reference2">The second assembly reference to compare.</param>
    /// <exception cref="AssertionException">Thrown when the references are not equivalent.</exception>
    public static void AssertEquals(
        IBdoAssemblyReference reference1,
        IBdoAssemblyReference reference2)
    {
        if ((reference1 != null && reference2 == null) || (reference1 == null && reference2 != null))
        {
            Assert.That(Equals(reference1, reference2), "One of the assembly reference objects is null while the other is not.");
        }

        // Using FluentAssertions for deep equivalence check
        reference1.Should().BeEquivalentTo(reference2);
    }

    /// <summary>
    /// Creates a default/null assembly reference for testing.
    /// </summary>
    public void Create1Test()
    {
        _reference1 = BdoData.Assembly(null, null);
    }

    /// <summary>
    /// Creates a randomized assembly reference populated with fake data.
    /// </summary>
    public void Create2Test()
    {
        var f = new Faker();
        _reference2 = BdoData.Assembly(
            f.Random.Word(),
            new Version(f.Random.Int(1000), f.Random.Int(1000), f.Random.Int(1000), f.Random.Int(1000)));

        _reference2.Alias = f.Lorem.Word();
        _reference2.AssemblyFileName = f.Lorem.Word();
        _reference2.Identifier = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Tests that two different assembly references are not considered equal.
    /// </summary>
    [Test, Order(1)]
    public void AssemblyRefEqualsTest()
    {
        Create1Test();
        Create2Test();

        Assert.That(_reference1, Is.Not.EqualTo(_reference2), "The assembly references should not be equal.");
    }
}