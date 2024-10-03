using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(301)]
public class StringDictionaryDbTests
{
    private BdoStringDictionaryTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoStringDictionaryTests();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void InsertTest()
    {
        if (_dataTests._dico == null)
        {
            _dataTests.Create1Test();
        }

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Upsert(_dataTests._dico);
        dbContext.SaveChanges();
    }

    [Test, Order(2)]
    public void UpdateTest()
    {
        if (_dataTests._dico == null)
        {
            InsertTest();
        }

        var dico = _dataTests._dico;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dico["value1"] = "newValue";
        dico.Add("test", "TITI");

        dbContext.Upsert(dico);
        dbContext.SaveChanges();

        var dicoDb = dbContext.GetStringDictionary(dico.Identifier).ToPoco<string>();
        Assert.That(dico.IsDeepEqual(dicoDb) == true, "Bad item storage");
    }

    [Test, Order(3)]
    public void DeleteTest()
    {
        if (_dataTests._dico == null)
        {
            InsertTest();
        }

        var dico = _dataTests._dico;

        using var dbContext = GlobalIOTestData.CreateDbContext();
        var dto = dico.ToDto();
        dbContext.Remove(dto);
        dbContext.SaveChanges();

        var dicoDb = dbContext.GetExpression(dico.Identifier).ToPoco();
        Assert.That(dicoDb == null, "Bad item storage");
    }
}
