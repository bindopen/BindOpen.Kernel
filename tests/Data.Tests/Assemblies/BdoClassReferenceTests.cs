using Bogus;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace BindOpen.Data.Assemblies;

[TestFixture, Order(210)]
public class BdoClassReferenceTests
{
    public IBdoClassReference _classRef1 = null, _classRef2 = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    public static void AssertEquals(
        IBdoClassReference reference1,
        IBdoClassReference reference2)
    {
        if ((reference1 != null && reference2 == null) || (reference1 == null && reference2 != null))
        {
            Assert.That(Equals(reference1, reference2), "Unmatched objects");
        }

        reference1.Should().BeEquivalentTo(reference2);
    }

    public void Create1Test()
    {
        var tests = new BdoAssemblyReferenceTests();
        tests.Create1Test();

        var f = new Faker();
        _classRef1 = BdoData.Class(tests._reference1, f.Random.Word());
    }

    public void Create2Test()
    {
        var tests = new BdoAssemblyReferenceTests();
        tests.Create2Test();

        var f = new Faker();
        _classRef2 = BdoData.Class(tests._reference2, f.Random.Word());
        _classRef2.Identifier = Guid.NewGuid().ToString();
    }

    [Test, Order(1)]
    public void ClassRefEqualsTest()
    {
        Create1Test();
        Create2Test();

        Assert.That(_classRef1 != _classRef2, "Bad class reference");
    }
}
