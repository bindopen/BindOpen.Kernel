//using DeepEqual.Syntax;
//using NUnit.Framework;

//namespace BindOpen.Data.Meta;

//[TestFixture, Order(304)]
//public class ConfigurationDbTests
//{
//    private BdoConfigurationTests _dataTests;

//    [OneTimeSetUp]
//    public void OneTimeSetUp()
//    {
//        _dataTests = new BdoConfigurationTests();
//        _dataTests.OneTimeSetUp();
//    }

//    [Test, Order(1)]
//    public void InsertTest()
//    {
//        var config10 = _dataTests._config10;

//        using var dbContext = GlobalIOTestData.CreateDbContext();
//        dbContext.Upsert(config10);
//        dbContext.SaveChanges();

//        var expDb = dbContext.GetConfiguration(config10.Identifier).ToPoco();
//        Assert.That(config10.IsDeepEqual(expDb) == true, "Bad item storage");
//    }

//    [Test, Order(2)]
//    public void Update2Test()
//    {
//        var exp = _dataTests._config10a;

//        using var dbContext = GlobalIOTestData.CreateDbContext();
//        var id = _dataTests._exp?.Identifier;
//        _dataTests.Create2Test();
//        _dataTests._exp.Identifier = id;

//        dbContext.Upsert(exp);
//        dbContext.SaveChanges();

//        var expDb = dbContext.GetConfiguration(exp.Identifier).ToPoco();
//        Assert.That(exp.IsDeepEqual(expDb) == true, "Bad item storage");
//    }

//    [Test, Order(3)]
//    public void Update3Test()
//    {
//        var exp = _dataTests._config20;

//        using var dbContext = GlobalIOTestData.CreateDbContext();
//        var id = _dataTests._exp?.Identifier;
//        _dataTests.Create3Test();
//        _dataTests._exp.Identifier = id;

//        dbContext.Upsert(exp);
//        dbContext.SaveChanges();


//        var expDb = dbContext.GetConfiguration(exp.Identifier).ToPoco();
//        Assert.That(exp.IsDeepEqual(expDb) == true, "Bad item storage");
//    }

//    [Test, Order(4)]
//    public void DeleteTest()
//    {
//        using var dbContext = GlobalIOTestData.CreateDbContext();

//        dbContext.Remove(_dataTests._config10.ToDto());
//        dbContext.Remove(_dataTests._config10a.ToDto());
//        dbContext.Remove(_dataTests._config20.ToDto());

//        dbContext.SaveChanges();

//        var expDb10 = dbContext.GetConfiguration(_dataTests._config10.Identifier).ToPoco();
//        Assert.That(expDb10 == null, "Bad item storage");

//        var expDb10a = dbContext.GetConfiguration(_dataTests._config10a.Identifier).ToPoco();
//        Assert.That(expDb10a == null, "Bad item storage");

//        var expDb20 = dbContext.GetConfiguration(_dataTests._config20.Identifier).ToPoco();
//        Assert.That(expDb20 == null, "Bad item storage");
//    }
//}
