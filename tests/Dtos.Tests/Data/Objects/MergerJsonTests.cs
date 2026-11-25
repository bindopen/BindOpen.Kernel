using BindOpen.Data.Objects;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data;

/// <summary>
/// Contains unit tests for JSON serialization and deserialization of <see cref="IBdoMerger"/>
/// instances via <see cref="MergerDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that mergers created by <see cref="BdoMergerTests"/> can be mapped to DTOs,
/// persisted to JSON files on disk, and reloaded back into domain objects without losing
/// structural or property equality.
/// </remarks>
[TestFixture]
public class MergerJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "Merger{0}.json";
    private BdoMergerTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoMergerTests"/>
    /// helper before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_json, 1));
        File.Delete(string.Format(_filePath_json, 2));
        File.Delete(string.Format(_filePath_json, 3));

        _dataTests = new BdoMergerTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoMerger"/> instance converts to a
    /// <see cref="MergerDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson1Test()
    {
        _dataTests.Create1Test();
        var exp = _dataTests._merger;

        var filePath = string.Format(_filePath_json, 1);

        _isSaved1 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved1, Is.True, "Merger saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved JSON file loads back into a <see cref="MergerDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoMerger"/>.
    /// </summary>
    [Test]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var exp = _dataTests._merger;
        var exp_fromDto = JsonHelper.LoadJson<MergerDto>(filePath).ToPoco();
        BdoMergerTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="IBdoMerger"/> instance converts to a
    /// <see cref="MergerDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson2Test()
    {
        _dataTests.Create2Test();
        var exp = _dataTests._merger;

        var filePath = string.Format(_filePath_json, 2);

        _isSaved2 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved2, Is.True, "Merger saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved JSON file loads back into a <see cref="MergerDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoMerger"/>.
    /// </summary>
    [Test]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var exp = _dataTests._merger;
        var exp_fromDto = JsonHelper.LoadJson<MergerDto>(filePath).ToPoco();
        BdoMergerTests.AssertEquals(exp, exp_fromDto);
    }

    // Test 3

    /// <summary>
    /// Verifies that the third test <see cref="IBdoMerger"/> instance converts to a
    /// <see cref="MergerDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson3Test()
    {
        _dataTests.Create3Test();
        var exp = _dataTests._merger;

        var filePath = string.Format(_filePath_json, 3);

        _isSaved3 = exp.ToDto().SaveJson(filePath);
        Assert.That(_isSaved3, Is.True, "Merger saving failed.");
    }

    /// <summary>
    /// Verifies that the third saved JSON file loads back into a <see cref="MergerDto"/>,
    /// converts to a POCO via <c>ToPoco()</c>, and matches the original <see cref="IBdoMerger"/>.
    /// </summary>
    [Test]
    public void LoadJson3Test()
    {
        if (!_isSaved3)
        {
            SaveJson3Test();
        }

        var filePath = string.Format(_filePath_json, 3);

        var exp = _dataTests._merger;
        var exp_fromDto = JsonHelper.LoadJson<MergerDto>(filePath).ToPoco();
        BdoMergerTests.AssertEquals(exp, exp_fromDto);
    }
}