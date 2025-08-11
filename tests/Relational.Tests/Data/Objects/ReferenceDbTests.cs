using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(302)]
public class ReferenceDbTests
{
    private BdoReferenceTests _dataTests;
    private BdoScopingReferenceTests _scopingTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoReferenceTests();
        _dataTests.OneTimeSetUp();

        _scopingTests = new BdoScopingReferenceTests();
        _scopingTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        if (_dataTests._reference == null)
        {
            _dataTests.Create1Test();
        }

        var reference = _dataTests._reference;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        dbContext.Upsert(reference);
        dbContext.SaveChanges();

        var referenceDb = dbContext.GetReference(reference.Identifier).ToPoco();
        Assert.That(reference.IsDeepEqual(referenceDb) == true, "Bad item storage");
    }

    [Test, Order(2)]
    public void Update2Test()
    {
        if (_dataTests._reference == null)
        {
            InsertTest();
        }

        var reference = _dataTests._reference;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        var id = _dataTests._reference?.Identifier;
        _dataTests.Create2Test();
        _dataTests._reference.Identifier = id;

        dbContext.Upsert(reference);
        dbContext.SaveChanges();

        var referenceDb = dbContext.GetReference(reference.Identifier).ToPoco();
        Assert.That(reference.IsDeepEqual(referenceDb) == true, "Bad item storage");
    }

    [Test, Order(3)]
    public void Update3Test()
    {
        if (_dataTests._reference == null)
        {
            InsertTest();
        }

        var reference = _dataTests._reference;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        var id = _dataTests._reference?.Identifier;
        _scopingTests.Create3Test();
        _dataTests._reference.Identifier = id;

        dbContext.Upsert(reference);
        dbContext.SaveChanges();


        var referenceDb = dbContext.GetReference(reference.Identifier).ToPoco();
        Assert.That(reference.IsDeepEqual(referenceDb) == true, "Bad item storage");
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._reference == null)
        {
            InsertTest();
        }

        var reference = _dataTests._reference;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        dbContext.Delete(reference);
        dbContext.SaveChanges();

        var referenceDb = dbContext.GetReference(reference.Identifier).ToPoco();
        Assert.That(referenceDb == null, "Bad item storage");
    }
}
