using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class StringDictionaryJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "StringDictionary{0}.json";
    private BdoStringDictionaryTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        _dataTests = new BdoStringDictionaryTests();
        _dataTests.OneTimeSetUp();
    }

    public static bool Equals(
        ITBdoDictionary<string> exp1,
        ITBdoDictionary<string> exp2)
    {
        var b = exp1 != null && exp2 != null
            && exp1.IsDeepEqual(exp2);
        return b;
    }

    // Test 1

    [Test, Order(10)]
    public void SaveJson1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._dico;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved1, "StringDictionary saving failed");
    }

    [Test, Order(11)]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var exp = _dataTests._dico;
        var exp_fromDto = JsonHelper.LoadJson<StringDictionaryDto>(filePath).ToPoco<string>();
        Assert.That(Equals(exp, exp_fromDto), "Error while loading");
    }

    // Test 2

    [Test, Order(20)]
    public void SaveJson2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._dico;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved2, "StringDictionary saving failed");
    }

    [Test, Order(21)]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var exp = _dataTests._dico;
        var exp_fromDto = JsonHelper.LoadJson<StringDictionaryDto>(filePath).ToPoco<string>();
        Assert.That(Equals(exp, exp_fromDto), "Error while loading");
    }

    // Test 3

    [Test, Order(30)]
    public void SaveJson3Test()
    {
        _dataTests.Create3Test();
        var exp = _dataTests._dico;

        var filePath = string.Format(_filePath_json, 3);

        _isSaved3 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved3, "StringDictionary saving failed");
    }

    [Test, Order(31)]
    public void LoadJson3Test()
    {
        if (!_isSaved3)
        {
            SaveJson3Test();
        }

        var filePath = string.Format(_filePath_json, 3);

        var exp = _dataTests._dico;
        var exp_fromDto = JsonHelper.LoadJson<StringDictionaryDto>(filePath).ToPoco<string>();
        Assert.That(Equals(exp, exp_fromDto), "Error while loading");
    }
}