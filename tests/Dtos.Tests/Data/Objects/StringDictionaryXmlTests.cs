using BindOpen.Data.Objects;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for XML serialization and deserialization of <see cref="ITBdoDictionary{T}"/>
/// string dictionaries via <see cref="StringDictionaryDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that string dictionaries created by <see cref="BdoStringDictionaryTests"/> can be
/// mapped to DTOs, persisted to XML files on disk, and reloaded back into domain objects without losing
/// key-value entries or structural equality.
/// </remarks>
[TestFixture]
public class StringDictionaryXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "StringDictionary{0}.xml";
    private BdoStringDictionaryTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoStringDictionaryTests"/>
    /// helper before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_xml, 1));
        File.Delete(string.Format(_filePath_xml, 2));
        File.Delete(string.Format(_filePath_xml, 3));

        _dataTests = new BdoStringDictionaryTests();
        _dataTests.OneTimeSetUp();
    }

    /// <summary>
    /// Determines whether two <see cref="ITBdoDictionary{T}"/> string dictionary instances
    /// are non-null and deeply equal.
    /// </summary>
    /// <param name="exp1">The first dictionary instance to compare.</param>
    /// <param name="exp2">The second dictionary instance to compare.</param>
    /// <returns><c>true</c> if both dictionaries are non-null and deeply equal; otherwise, <c>false</c>.</returns>
    public static bool Equals(
        ITBdoDictionary<string> exp1,
        ITBdoDictionary<string> exp2)
    {
        var b = exp1 != null && exp2 != null
            && exp1.IsDeepEqual(exp2);
        return b;
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="ITBdoDictionary{T}"/> string dictionary converts to a
    /// <see cref="StringDictionaryDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._dico;

        var filePath = string.Format(_filePath_xml, 1);

        _isSaved1 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved1, Is.True, "StringDictionary saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved XML file loads back into a <see cref="StringDictionaryDto"/>,
    /// converts to a POCO via <c>ToPoco&lt;string&gt;()</c>, and matches the original dictionary.
    /// </summary>
    [Test]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_xml, 1);

        var exp = _dataTests._dico;
        var exp_fromDto = XmlHelper.LoadXml<StringDictionaryDto>(filePath).ToPoco<string>();
        Assert.That(Equals(exp, exp_fromDto), Is.True, "Error while loading StringDictionary from XML.");
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="ITBdoDictionary{T}"/> string dictionary converts to a
    /// <see cref="StringDictionaryDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._dico;

        var filePath = string.Format(_filePath_xml, 2);

        _isSaved2 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved2, Is.True, "StringDictionary saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved XML file loads back into a <see cref="StringDictionaryDto"/>,
    /// converts to a POCO via <c>ToPoco&lt;string&gt;()</c>, and matches the original dictionary.
    /// </summary>
    [Test]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_xml, 2);

        var exp = _dataTests._dico;
        var exp_fromDto = XmlHelper.LoadXml<StringDictionaryDto>(filePath).ToPoco<string>();
        Assert.That(Equals(exp, exp_fromDto), Is.True, "Error while loading StringDictionary from XML.");
    }

    // Test 3

    /// <summary>
    /// Verifies that the third test <see cref="ITBdoDictionary{T}"/> string dictionary converts to a
    /// <see cref="StringDictionaryDto"/> and saves cleanly to an XML file.
    /// </summary>
    [Test]
    public void SaveXml3Test()
    {
        _dataTests.Create3Test();
        var exp = _dataTests._dico;

        var filePath = string.Format(_filePath_xml, 3);

        _isSaved3 = exp.ToDto().SaveXml(filePath);
        Assert.That(_isSaved3, Is.True, "StringDictionary saving failed.");
    }

    /// <summary>
    /// Verifies that the third saved XML file loads back into a <see cref="StringDictionaryDto"/>,
    /// converts to a POCO via <c>ToPoco&lt;string&gt;()</c>, and matches the original dictionary.
    /// </summary>
    [Test]
    public void LoadXml3Test()
    {
        if (!_isSaved3)
        {
            SaveXml3Test();
        }

        var filePath = string.Format(_filePath_xml, 3);

        var exp = _dataTests._dico;
        var exp_fromDto = XmlHelper.LoadXml<StringDictionaryDto>(filePath).ToPoco<string>();
        Assert.That(Equals(exp, exp_fromDto), Is.True, "Error while loading StringDictionary from XML.");
    }
}