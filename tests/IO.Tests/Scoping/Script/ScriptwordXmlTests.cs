using BindOpen.Data;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Script;

[TestFixture, Order(210)]
public class ScriptwordXmlTests
{
    private readonly string _filePath_xml = GlobalTestData.WorkingFolder + "Scriptword{0}.xml";
    private BdoScriptwordTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);

        _dataTests = new BdoScriptwordTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveXml1Test()
    {
        var word = _dataTests._scriptwordA;

        var filePath = string.Format(_filePath_xml, 1);

        _isSaved1 = word.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, "Scriptword saving failed");
    }

    [Test, Order(11)]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_xml, 1);

        var word = _dataTests._scriptwordA;
        var word_fromDto = XmlHelper.LoadXml<ScriptwordDto>(filePath).ToPoco();
        BdoScriptwordTests.AssertEquals(word, word_fromDto);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveXml2Test()
    {
        var word = _dataTests._scriptwordA;

        var filePath = string.Format(_filePath_xml, 2);

        _isSaved2 = word.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, "Scriptword saving failed");
    }

    [Test, Order(21)]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_xml, 2);

        var word = _dataTests._scriptwordA;
        var word_fromDto = XmlHelper.LoadXml<ScriptwordDto>(filePath).ToPoco();
        BdoScriptwordTests.AssertEquals(word, word_fromDto);
    }

    // Test 3

    [Test, Order(30)]
    public void SaveXml3Test()
    {
        var word = _dataTests._scriptwordA;

        var filePath = string.Format(_filePath_xml, 3);

        _isSaved3 = word.ToDto().SaveXml(filePath);
        Assert.That(_isSaved3, "Scriptword saving failed");
    }

    [Test, Order(31)]
    public void LoadXml3Test()
    {
        if (!_isSaved3)
        {
            SaveXml3Test();
        }

        var filePath = string.Format(_filePath_xml, 3);

        var word = _dataTests._scriptwordA;
        var word_fromDto = XmlHelper.LoadXml<ScriptwordDto>(filePath).ToPoco();
        BdoScriptwordTests.AssertEquals(word, word_fromDto);
    }
}
