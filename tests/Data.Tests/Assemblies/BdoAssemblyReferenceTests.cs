using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;

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
        IBdoAssemblyReference exp1,
        IBdoAssemblyReference exp2)
    {
        if ((exp1 != null && exp2 == null) || (exp1 == null && exp2 != null))
        {
            Assert.That(Equals(exp1, exp2), "Unmatched objects");
        }

        var deepEq = exp1.WithDeepEqual(exp2);
        deepEq.Assert();
    }

    public void Create1Test()
    {
        _reference1 = BdoData.Assembly(null, null);
    }

    public void Create2Test()
    {
        var f = new Faker();
        _reference2 = BdoData.Assembly(f.Random.Word());
    }

    [Test, Order(1)]
    public void AssemblyRefEqualsTest()
    {
        Create1Test();
        Create2Test();

        Assert.That(_reference1 != _reference2, "Bad assembly reference");
    }
}
