using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

[TestFixture, Order(304)]
public class MetaObjectDbTests
{
    private BdoMetaObjectTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoMetaObjectTests();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        if (_dataTests._metaObject == null)
        {
            _dataTests.NewTest();
        }

        var meta = _dataTests._metaObject;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(meta);
        dbContext.SaveChanges();

        var metaFromDb = dbContext.GetMetaData(meta.Identifier).ToPoco();
        metaFromDb.WithDeepEqual(meta)
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(2)]
    public void Update1Test()
    {
        if (_dataTests._metaObject == null)
        {
            InsertTest();
        }

        var id = _dataTests._metaObject?.Identifier;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        _dataTests.ToMetaTest();
        var meta = _dataTests._metaObject;
        meta.Identifier = id;

        dbContext.Upsert(meta);
        dbContext.SaveChanges();

        var metaFromDb = dbContext.GetMetaData(meta.Identifier).ToPoco();
        metaFromDb.WithDeepEqual(meta)
            .SkipDefault<IBdoMetaData>()
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(3)]
    public void Update2Test()
    {
        if (_dataTests._metaObject == null)
        {
            InsertTest();
        }

        var id = _dataTests._metaObject?.Identifier;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        _dataTests.NewTest();
        var meta = _dataTests._metaObject;
        meta.Identifier = id;

        dbContext.Upsert(meta);
        dbContext.SaveChanges();

        var metaFromDb = dbContext.GetMetaData(meta.Identifier).ToPoco();
        metaFromDb.WithDeepEqual(meta)
            .SkipDefault<IBdoMetaData>()
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._metaObject == null)
        {
            InsertTest();
        }

        var meta = _dataTests._metaObject;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Delete(meta);
        dbContext.SaveChanges();

        var metaDb = dbContext.GetMetaData(meta.Identifier).ToPoco();
        Assert.That(metaDb == null, "Bad item storage");
    }
}
