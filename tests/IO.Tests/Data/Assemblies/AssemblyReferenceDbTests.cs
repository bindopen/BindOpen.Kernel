using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data.Assemblies;

[TestFixture, Order(304)]
public class AssemblyReferenceDbTests
{
    private BdoAssemblyReferenceTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoAssemblyReferenceTests();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        _dataTests.Create1Test();
        var reference = _dataTests._reference1;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(reference);
        dbContext.SaveChanges();

        var expDb = dbContext.GetAssemblyReference(reference.Identifier).ToPoco();
        Assert.That(reference.IsDeepEqual(expDb) == true, "Bad item storage");
    }

    [Test, Order(2)]
    public void UpdateTest()
    {
        if (_dataTests._reference1 == null)
        {
            InsertTest();
        }

        _dataTests.Create2Test();
        var exp = _dataTests._reference2;
        var id = _dataTests._reference1?.Identifier;
        _dataTests._reference2.Identifier = id;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(exp);
        dbContext.SaveChanges();

        var expDb = dbContext.GetAssemblyReference(exp.Identifier).ToPoco();
        Assert.That(exp.IsDeepEqual(expDb) == true, "Bad item storage");
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._reference1 == null)
        {
            InsertTest();
        }

        var reference = _dataTests._reference1;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var dto = reference.ToDto();
        dbContext.Remove(dto);
        dbContext.SaveChanges();

        var expDb = dbContext.GetAssemblyReference(reference.Identifier).ToPoco();
        Assert.That(expDb == null, "Bad item storage");
    }
}
