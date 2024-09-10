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
        var exp = _dataTests._reference1;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(exp);
        dbContext.SaveChanges();

        var expDb = dbContext.GetAssemblyReference(exp.Identifier).ToPoco();
        Assert.That(exp.IsDeepEqual(expDb) == true, "Bad item storage");
    }

    [Test, Order(2)]
    public void UpdateTest()
    {
        if (_dataTests._reference1 == null)
        {
            InsertTest();
        }

        var exp = _dataTests._reference1;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var id = _dataTests._reference1?.Identifier;
        _dataTests.Create2Test();
        _dataTests._reference1.Identifier = id;

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

        var exp = _dataTests._reference1;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var dto = exp.ToDto();
        dbContext.Remove(dto);
        dbContext.SaveChanges();

        var expDb = dbContext.GetAssemblyReference(exp.Identifier).ToPoco();
        Assert.That(expDb == null, "Bad item storage");
    }
}
