using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

[TestFixture, Order(304)]
public class MetaScalarSetDbTests
{
    private BdoMetaScalarSetTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoMetaScalarSetTests();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        if (_dataTests._metaSet == null)
        {
            _dataTests.NewTest();
        }

        var set = _dataTests._metaSet;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(set);
        dbContext.SaveChanges();

        var setDb = dbContext.GetMetaSet(set.Identifier).ToPoco();

        set[0].ShouldDeepEqual(setDb[set[0].Name]);

        Assert.That(set.IsDeepEqual(setDb) == true, "Bad item storage");
    }

    [Test, Order(2)]
    public void UpdateTest()
    {
        if (_dataTests._metaSet == null)
        {
            InsertTest();
        }

        var id = _dataTests._metaSet?.Identifier;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        _dataTests.UpdateCheckRepairTest();
        var set = _dataTests._metaSet;
        set.Identifier = id;

        dbContext.Upsert(set);
        dbContext.SaveChanges();

        var setDb = dbContext.GetMetaSet(set.Identifier).ToPoco();
        Assert.That(set.IsDeepEqual(setDb) == true, "Bad item storage");
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._metaSet == null)
        {
            InsertTest();
        }

        var set = _dataTests._metaSet;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var dto = set.ToDto();
        dbContext.Remove(dto);
        dbContext.SaveChanges();

        var setDb = dbContext.GetMetaSet(set.Identifier).ToPoco();
        Assert.That(setDb == null, "Bad item storage");
    }
}
