using BindOpen.Data;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Script;

[TestFixture, Order(210)]
public class ScriptwordJsonTests
{
    private readonly string _filePath_json = GlobalTestData.WorkingFolder + "Scriptword{0}.json";
    private BdoScriptwordTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        _dataTests = new BdoScriptwordTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveJson1Test()
    {
        var word = _dataTests._scriptwordA;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = word.ToDto().SaveJson(filePath);
        Assert.That(_isSaved1, "Scriptword saving failed");
    }

    [Test, Order(11)]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var word = _dataTests._scriptwordA;
        var word_fromDto = JsonHelper.LoadJson<ScriptwordDto>(filePath).ToPoco();
        BdoScriptwordTests.AssertEquals(word, word_fromDto);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveJson2Test()
    {
        var word = _dataTests._scriptwordA;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = word.ToDto().SaveJson(filePath);
        Assert.That(_isSaved2, "Scriptword saving failed");
    }

    [Test, Order(21)]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var word = _dataTests._scriptwordA;
        var word_fromDto = JsonHelper.LoadJson<ScriptwordDto>(filePath).ToPoco();
        BdoScriptwordTests.AssertEquals(word, word_fromDto);
    }

    // Test 3

    [Test, Order(30)]
    public void SaveJson3Test()
    {
        var word = _dataTests._scriptwordA;

        var filePath = string.Format(_filePath_json, 3);

        _isSaved3 = word.ToDto().SaveJson(filePath);
        Assert.That(_isSaved3, "Scriptword saving failed");
    }

    [Test, Order(31)]
    public void LoadJson3Test()
    {
        if (!_isSaved3)
        {
            SaveJson3Test();
        }

        var filePath = string.Format(_filePath_json, 3);

        var word = _dataTests._scriptwordA;
        var word_fromDto = JsonHelper.LoadJson<ScriptwordDto>(filePath).ToPoco();
        BdoScriptwordTests.AssertEquals(word, word_fromDto);
    }
}
