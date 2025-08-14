using BindOpen.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class BdoReferenceTests
{
    private dynamic _valueSet;
    public IBdoReference _reference = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _valueSet = BdoReferenceFaker.GetValueSet();
    }

    public static void AssertEquals(
        IBdoReference ref1,
        IBdoReference ref2)
    {
        if ((ref1 != null && ref2 == null) || (ref1 == null && ref2 != null))
        {
            Assert.That(Equals(ref1, ref2), "Unmatched objects");
        }

        ref1.Should().BeEquivalentTo(ref2);
    }

    [Test, Order(1)]
    public void Create1Test()
    {
        _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Literal as string, BdoExpressionKind.Literal));

        Assert.That(_reference?.ToString() == (string)_valueSet.Literal, "");
    }

    [Test, Order(2)]
    public void Create2Test()
    {
        _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Script as string, BdoExpressionKind.Script));

        Assert.That(_reference?.ToString() == (string)_valueSet.Script, "");
    }
}
