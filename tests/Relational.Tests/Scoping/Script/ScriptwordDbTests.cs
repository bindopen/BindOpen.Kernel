using BindOpen.Data.Meta;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

[TestFixture, Order(304)]
public class ScriptwordFromDbTests
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

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        dbContext.Upsert(word);
        dbContext.SaveChanges();

        var wordFromDb = dbContext.GetMetaData(word.Identifier).ToPoco();
        wordFromDb.WithDeepEqual(word)
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(2)]
    public void Update2Test()
    {
        if (_dataTests._scriptwordA == null)
        {
            InsertTest();
        }

        var id = _dataTests._scriptwordA?.Identifier;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        var word = _dataTests._scriptwordA;
        word.Identifier = id;

        dbContext.Upsert(word);
        dbContext.SaveChanges();

        var wordFromDb = dbContext.GetMetaData(word.Identifier).ToPoco();
        wordFromDb.WithDeepEqual(word)
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(3)]
    public void Update3Test()
    {
        if (_dataTests._scriptwordA == null)
        {
            InsertTest();
        }

        var id = _dataTests._scriptwordA?.Identifier;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        var word = _dataTests._scriptwordA;
        word.Identifier = id;

        dbContext.Upsert(word);
        dbContext.SaveChanges();

        var wordFromDb = dbContext.GetMetaData(word.Identifier).ToPoco();
        wordFromDb.WithDeepEqual(word)
            .IgnoreProperty<IBdoMetaData>(x => x.Parent)
            .Assert();
    }

    [Test, Order(4)]
    public void DeleteTest()
    {
        if (_dataTests._scriptwordA == null)
        {
            InsertTest();
        }

        var word = _dataTests._scriptwordA;

        using var dbContext = GlobalRelationalTestData.CreateDbContext();
        var dbItem = word.ToDb(dbContext);
        dbContext.Remove(dbItem);
        dbContext.SaveChanges();

        var wordFromDb = dbContext.GetScriptword(word.Identifier).ToPoco();
        Assert.That(wordFromDb == null, "Bad item storage");
    }
}
