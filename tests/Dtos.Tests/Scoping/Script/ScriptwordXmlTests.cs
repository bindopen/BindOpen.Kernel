using BindOpen.Data;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Contains unit tests for XML serialization and deserialization of <see cref="IBdoScriptword"/>
/// instances via <see cref="ScriptwordDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that scriptwords created by <see cref="BdoScriptwordTests"/> can be mapped
/// to DTOs, persisted to XML files on disk, and reloaded back into domain objects without losing
/// structural or property equality.
/// </remarks>
[TestFixture]
public class ScriptwordXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "Scriptword{0}.xml";
    private BdoScriptwordTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoScriptwordTests"/>
    /// helper before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_xml, 1));
        File.Delete(string.Format(_filePath_xml, 2));
        File.Delete(string.Format(_filePath_xml, 3));

        _dataTests = new BdoScriptwordTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoScriptword"/> instance converts to a
    /// <see cref="ScriptwordDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml1Test()
    {
        var word = _dataTests._scriptwordA;

        var filePath = string.Format(_filePath_xml, 1);

        _isSaved1 = word.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, Is.True, "Scriptword saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved XML file loads back into a <see cref="ScriptwordDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoScriptword"/>.
    /// </summary>
    [Test]
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

    /// <summary>
    /// Verifies that the second test <see cref="IBdoScriptword"/> instance converts to a
    /// <see cref="ScriptwordDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml2Test()
    {
        var word = _dataTests._scriptwordA;

        var filePath = string.Format(_filePath_xml, 2);

        _isSaved2 = word.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, Is.True, "Scriptword saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved XML file loads back into a <see cref="ScriptwordDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoScriptword"/>.
    /// </summary>
    [Test]
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

    /// <summary>
    /// Verifies that the third test <see cref="IBdoScriptword"/> instance converts to a
    /// <see cref="ScriptwordDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml3Test()
    {
        var word = _dataTests._scriptwordA;

        var filePath = string.Format(_filePath_xml, 3);

        _isSaved3 = word.ToDto().SaveXml(filePath);
        Assert.That(_isSaved3, Is.True, "Scriptword saving failed.");
    }

    /// <summary>
    /// Verifies that the third saved XML file loads back into a <see cref="ScriptwordDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoScriptword"/>.
    /// </summary>
    [Test]
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