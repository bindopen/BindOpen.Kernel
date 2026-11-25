using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for JSON serialization and deserialization of <see cref="IBdoReference"/>
/// instances via <see cref="ReferenceDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that standard and scoping references created by <see cref="BdoReferenceTests"/>
/// and <see cref="BdoScopingReferenceTests"/> can be mapped to DTOs, persisted to JSON files on disk,
/// and reloaded back into domain objects without losing structural or property equality.
/// </remarks>
[TestFixture]
public class ReferenceJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "Reference{0}.json";
    private BdoReferenceTests _dataTests;
    private BdoScopingReferenceTests _scopingTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoReferenceTests"/>
    /// and <see cref="BdoScopingReferenceTests"/> helpers before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_json, 1));
        File.Delete(string.Format(_filePath_json, 2));
        File.Delete(string.Format(_filePath_json, 3));

        _dataTests = new BdoReferenceTests();
        _dataTests.OneTimeSetUp();

        _scopingTests = new BdoScopingReferenceTests();
        _scopingTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoReference"/> instance converts to a
    /// <see cref="ReferenceDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson1Test()
    {
        _dataTests.Create1Test();
        var reference = _dataTests._reference;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = reference.ToDto().SaveJson(filePath);
        Assert.That(_isSaved1, Is.True, "Reference saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved JSON file loads back into a <see cref="ReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoReference"/>.
    /// </summary>
    [Test]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var reference = _dataTests._reference;
        var reference_fromDto = JsonHelper.LoadJson<ReferenceDto>(filePath).ToPoco();
        BdoReferenceTests.AssertEquals(reference, reference_fromDto);
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="IBdoReference"/> instance converts to a
    /// <see cref="ReferenceDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson2Test()
    {
        _dataTests.Create2Test();
        var reference = _dataTests._reference;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = reference.ToDto().SaveJson(filePath);
        Assert.That(_isSaved2, Is.True, "Reference saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved JSON file loads back into a <see cref="ReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoReference"/>.
    /// </summary>
    [Test]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var reference = _dataTests._reference;
        var reference_fromDto = JsonHelper.LoadJson<ReferenceDto>(filePath).ToPoco();
        BdoReferenceTests.AssertEquals(reference, reference_fromDto);
    }

    // Test 3

    /// <summary>
    /// Verifies that the third scoping test <see cref="IBdoReference"/> instance converts to a
    /// <see cref="ReferenceDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson3Test()
    {
        _scopingTests.Create3Test();
        var reference = _dataTests._reference;

        var filePath = string.Format(_filePath_json, 3);

        _isSaved3 = reference.ToDto().SaveJson(filePath);
        Assert.That(_isSaved3, Is.True, "Reference saving failed.");
    }

    /// <summary>
    /// Verifies that the third saved JSON file loads back into a <see cref="ReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoReference"/>.
    /// </summary>
    [Test]
    public void LoadJson3Test()
    {
        if (!_isSaved3)
        {
            SaveJson3Test();
        }

        var filePath = string.Format(_filePath_json, 3);

        var reference = _dataTests._reference;
        var reference_fromDto = JsonHelper.LoadJson<ReferenceDto>(filePath).ToPoco();
        BdoReferenceTests.AssertEquals(reference, reference_fromDto);
    }
}