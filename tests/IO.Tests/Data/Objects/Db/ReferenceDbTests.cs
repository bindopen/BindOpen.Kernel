using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(302)]
public class ReferenceDbTests
{
    private BdoReferenceTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoReferenceTests();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        if (_dataTests._ref == null)
        {
            _dataTests.Create1Test();
        }

        var reference = _dataTests._ref;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(reference);
        dbContext.SaveChanges();

        var referenceDb = dbContext.GetReference(reference.Identifier).ToPoco();
        Assert.That(reference.IsDeepEqual(referenceDb) == true, "Bad item storage");
    }

    [Test, Order(2)]
    public void Update2Test()
    {
        if (_dataTests._ref == null)
        {
            InsertTest();
        }

        var reference = _dataTests._ref;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var id = _dataTests._ref?.Identifier;
        _dataTests.Create2Test();
        _dataTests._ref.Identifier = id;

        dbContext.Upsert(reference);
        dbContext.SaveChanges();

        var referenceDb = dbContext.GetReference(reference.Identifier).ToPoco();
        Assert.That(reference.IsDeepEqual(referenceDb) == true, "Bad item storage");
    }

    [Test, Order(3)]
    public void Update3Test()
    {
        if (_dataTests._ref == null)
        {
            InsertTest();
        }

        var reference = _dataTests._ref;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var id = _dataTests._ref?.Identifier;
        _dataTests.Create3Test();
        _dataTests._ref.Identifier = id;

        dbContext.Upsert(reference);
        dbContext.SaveChanges();


        var referenceDb = dbContext.GetReference(reference.Identifier).ToPoco();
        Assert.That(reference.IsDeepEqual(referenceDb) == true, "Bad item storage");
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._ref == null)
        {
            InsertTest();
        }

        var reference = _dataTests._ref;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var dto = reference.ToDto();
        dbContext.Remove(dto);
        dbContext.SaveChanges();

        var referenceDb = dbContext.GetReference(reference.Identifier).ToPoco();
        Assert.That(referenceDb == null, "Bad item storage");
    }
}
