using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(304)]
public class ExpressionDbTests
{
    private BdoExpressionTests _dataTests;
    private BdoScopingExpressionTests _scopingTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoExpressionTests();
        _dataTests.OneTimeSetUp();

        _scopingTests = new BdoScopingExpressionTests();
        _scopingTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        if (_dataTests._exp == null)
        {
            _dataTests.Create1Test();
        }

        var exp = _dataTests._exp;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(exp);
        dbContext.SaveChanges();

        var expDb = dbContext.GetExpression(exp.Identifier).ToPoco();
        Assert.That(exp.IsDeepEqual(expDb) == true, "Bad item storage");
    }

    [Test, Order(2)]
    public void Update2Test()
    {
        if (_dataTests._exp == null)
        {
            InsertTest();
        }

        var id = _dataTests._exp?.Identifier;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        _dataTests.Create2Test();
        var exp = _dataTests._exp;
        exp.Identifier = id;

        dbContext.Upsert(exp);
        dbContext.SaveChanges();

        var expDb = dbContext.GetExpression(exp.Identifier).ToPoco();
        Assert.That(exp.IsDeepEqual(expDb) == true, "Bad item storage");
    }

    [Test, Order(3)]
    public void Update3Test()
    {
        if (_dataTests._exp == null)
        {
            InsertTest();
        }

        var id = _dataTests._exp?.Identifier;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        _scopingTests.Create3Test();
        var exp = _dataTests._exp;
        exp.Identifier = id;

        dbContext.Upsert(exp);
        dbContext.SaveChanges();


        var expDb = dbContext.GetExpression(exp.Identifier).ToPoco();
        Assert.That(exp.IsDeepEqual(expDb) == true, "Bad item storage");
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._exp == null)
        {
            InsertTest();
        }

        var exp = _dataTests._exp;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Delete(exp);
        dbContext.SaveChanges();

        var expDb = dbContext.GetExpression(exp.Identifier).ToPoco();
        Assert.That(expDb == null, "Bad item storage");
    }
}
