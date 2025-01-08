using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

[TestFixture, Order(304)]
public class ConfigurationDbTests
{
    private BdoConfigurationTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoConfigurationTests();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        var config = _dataTests._config10;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(config);
        dbContext.SaveChanges();

        var configDb = dbContext.GetConfiguration(config.Identifier).ToPoco();

        config.WithDeepEqual(configDb)
            .SkipDefault<IBdoMetaData>()
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(2)]
    public void Update2Test()
    {
        if (_dataTests._config10a == null)
        {
            InsertTest();
        }

        var id = _dataTests._config10?.Identifier;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var config = _dataTests._config10a;
        config.Identifier = id;

        dbContext.Upsert(config);
        dbContext.SaveChanges();

        var configDb = dbContext.GetConfiguration(config.Identifier).ToPoco();
        config.WithDeepEqual(configDb)
            .SkipDefault<IBdoMetaData>()
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(3)]
    public void Update3Test()
    {
        if (_dataTests._config20 == null)
        {
            InsertTest();
        }

        var id = _dataTests._config10?.Identifier;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var config = _dataTests._config20;
        config.Identifier = id;

        dbContext.Upsert(config);
        dbContext.SaveChanges();


        var configDb = dbContext.GetConfiguration(config.Identifier).ToPoco();
        config.WithDeepEqual(configDb)
            .SkipDefault<IBdoMetaData>()
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._config10 == null)
        {
            InsertTest();
        }

        var config = _dataTests._config10;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Delete(config);
        dbContext.SaveChanges();

        var configDb = dbContext.GetConfiguration(config.Identifier).ToPoco();
        Assert.That(configDb == null, "Bad item storage");
    }
}
