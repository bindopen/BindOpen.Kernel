using Bogus;
using NUnit.Framework;

namespace BindOpen.Data.Assemblies;

[TestFixture, Order(210)]
public class BdoClassReferenceTests
{
    public IBdoClassReference _classRef1 = null, _classRef2 = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
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
    }

    [Test, Order(1)]
    public void ClassRefEqualsTest()
    {
        Create1Test();
        Create2Test();

        Assert.That(_classRef1 != _classRef2, "Bad class reference");
    }
}
