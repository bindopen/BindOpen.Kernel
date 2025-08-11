using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(303)]
public class MergerDbTests
{
    private BdoMergerTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoMergerTests();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        if (_dataTests._merger == null)
        {
            _dataTests.Create1Test();
        }

        var merger = _dataTests._merger;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        dbContext.Upsert(merger);
        dbContext.SaveChanges();

        var mergerDb = dbContext.GetMerger(merger.Identifier).ToPoco();
        Assert.That(merger.IsDeepEqual(mergerDb) == true, "Bad item storage");
    }

    [Test, Order(2)]
    public void Update2Test()
    {
        if (_dataTests._merger == null)
        {
            InsertTest();
        }

        var merger = _dataTests._merger;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        var id = _dataTests._merger?.Identifier;
        _dataTests.Create2Test();
        _dataTests._merger.Identifier = id;

        dbContext.Upsert(merger);
        dbContext.SaveChanges();

        var mergerDb = dbContext.GetMerger(merger.Identifier).ToPoco();
        Assert.That(merger.IsDeepEqual(mergerDb) == true, "Bad item storage");
    }

    [Test, Order(3)]
    public void Update3Test()
    {
        if (_dataTests._merger == null)
        {
            InsertTest();
        }

        var merger = _dataTests._merger;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        var id = _dataTests._merger?.Identifier;
        _dataTests.Create3Test();
        _dataTests._merger.Identifier = id;

        dbContext.Upsert(merger);
        dbContext.SaveChanges();


        var mergerDb = dbContext.GetMerger(merger.Identifier).ToPoco();
        Assert.That(merger.IsDeepEqual(mergerDb) == true, "Bad item storage");
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._merger == null)
        {
            InsertTest();
        }

        var merger = _dataTests._merger;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        var dto = merger.ToDb();
        dbContext.Remove(dto);
        dbContext.SaveChanges();

        var mergerDb = dbContext.GetMerger(merger.Identifier).ToPoco();
        Assert.That(mergerDb == null, "Bad item storage");
    }
}
