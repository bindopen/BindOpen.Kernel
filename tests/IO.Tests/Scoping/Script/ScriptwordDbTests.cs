using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

[TestFixture, Order(304)]
public class ScriptwordDbTests
{
    private BdoScriptwordTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoScriptwordTests();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        var word = _dataTests._scriptwordA;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(word);
        dbContext.SaveChanges();

        var wordDb = dbContext.GetScriptword(word.Identifier).ToPoco();
        Assert.That(word.IsDeepEqual(wordDb) == true, "Bad item storage");
    }

    [Test, Order(2)]
    public void Update2Test()
    {
        if (_dataTests._scriptwordA == null)
        {
            InsertTest();
        }

        var id = _dataTests._scriptwordA?.Identifier;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var word = _dataTests._scriptwordA;
        word.Identifier = id;

        dbContext.Upsert(word);
        dbContext.SaveChanges();

        var wordDb = dbContext.GetScriptword(word.Identifier).ToPoco();
        Assert.That(word.IsDeepEqual(wordDb) == true, "Bad item storage");
    }

    [Test, Order(3)]
    public void Update3Test()
    {
        if (_dataTests._scriptwordA == null)
        {
            InsertTest();
        }

        var id = _dataTests._scriptwordA?.Identifier;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var word = _dataTests._scriptwordA;
        word.Identifier = id;

        dbContext.Upsert(word);
        dbContext.SaveChanges();


        var wordDb = dbContext.GetScriptword(word.Identifier).ToPoco();
        Assert.That(word.IsDeepEqual(wordDb) == true, "Bad item storage");
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._scriptwordA == null)
        {
            InsertTest();
        }

        var word = _dataTests._scriptwordA;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var dbItem = word.ToDb(dbContext);
        dbContext.Remove(dbItem);
        dbContext.SaveChanges();

        var wordDb = dbContext.GetScriptword(word.Identifier).ToPoco();
        Assert.That(wordDb == null, "Bad item storage");
    }
}
