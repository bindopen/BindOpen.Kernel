using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// Contains unit tests for JSON serialization and deserialization of <see cref="IBdoClassReference"/>
/// instances via <see cref="ClassReferenceDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that class references created by <see cref="BdoClassReferenceTests"/> can be
/// mapped to DTOs, persisted to JSON files on disk, and reloaded back into domain objects without losing
/// structural or property equality.
/// </remarks>
[TestFixture]
public class ClassReferenceJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "ClassReference{0}.json";
    private BdoClassReferenceTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoClassReferenceTests"/>
    /// helper before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_json, 1));
        File.Delete(string.Format(_filePath_json, 2));

        _dataTests = new BdoClassReferenceTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoClassReference"/> instance converts to a
    /// <see cref="ClassReferenceDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._classRef1;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved1, Is.True, "ClassReference saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved JSON file loads back into a <see cref="ClassReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoClassReference"/>.
    /// </summary>
    [Test]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var exp = _dataTests._classRef1;
        var exp_fromDto = JsonHelper.LoadJson<ClassReferenceDto>(filePath).ToPoco();
        BdoClassReferenceTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="IBdoClassReference"/> instance converts to a
    /// <see cref="ClassReferenceDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._classRef2;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved2, Is.True, "ClassReference saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved JSON file loads back into a <see cref="ClassReferenceDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoClassReference"/>.
    /// </summary>
    [Test]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var exp = _dataTests._classRef2;
        var exp_fromDto = JsonHelper.LoadJson<ClassReferenceDto>(filePath).ToPoco();
        BdoClassReferenceTests.AssertEquals(exp, exp_fromDto);
    }
}