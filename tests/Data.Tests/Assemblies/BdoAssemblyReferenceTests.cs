using Bogus;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Contains unit tests for assembly reference management within the BindOpen Kernel.
/// </summary>
/// <remarks>
/// These tests verify that assembly identifiers, versions, file names, and aliases
/// are properly initialized, populated, and compared for equality.
/// </remarks>
[TestFixture]
public class BdoAssemblyReferenceTests
{
    /// <summary>
    /// Gets or sets the first assembly reference instance used in comparison tests.
    /// </summary>
    public IBdoAssemblyReference _reference1 = null;

    /// <summary>
    /// Gets or sets the second assembly reference instance used in comparison tests.
    /// </summary>
    public IBdoAssemblyReference _reference2 = null;

    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    /// <summary>
    /// Asserts that two <see cref="IBdoAssemblyReference"/> instances are structurally equivalent.
    /// </summary>
    /// <param name="reference1">The first assembly reference to compare.</param>
    /// <param name="reference2">The second assembly reference to compare.</param>
    /// <exception cref="AssertionException">
    /// Thrown when only one of the references is <see langword="null"/> or when their properties do not match.
    /// </exception>
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
    /// Initializes <see cref="_reference1"/> with <see langword="null"/> name and version values.
    /// </summary>
    public void Create1Test()
    {
        _reference1 = BdoData.Assembly(null, null);
    }

    /// <summary>
    /// Initializes <see cref="_reference2"/> with randomized fake data for name, version, alias, file name, and identifier.
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
    /// Verifies that two distinct assembly references (one empty, one populated) are not evaluated as equal.
    /// </summary>
    [Test]
    public void AssemblyRefEqualsTest()
    {
        Create1Test();
        Create2Test();

        Assert.That(_reference1, Is.Not.EqualTo(_reference2), "The assembly references should not be equal.");
    }
}