using BindOpen.Data.Objects;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for JSON serialization and deserialization of <see cref="ITBdoDictionary{T}"/>
/// string dictionaries via <see cref="StringDictionaryDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that string dictionaries created by <see cref="BdoStringDictionaryTests"/> can be
/// mapped to DTOs, persisted to JSON files on disk, and reloaded back into domain objects without losing
/// key-value entries or structural equality.
/// </remarks>
[TestFixture]
public class StringDictionaryJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "StringDictionary{0}.json";
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
        File.Delete(string.Format(_filePath_json, 1));
        File.Delete(string.Format(_filePath_json, 2));
        File.Delete(string.Format(_filePath_json, 3));

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
    /// <see cref="StringDictionaryDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._dico;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved1, Is.True, "StringDictionary saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved JSON file loads back into a <see cref="StringDictionaryDto"/>,
    /// converts to a POCO via <c>ToPoco&lt;string&gt;()</c>, and matches the original dictionary.
    /// </summary>
    [Test]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var exp = _dataTests._dico;
        var exp_fromDto = JsonHelper.LoadJson<StringDictionaryDto>(filePath).ToPoco<string>();
        Assert.That(Equals(exp, exp_fromDto), Is.True, "Error while loading StringDictionary from JSON.");
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="ITBdoDictionary{T}"/> string dictionary converts to a
    /// <see cref="StringDictionaryDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._dico;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved2, Is.True, "StringDictionary saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved JSON file loads back into a <see cref="StringDictionaryDto"/>,
    /// converts to a POCO via <c>ToPoco&lt;string&gt;()</c>, and matches the original dictionary.
    /// </summary>
    [Test]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var exp = _dataTests._dico;
        var exp_fromDto = JsonHelper.LoadJson<StringDictionaryDto>(filePath).ToPoco<string>();
        Assert.That(Equals(exp, exp_fromDto), Is.True, "Error while loading StringDictionary from JSON.");
    }

    // Test 3

    /// <summary>
    /// Verifies that the third test <see cref="ITBdoDictionary{T}"/> string dictionary converts to a
    /// <see cref="StringDictionaryDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson3Test()
    {
        _dataTests.Create3Test();
        var exp = _dataTests._dico;

        var filePath = string.Format(_filePath_json, 3);

        _isSaved3 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved3, Is.True, "StringDictionary saving failed.");
    }

    /// <summary>
    /// Verifies that the third saved JSON file loads back into a <see cref="StringDictionaryDto"/>,
    /// converts to a POCO via <c>ToPoco&lt;string&gt;()</c>, and matches the original dictionary.
    /// </summary>
    [Test]
    public void LoadJson3Test()
    {
        if (!_isSaved3)
        {
            SaveJson3Test();
        }

        var filePath = string.Format(_filePath_json, 3);

        var exp = _dataTests._dico;
        var exp_fromDto = JsonHelper.LoadJson<StringDictionaryDto>(filePath).ToPoco<string>();
        Assert.That(Equals(exp, exp_fromDto), Is.True, "Error while loading StringDictionary from JSON.");
    }
}