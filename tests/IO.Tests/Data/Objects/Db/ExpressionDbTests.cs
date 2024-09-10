using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(304)]
public class ExpressionDbTests
{
    private BdoExpressionTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoExpressionTests();
        _dataTests.OneTimeSetUp();
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

        var exp = _dataTests._exp;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var id = _dataTests._exp?.Identifier;
        _dataTests.Create2Test();
        _dataTests._exp.Identifier = id;

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

        var exp = _dataTests._exp;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var id = _dataTests._exp?.Identifier;
        _dataTests.Create3Test();
        _dataTests._exp.Identifier = id;

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
        var dto = exp.ToDto();
        dbContext.Remove(dto);
        dbContext.SaveChanges();

        var expDb = dbContext.GetExpression(exp.Identifier).ToPoco();
        Assert.That(expDb == null, "Bad item storage");
    }
}
