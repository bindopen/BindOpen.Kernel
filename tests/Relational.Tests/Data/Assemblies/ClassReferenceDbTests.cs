using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data.Assemblies;

[TestFixture, Order(304)]
public class ClassReferenceDbTests
{
    private BdoClassReferenceTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoClassReferenceTests();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        _dataTests.Create1Test();
        var reference = _dataTests._classRef1;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        dbContext.Upsert(reference);
        dbContext.SaveChanges();

        var referenceDb = dbContext.GetClassReference(reference.Identifier).ToPoco();

        var deepEq = reference.WithDeepEqual(referenceDb);
        deepEq.Assert();
    }

    [Test, Order(2)]
    public void UpdateTest()
    {
        if (_dataTests._classRef1 == null)
        {
            InsertTest();
        }

        _dataTests.Create2Test();
        var reference = _dataTests._classRef2;
        var id = _dataTests._classRef1?.Identifier;
        _dataTests._classRef2.Identifier = id;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        dbContext.Upsert(reference);
        dbContext.SaveChanges();

        var referenceDb = dbContext.GetClassReference(reference.Identifier).ToPoco();

        var deepEq = reference.WithDeepEqual(referenceDb);
        deepEq.Assert();
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._classRef1 == null)
        {
            InsertTest();
        }

        var reference = _dataTests._classRef1;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        var dto = reference.ToDb();
        dbContext.Remove(dto);
        dbContext.SaveChanges();

        var referenceDb = dbContext.GetClassReference(reference.Identifier).ToPoco();
        Assert.That(referenceDb == null, "Bad item storage");
    }
}
