using Bogus;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BindOpen.Data.Assemblies;

[TestFixture, Order(210)]
public class BdoAssemblyReferenceTests
{
    public IBdoAssemblyReference _reference1 = null, _reference2 = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    public static void AssertEquals(
        IBdoAssemblyReference reference1,
        IBdoAssemblyReference reference2)
    {
        if ((reference1 != null && reference2 == null) || (reference1 == null && reference2 != null))
        {
            Assert.That(Equals(reference1, reference2), "Unmatched objects");
        }

        reference1.Should().BeEquivalentTo(reference2);
    }

    public void Create1Test()
    {
        _reference1 = BdoData.Assembly(null, null);
    }

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

    [Test, Order(1)]
    public void AssemblyRefEqualsTest()
    {
        Create1Test();
        Create2Test();

        Assert.That(_reference1 != _reference2, "Bad assembly reference");
    }
}
